using Lib;

namespace UseCase;

public class UserCreateResponse : IResponse
{
    public UserCreateResponse(string userId)
    {
        UserId = userId;
    }

    public string UserId { get; }
}
