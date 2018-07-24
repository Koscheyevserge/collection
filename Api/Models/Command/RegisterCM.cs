using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Command
{
    public class RegisterCM : BaseСM
    {
        [MinLength(6)]
        public string Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
