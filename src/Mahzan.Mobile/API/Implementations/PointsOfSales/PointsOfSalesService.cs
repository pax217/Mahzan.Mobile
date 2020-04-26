using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.PointsOfSales;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.PointsOfSales;
using Mahzan.Mobile.API.Results.PointsOfSales;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.PointsOfSales
{
    public class PointsOfSalesService : BaseService, IPointsOfSalesService
    {
        public PointsOfSalesService(
           IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
           ) : base(aspNetUsersRepository)
        {

        }
        public async Task<GetPointsOfSalesResult> Get(GetPointsOfSalesFilter getPointsOfSalesFilter)
        {
            GetPointsOfSalesResult result = new GetPointsOfSalesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/PointsOfSales");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                query["StoresId"] = getPointsOfSalesFilter.StoresId.ToString();
                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetPointsOfSalesResult>(respuesta);
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
