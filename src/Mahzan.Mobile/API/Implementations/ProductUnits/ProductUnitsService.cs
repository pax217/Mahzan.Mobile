using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.ProductUnits;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.ProductUnits;
using Mahzan.Mobile.API.Results.ProductUnits;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.ProductUnits
{
    public class ProductUnitsService : BaseService, IProductUnitsService
    {
        public ProductUnitsService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
            ) : base(aspNetUsersRepository)
        {

        }
        public async Task<GetProductUnitsResult> Get(GetProductUnitsFilter getProductUnitsFilter)
        {
            GetProductUnitsResult result = new GetProductUnitsResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/ProductUnits");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                if (getProductUnitsFilter.ProductUnitsId != null)
                {
                    query["ProductUnitsId"] = getProductUnitsFilter.ProductUnitsId.ToString();
                }


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetProductUnitsResult>(respuesta);
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
