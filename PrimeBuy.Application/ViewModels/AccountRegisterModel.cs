using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrimeBuy.Application.ViewModels
{
    public class AccountRegisterModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string CPF { get; set; }
        public DateTime Birthdate { get; set; }
        public string Email { get; set; }
    }
}