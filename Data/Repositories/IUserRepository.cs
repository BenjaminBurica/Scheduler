using System;
using System.Reflection.Metadata;
using Entities;
namespace Data.Repositories;

public interface IUserRepository
{
    User? GetUserbyPhoneNumber (string phoneNumber);
}
