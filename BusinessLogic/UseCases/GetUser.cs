namespace BusinessLogic.UseCases;

using BusinessLogic.Models;
using Entities;
using Data.Repositories;
using System.Reflection.Metadata;

public class GetUser
{
    public GetUserResponse Execute(GetUserRequest request)
    {
        var repo = new UserRepository();
        var user = repo.GetUserbyPhoneNumber(request.PhoneNumber);
        return new GetUserResponse { User = user };
    }
}