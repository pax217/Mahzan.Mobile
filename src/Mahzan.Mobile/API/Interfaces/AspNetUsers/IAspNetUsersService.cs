using Mahzan.Mobile.API.Requests.AspNetUsers;
using Mahzan.Mobile.API.Results.AspNetUsers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.AspNetUsers
{
    public interface IAspNetUsersService
    {
        Task<LogInResult> LogIn(LogInRequest logInRequest);
    }
}
