using CatDrop.Interfaces;
using CatDrop.Models;
using CatDrop.Models.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDrop.Services
{
  public class UserService : IUserService
  {

    private ILogger _logger;

    public UserService(ILogger logger)
    {
      _logger = logger;
    }

    //TODO: the way to store the users in a static list is not substainable. DB based user management will be required.
    public IEnumerable<User> GetUsers()
    {
      return Users;
    }

    public void AddUser(string userName, string password)
    {
      if (String.IsNullOrEmpty(userName) || String.IsNullOrEmpty(password))
      {
        throw new UserException("User name and password can not be empty.");
      }

      if (Users.Any(u => u.UserName == userName))
      {
        throw new UserException("User name already used.");
      }

      Users.Add(new User() { UserName = userName, Password = password, Registered = DateTime.Now, ServiceCount = 0 });

    }

    public bool Validate(string userName, string password)
    {
      return Users.Any(u => u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase) && u.Password.Equals(password));
    }

    public void ServiceUsed(string userName)
    {
      var user = Users.Where(u => u.UserName == userName).FirstOrDefault();

      if (user != null)
      {
        user.ServiceCount++;
      }
    }


    public User GetUser(string userName)
    {
      return Users.Where(u => u.UserName == userName).FirstOrDefault();
    }

    private static List<User> Users = new List<User>
        {
                new User{UserName="User1", Password="Password01!", Registered = DateTime.Now, ServiceCount =0},
                new User{UserName="User2", Password="Password01!", Registered = DateTime.Now, ServiceCount =0},
                new User{UserName="User3", Password="Password01!", Registered = DateTime.Now, ServiceCount =0},
                new User{UserName="User4", Password="Password01!", Registered = DateTime.Now, ServiceCount =0},
                new User{UserName="User5", Password="Password01!", Registered = DateTime.Now, ServiceCount =0}
        };

  }
}
