using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDrop.Interfaces
{
  public interface ICatHttpClient
  { 
    Task<Stream> GetStreamAsync(string endPoint);
  }
}
