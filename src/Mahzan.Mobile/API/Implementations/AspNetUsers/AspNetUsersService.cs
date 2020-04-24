using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.AspNetUsers;
using Mahzan.Mobile.API.Requests.AspNetUsers;
using Mahzan.Mobile.API.Results.AspNetUsers;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.AspNetUsers
{
    public class AspNetUsersService : BaseService,IAspNetUsersService
    {
        public AspNetUsersService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
            ) : base(aspNetUsersRepository) 
        {
        
        }

        public async Task<LogInResult> LogIn(LogInRequest logInRequest)
        {
            LogInResult result = new LogInResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/AspNetUsers/LogIn");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["UserName"] = logInRequest.UserName;
                query["Password"] = logInRequest.Password;
                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();
                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<LogInResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }
    }
}
