using Lib;

namespace UseCase;

public class UserCreateRequest : IRequest<UserCreateResponse>
{
    public UserCreateRequest(string userName)
    {
        UserName = userName;
    }

    public string UserName { get; }
}
