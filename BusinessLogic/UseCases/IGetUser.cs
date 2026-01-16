namespace BusinessLogic.UseCases;

using BusinessLogic.Models;

public interface IGetUser
{
    GetUserResponse Execute(GetUserRequest request);
}
