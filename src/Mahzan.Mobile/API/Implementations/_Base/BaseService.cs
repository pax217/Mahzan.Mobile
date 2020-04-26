using Mahzan.Mobile.SqLite.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Implementations._Base
{
    public class BaseService
    {
        private readonly IRepository<SqLite.Entities.AspNetUsers> _aspNetUsersRepository;

        //public static string URL_API = "http://localhost:5000";
        public static string URL_API = "http://localhost:6472";
        public static string TOKEN { get; set; }

        public BaseService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository) 
        {
            _aspNetUsersRepository = aspNetUsersRepository;

            Task.Run(() => GetToken());
        }

        private async Task GetToken() 
        {
            List<SqLite.Entities.AspNetUsers> aspNetUsers;
            aspNetUsers = await _aspNetUsersRepository.Get();

            TOKEN = aspNetUsers.FirstOrDefault().Token;
        }
    }
}
