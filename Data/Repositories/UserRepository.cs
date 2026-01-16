using System;
using Entities;
using Microsoft.VisualBasic;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Runtime.CompilerServices;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IConfiguration configuration;

    public UserRepository(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public User? GetUserbyPhoneNumber(string? phoneNumber)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetUserbyPhoneNumberAsync(string? phoneNumber)
    {
        var connectionString = configuration.GetConnectionString("Schedule") ?? "";
        await using var dataSource = NpgsqlDataSource.Create(connectionString);
        var sql = @"
            select user_id, role_id, rist_name, last_name, phone_number, created_date, updated_date
            from users
            where phone_number = ($1)
        ";
        using var connection = await dataSource.OpenConnectionAsync();
        await using var cmd = new NpgsqlCommand(sql, connection)
        {
            Parameters =
            {
                new() { Value = phoneNumber}
            }
        };
        using var results = await cmd.ExecuteReaderAsync(System.Data.CommandBehavior.SingleResult);
        if (!results.HasRows)
        {
            return null;

        }
        await results.ReadAsync();
        var user = new User
        {
            UserId = results.GetInt32(0),
            Role = ReadRole(results, 1),
            FirstName = ReadString(results, 2),
            LastName = ReadString(results, 3),
            PhoneNumber = ReadString(results, 4),
            CreatedDate = ReadDateTime(results, 5),
            UpdatedDate = ReadDateTime(results, 6),

        };
        return user;
    }

    private Role ReadRole(DbDataReader reader, int colIndex)
    {
        var roleId = reader.GetInt32(colIndex);
        return (Role)roleId;
    }

    private string? ReadString(DbDataReader reader, int colIndex)
    {
        if (reader.IsDBNull(colIndex))
        {
            return null;

        }
        return reader.GetString(colIndex);
    }

    private DateTime? ReadDateTime(DbDataReader reader, int colIndex)
    {
        if (reader.IsDBNull(colIndex))
        {
            return null;
        }

        return reader.GetDateTime(colIndex);
    }
}



