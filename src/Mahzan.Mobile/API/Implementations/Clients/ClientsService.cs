using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.Clients;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.Clients;
using Mahzan.Mobile.API.Requests.Clients;
using Mahzan.Mobile.API.Results.Clients;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.Clients
{
    public class ClientsService : BaseService, IClientsService
    {
        public ClientsService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository)
            : base(aspNetUsersRepository)
        {

        }

        public async Task<GetClientsResult> Get(GetClientsFilter filter)
        {
            GetClientsResult result = new GetClientsResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Clients");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                if (filter.ClientsId != null )
                {
                    query["ClientsId"] = filter.ClientsId.ToString();
                }

                if (filter.RFC != null)
                {
                    query["RFC"] = filter.RFC;
                }

                if (filter.BusinessName != null)
                {
                    query["BusinessName"] = filter.BusinessName;
                }


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetClientsResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<PostClientsResult> Post(PostClientsRequest request)
        {
            PostClientsResult result = new PostClientsResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Clients");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PostClientsResult>(respuesta);
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
