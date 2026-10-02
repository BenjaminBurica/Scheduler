namespace Data.Repositories;
using Entities;
using Npgsql;
using System;
using Microsoft.VisualBasic;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using System.Runtime.CompilerServices;

public class ScheduleRepository : IScheduleRepository
{
    private readonly IConfiguration configuration;

    public ScheduleRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
public async Task SaveScheduleAsync(int id, List<StoreSchedule> schedule)
{
    var connectionString = configuration.GetConnectionString("Schedule") ?? "";
    await using var dataSource = NpgsqlDataSource.Create(connectionString);

    const string sql = @"
        INSERT INTO public.store_schedule
        (
            service_provider_id,
            day_of_week,
            open_time,
            closed_time,
            closed
        )
        VALUES
        (
            ($1), ($2), ($3), ($4), ($5)
        )
        ON CONFLICT (service_provider_id, day_of_week)
        DO UPDATE SET
            open_time = EXCLUDED.open_time,
            closed_time = EXCLUDED.closed_time,
            closed = EXCLUDED.closed,
            updated_date = NOW();
    ";

    using var connection = await dataSource.OpenConnectionAsync();

    for (int dayOfWeek = 0; dayOfWeek < schedule.Count; dayOfWeek++)
    {
        var day = schedule[dayOfWeek];

        await using var cmd = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new() { Value = id },
                new() { Value = dayOfWeek },
                new() { Value = (object?)day.Start ?? DBNull.Value },
                new() { Value = (object?)day.End ?? DBNull.Value },
                new() { Value = day.Closed ?? false }
            }
        };

        await cmd.ExecuteNonQueryAsync();
    }
}

    public async Task<List<StoreSchedule>> GetScheduleAsync(int id)
    {
        var connectionString = configuration.GetConnectionString("Schedule") ?? "";
        await using var dataSource = NpgsqlDataSource.Create(connectionString);
        var sql = @"SELECT
            ss.day_of_week,
            ss.open_time,
            ss.closed_time,
            ss.closed
            FROM public.store_schedule ss
            WHERE ss.service_provider_id = ($1)
            ORDER BY ss.day_of_week;";
        using var connection = await dataSource.OpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new() { Value = id }
            }
        };

        using var results = await cmd.ExecuteReaderAsync();
        var schedule = new List<StoreSchedule>();
        while (await results.ReadAsync())
        {
            var day = new StoreSchedule
            {
                Start = results.IsDBNull(1) ? null : results.GetFieldValue<TimeOnly>(1),
                End = results.IsDBNull(2) ? null : results.GetFieldValue<TimeOnly>(2),
                Closed = results.GetBoolean(3)
            };
            schedule.Add(day);

            }
            return schedule;
        }
}