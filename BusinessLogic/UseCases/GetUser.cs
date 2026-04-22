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
    public async Task<GetUserResponse> ExecuteAsync(GetUserRequest request)
    {
        var user = await userRepository.GetUserbyPhoneNumberAsync(request.PhoneNumber);
        return new GetUserResponse { User = user };
    }
}