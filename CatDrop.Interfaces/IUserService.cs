using CatDrop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDrop.Interfaces
{
  public interface IUserService
  {
    bool Validate(string userName, string password);
    void AddUser(string userName, string password);
    User GetUser(string userName);
    void ServiceUsed(string userName);
  }
}
