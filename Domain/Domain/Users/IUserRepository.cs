namespace Domain.Domain.Users;
public interface IUserRepository
{
    void Save(User user);
    User FindByUserName(string username);
    IEnumerable<User> FindAll();
}
