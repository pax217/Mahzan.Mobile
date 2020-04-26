using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.PaymentTypes;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.PaymentTypes;
using Mahzan.Mobile.API.Results.PaymentTypes;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.PaymentTypes
{
    public class PaymentTypesService : BaseService, IPaymentTypesService
    {
        public PaymentTypesService(
           IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
           ) : base(aspNetUsersRepository)
        {

        }

        public async Task<GetPaymentTypesResult> Get(GetPaymentTypesFilter getPaymentTypesFilter)
        {
            GetPaymentTypesResult result = new GetPaymentTypesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/PaymentTypes");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                //if (getPaymentTypesFilter.ProductCategoriesId != null)
                //{
                //    query["ProductCategoriesId"] = getProductCategoriesFilter.ProductCategoriesId.ToString();
                //}


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetPaymentTypesResult>(respuesta);
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
