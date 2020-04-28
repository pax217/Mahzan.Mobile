using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.PointsOfSales;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.PointsOfSales;
using Mahzan.Mobile.API.Requests.PointsOfSales;
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

        public async Task<DeletePointsOfSalesResult> Delete(Guid pointsOfSalesId)
        {
            DeletePointsOfSalesResult result = new DeletePointsOfSalesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/PointsOfSales");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["PointsOfSalesId"] = pointsOfSalesId.ToString();

                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<DeletePointsOfSalesResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GetPointsOfSalesResult> Get(GetPointsOfSalesFilter getPointsOfSalesFilter)
        {
            GetPointsOfSalesResult result = new GetPointsOfSalesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/PointsOfSales");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                if (getPointsOfSalesFilter.StoresId!=null)
                {
                    query["StoresId"] = getPointsOfSalesFilter.StoresId.ToString();

                }

                if (getPointsOfSalesFilter.PointsOfSales != null)
                {
                    query["PointsOfSalesId"] = getPointsOfSalesFilter.PointsOfSales.ToString();

                }
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

        public async Task<PostPointOfSalesResult> Post(PostPointOfSalesRequest request)
        {
            PostPointOfSalesResult result = new PostPointOfSalesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/PointsOfSales");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PostPointOfSalesResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<PutPointsOfSalesResult> Put(PutPointsOfSalesRequest request)
        {
            PutPointsOfSalesResult result = new PutPointsOfSalesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/PointsOfSales");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PutPointsOfSalesResult>(respuesta);
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
