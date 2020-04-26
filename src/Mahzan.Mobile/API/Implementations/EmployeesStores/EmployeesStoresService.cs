using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.EmployeesStores;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.EmployeesStores;
using Mahzan.Mobile.API.Results.EmployeesStores;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.EmployeesStores
{
    public class EmployeesStoresService : BaseService, IEmployeesStoresService
    {
        public EmployeesStoresService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
            ) : base(aspNetUsersRepository)
        {

        }
        public async Task<GetEmployeesStoresResult> Get(GetEmployeesStoresFilter getEmployeesStoresFilter)
        {
            GetEmployeesStoresResult result = new GetEmployeesStoresResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/EmployeesStores");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                if (getEmployeesStoresFilter.EmployeesId!=null)
                {
                    query["EmployeesId"] = getEmployeesStoresFilter.EmployeesId.ToString().ToUpper();

                }

                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetEmployeesStoresResult>(respuesta);
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
