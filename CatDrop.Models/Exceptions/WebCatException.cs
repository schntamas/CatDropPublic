using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDrop.Models.Exceptions
{
  public class WebCatException: Exception
  {
    public WebCatException(string message, Exception exception): base(message, exception)
    {

    }
  }
}
