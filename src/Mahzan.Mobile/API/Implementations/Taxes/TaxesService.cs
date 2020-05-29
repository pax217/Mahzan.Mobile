using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.Taxes;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.Taxes;
using Mahzan.Mobile.API.Requests.Taxes;
using Mahzan.Mobile.API.Results.Taxes;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.Taxes
{
    public class TaxesService : BaseService,ITaxesService
    {
        public TaxesService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository)
            : base(aspNetUsersRepository)
        {

        }
        public async Task<GetTaxesResult> GetWhere(GetTaxesFilter filter)
        {
            GetTaxesResult result = new GetTaxesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Taxes");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetTaxesResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<CreateTaxResult> CreateTax(CreateTaxCommand command)
        {
            CreateTaxResult result = new CreateTaxResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Taxes/create");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(command);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<CreateTaxResult>(respuesta);
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
