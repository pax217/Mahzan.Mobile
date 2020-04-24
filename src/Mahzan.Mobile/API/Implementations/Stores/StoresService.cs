using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Requests.Stores;
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

        public async Task<AddStoresResult> Add(AddStoresRequest request)
        {
            AddStoresResult result = new AddStoresResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Stores");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<AddStoresResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<DeleteStoresResult> Delete(Guid storesId)
        {
            DeleteStoresResult result = new DeleteStoresResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Stores");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["StoresId"] = storesId.ToString();

                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<DeleteStoresResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GetStoresResult> Get(GetStoresFilter filter)
        {
            GetStoresResult result = new GetStoresResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Stores");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                if (filter.StoresId!=null)
                {
                    query["StoresId"] = filter.StoresId.ToString();
                }


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

        public async Task<PutStoresResult> Update(PutStoresRequest request)
        {
            PutStoresResult result = new PutStoresResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Stores");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PutStoresResult>(respuesta);
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
