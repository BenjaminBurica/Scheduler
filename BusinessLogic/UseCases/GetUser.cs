namespace BusinessLogic.UseCases;

using BusinessLogic.Models;
using Entities;
using Data.Repositories;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

public class GetUser : IGetUser
{
    private readonly IUserRepository userRepository;
    public GetUser(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }
    public GetUserResponse Execute(GetUserRequest request)
    {
        var user = userRepository.GetUserbyPhoneNumber(request.PhoneNumber);
        return new GetUserResponse { User = user };
    }
}