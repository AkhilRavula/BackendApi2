using BackendApi2.Entities.Models;

namespace BackendApi2.Contracts;
public interface IUserRepository
{
    void RegisterUser(string username, string password,string role);

    Task<User> LoginUser(string username,string password);

    Task<bool> CheckUsernameExists(string username);
}

