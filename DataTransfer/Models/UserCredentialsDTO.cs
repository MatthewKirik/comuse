using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer.Models
{
    public class UserCredentialsDTO
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}
