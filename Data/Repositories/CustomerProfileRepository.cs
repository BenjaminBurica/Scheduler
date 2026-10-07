using Entities;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace Data.Repositories;

public class CustomerProfileRepository : ICustomerProfileRepository
{
    private readonly IConfiguration configuration;

    public CustomerProfileRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<int> SaveCustomerProfileAsync(
        CustomerProfile customerProfile)
    {
        var connectionString =
            configuration.GetConnectionString("Schedule") ?? "";

        await using var dataSource =
            NpgsqlDataSource.Create(connectionString);

        await using var connection =
            await dataSource.OpenConnectionAsync();

        const string customerProfileSql = @"
            INSERT INTO public.customer_profile
            (
                service_provider_id,
                first_name,
                phone_number,
                service_type
            )
            VALUES
            (
                $1, $2, $3, $4
            )
            RETURNING customer_profile_id;
        ";

        int customerProfileId;

        await using (var command =
            new NpgsqlCommand(customerProfileSql, connection))
        {
            command.Parameters.AddWithValue(
                customerProfile.ServiceProviderId);

            command.Parameters.AddWithValue(
                customerProfile.FirstName);

            command.Parameters.AddWithValue(
                customerProfile.PhoneNumber);

            command.Parameters.AddWithValue(
                customerProfile.ServiceType);

            customerProfileId =
                Convert.ToInt32(
                    await command.ExecuteScalarAsync());
        }


        const string stepSql = @"
            INSERT INTO public.customer_profile_step
            (
                customer_profile_id,
                step_order,
                step_type,
                duration_minutes
            )
            VALUES
            (
                $1, $2, $3, $4
            );
        ";


        foreach (var step in customerProfile.Steps)
        {
            await using var command =
                new NpgsqlCommand(stepSql, connection);

            command.Parameters.AddWithValue(
                customerProfileId);

            command.Parameters.AddWithValue(
                step.StepOrder);

            command.Parameters.AddWithValue(
                step.StepType);

            command.Parameters.AddWithValue(
                step.DurationMinutes);

            await command.ExecuteNonQueryAsync();
        }

        return customerProfileId;
    }


    public async Task<List<CustomerProfile>> GetCustomerProfilesAsync(
        int serviceProviderId)
    {
        var connectionString =
            configuration.GetConnectionString("Schedule") ?? "";

        await using var dataSource =
            NpgsqlDataSource.Create(connectionString);

        await using var connection =
            await dataSource.OpenConnectionAsync();


        const string sql = @"
            SELECT
                cp.customer_profile_id,
                cp.service_provider_id,
                cp.first_name,
                cp.phone_number,
                cp.service_type,
                s.customer_profile_step_id,
                s.step_order,
                s.step_type,
                s.duration_minutes
            FROM public.customer_profile cp
            LEFT JOIN public.customer_profile_step s
                ON cp.customer_profile_id = s.customer_profile_id
            WHERE cp.service_provider_id = $1
            ORDER BY
                cp.customer_profile_id,
                s.step_order;
        ";


        await using var command =
            new NpgsqlCommand(sql, connection);

        command.Parameters.AddWithValue(
            serviceProviderId);


        await using var results =
            await command.ExecuteReaderAsync();


        var customerProfiles =
            new List<CustomerProfile>();


        while (await results.ReadAsync())
        {
            int customerProfileId =
                results.GetInt32(0);


            var customerProfile =
                customerProfiles.FirstOrDefault(
                    c => c.Id == customerProfileId);


            if (customerProfile == null)
            {
                customerProfile = new CustomerProfile
                {
                    Id = customerProfileId,

                    ServiceProviderId =
                        results.GetInt32(1),

                    FirstName =
                        results.GetString(2),

                    PhoneNumber =
                        results.GetString(3),
                    ServiceType =
                        results.GetString(4)
                };

                customerProfiles.Add(
                    customerProfile);
            }


            if (!results.IsDBNull(4))
            {
                customerProfile.Steps.Add(
                    new CustomerProfileStep
                    {
                        Id =
                            results.GetInt32(5),

                        CustomerProfileId =
                            customerProfileId,

                        StepOrder =
                            results.GetInt32(6),

                        StepType =
                            results.GetString(7),

                        DurationMinutes =
                            results.GetInt32(8)
                    });
            }
        }


        return customerProfiles;
    }
}