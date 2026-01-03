using System;
using Entities;
using Microsoft.VisualBasic;

namespace Data.Repositories;

public class UserRepository : IUserRepository
{
    private List<User> userList = new List<User> {
    new User { UserId = 1, PhoneNumber = "123-456-7890", FirstName = "John", LastName = "Doe", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Role = Role.Customer },
    new User { UserId = 2, PhoneNumber = "949-867-5309", FirstName = "Jane", LastName = "Smith", CreatedDate = DateTime.Now, UpdatedDate = DateTime.Now, Role = Role.ServiceProvider }
    };
    public User? GetUserbyPhoneNumber(string? phoneNumber)
    {
        foreach (var user in userList)
        {
            if (phoneNumber == user.PhoneNumber)
            {
                return user;
            }
        }
        return null;
    }

}



