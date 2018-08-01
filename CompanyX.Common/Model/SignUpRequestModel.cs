using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyX.Common
{
    public class SignUpRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
