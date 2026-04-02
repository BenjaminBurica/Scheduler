using System;
using System.Reflection.Metadata;
using Entities;
using System.Data.Common;
namespace Data.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserbyPhoneNumberAsync (string? phoneNumber);
}
