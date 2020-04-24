using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Results.Stores;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.Stores
{
    public class StoresService : BaseService,IStoresService
    {
        public StoresService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository)
            :base(aspNetUsersRepository)
        { 
        
        }

        public async Task<GetStoresResult> Get(GetStoresFilter filter)
        {
            GetStoresResult result = new GetStoresResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Stores");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                //query["EmployeesId"] = filter.EmployeesId.ToString().ToUpper();
                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetStoresResult>(respuesta);
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
