using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.Companies;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.Companies;
using Mahzan.Mobile.API.Results.Companies;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.Companies
{
    public class CompaniesService: BaseService,ICompaniesService
    {
        public CompaniesService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository)
            : base(aspNetUsersRepository)
        {

        }

        public async Task<GetCompaniesResult> Get(GetCompaniesFilter filter)
        {
            GetCompaniesResult result = new GetCompaniesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Companies");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                //query["EmployeesId"] = filter.EmployeesId.ToString().ToUpper();
                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetCompaniesResult>(respuesta);
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
