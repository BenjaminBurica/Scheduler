namespace BusinessLogic.UseCases;

using BusinessLogic.Models;
using Scheduler.Entities;

public class GetUser
{
    public GetUserResponse Execute(GetUserRequest request)
    {
        return new GetUserResponse { User = new User { PhoneNumber = request.PhoneNumber } };
    }
}