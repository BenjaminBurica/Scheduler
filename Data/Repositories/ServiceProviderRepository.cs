namespace Data.Repositories;

using Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;

public class ServiceProviderRepository : IServiceProviderRepository
{
    private readonly IConfiguration configuration;

    public ServiceProviderRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task<ServiceProvider?> GetAsync(int id)
    {
        var connectionString = configuration.GetConnectionString("Schedule") ?? "";

        await using var dataSource = NpgsqlDataSource.Create(connectionString);
        using var connection = await dataSource.OpenConnectionAsync();

        var sql = @"
            SELECT 
                sp.service_provider_id,
                sp.business_name,
                ss.day_of_week,
                ss.open_time,
                ss.closed_time,
                ss.closed
            FROM public.service_provider sp
            LEFT JOIN public.store_schedule ss
                ON sp.service_provider_id = ss.service_provider_id
            WHERE sp.service_provider_id = ($1)
            ORDER BY ss.day_of_week;
        ";

        await using var cmd = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new() { Value = id }
            }
        };

        using var results = await cmd.ExecuteReaderAsync();

        if (!results.HasRows)
        {
            return null;
        }

        var serviceProvider = new Entities.ServiceProvider
        {
            Id = id,
            BusinessName = results.IsDBNull(1) ? null : results.GetString(1),
            Schedule = new StoreSchedule[7]
        };

        while (await results.ReadAsync())
        {
            int dayOfWeek = results.GetInt32(2);

            serviceProvider.Schedule[dayOfWeek] = new StoreSchedule
            {
                Start = results.IsDBNull(3) 
                    ? null 
                    : results.GetFieldValue<TimeOnly>(3),

                End = results.IsDBNull(4)
                    ? null
                    : results.GetFieldValue<TimeOnly>(4),

                Closed = results.GetBoolean(5)
            };
        }

        return serviceProvider;
    }
}