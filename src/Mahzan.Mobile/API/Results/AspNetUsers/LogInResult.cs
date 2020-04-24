using Mahzan.Mobile.API.Results._Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results.AspNetUsers
{
    public class LogInResult : Result
    {
        public string Token { get; set; }

        public string Role { get; set; }

        public Guid AspNetUsersId { get; set; }

        public string UserName { get; set; }

        public Guid EmployeesId { get; set; }
    }
}
