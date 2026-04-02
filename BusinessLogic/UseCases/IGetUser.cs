namespace BusinessLogic.UseCases;

using BusinessLogic.Models;

public interface IGetUser
{
    Task<GetUserResponse> ExecuteAsync(GetUserRequest request);
}
