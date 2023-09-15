using System.Security.Cryptography;
using System.Text;
using BackendApi2.Contracts;
using BackendApi2.Entities;
using BackendApi2.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApi2.Repository;
public class UserRepository : RepositoryBase<User>, IUserRepository
{
    public UserRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {

    }
    public async Task<bool> CheckUsernameExists(string username)
    {
        return await CheckExists(x=>x.Username.Equals(username));
    }

    public async Task<User> LoginUser(string username, string password)
    {
        User? usercred = await FindByCondition(x=>x.Username.Equals(username)).FirstOrDefaultAsync();

        if (usercred == null)
        {
            return null;
        }
        else
        {
          
          if(!matchpasswords(usercred.PasswordKey,usercred.Password,password))
          {
            return null;
          } 
          else
             return usercred;
          
        }

        
    }

    private bool matchpasswords(byte[] passwordkey,byte[] password,string passwordtext)
    {
         using(var hmac =  new HMACSHA512(passwordkey))
        {
            var _PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(passwordtext)); 

            for (int i = 0; i < _PasswordHash.Length; i++)
            {
                if(_PasswordHash[i] != password[i])
                  return false;
            }
        };
        return true;
    }


    public void RegisterUser(string username, string password,string role)
    {
        byte[] _PasswordHash ,_PasswordKey;

        using(var hmac =  new HMACSHA512())
        {
            _PasswordKey = hmac.Key;
            _PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)); 
        };

        var user =  new User()
        {
            Password = _PasswordHash,
            PasswordKey = _PasswordKey,
            Username = username,
            Role =  role
        };
      
         Create(user);     
          
    }
}
