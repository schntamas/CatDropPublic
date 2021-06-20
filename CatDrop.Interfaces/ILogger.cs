using CatDrop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDrop.Interfaces
{
  public interface ILogger
  {

    void Error(string errorMessage, Exception exception);
    void Activity(string message, string userName);
  }
}
