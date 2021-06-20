using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatDrop.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime Registered { get; set; }
        public int ServiceCount { get; set; }


  }
}
