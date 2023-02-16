using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bookstore.API.Dtos
{
    public class UserDto
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public string Token { get; set; }
    }
}
