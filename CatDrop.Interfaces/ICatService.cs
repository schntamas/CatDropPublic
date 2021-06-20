using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDrop.Interfaces
{
  public interface  ICatService
  {
    Task<byte[]> GetFlippedCatAsync();
    Task<byte[]> GetCatAsync();
  }
}
