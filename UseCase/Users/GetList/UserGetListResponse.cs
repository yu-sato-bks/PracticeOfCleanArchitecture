using Lib;

namespace UseCase;

public class UserGetListResponse : IResponse
{
    public UserGetListResponse(List<UserSummary> summaries)
    {
        Summaries = summaries;
    }

    public List<UserSummary> Summaries { get; }
}
