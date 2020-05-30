using ImTools;
using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.Tickets;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Requests.Tickets;
using Mahzan.Mobile.API.Results.Tickets;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.Tickets
{
    public class TicketsService : BaseService, ITicketsService
    {
        public TicketsService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository)
            : base(aspNetUsersRepository)
        {

        }

 

        public async Task<GetTicketsResult> Get(GetTicketsFilter filter)
        {
            GetTicketsResult result = new GetTicketsResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Tickets");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                if (filter.CreatedAt!=null)
                {
                    query["CreatedAt"] = filter.CreatedAt.Value.ToString("yyyy-MM-dd");
                }

                if (filter.TicketsId != null)
                {
                    query["TicketsId"] = filter.TicketsId.ToString();
                }


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetTicketsResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GetTicketResult> GetById(Guid ticketsId)
        {
            GetTicketResult result = new GetTicketResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Tickets/" + ticketsId.ToString());

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                //if (filter.CreatedAt != null)
                //{
                //    query["CreatedAt"] = filter.CreatedAt.Value.Date.ToString();
                //}


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetTicketResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GetTicketToPrintResult> GetTicketToPrint(Guid ticketsId)
        {
            GetTicketToPrintResult result = new GetTicketToPrintResult();

            StringBuilder uri = new StringBuilder();
            uri.Append(URL_API);
            uri.Append("/v1/Tickets/" + ticketsId.ToString());
            uri.Append("/get-ticket-to-print");

            UriBuilder uriBuilder = new UriBuilder(uri.ToString());

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                //if (filter.CreatedAt != null)
                //{
                //    query["CreatedAt"] = filter.CreatedAt.Value.Date.ToString();
                //}


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetTicketToPrintResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<PostTicketCalculationResult> TicketCalculation(PostTicketCalculationRequest postTicketCalculationRequest)
        {
            PostTicketCalculationResult result = new PostTicketCalculationResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Tickets/ticket-calculation");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(postTicketCalculationRequest);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PostTicketCalculationResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<PostTicketCloseSaleResult> TicketCloseSale(PostTicketCalculationRequest postTicketCalculationRequest)
        {
            {
                PostTicketCloseSaleResult result = new PostTicketCloseSaleResult();
                UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Tickets/ticket-close-sale");
                try
                {
                    HttpClient httpClient = new HttpClient();

                    string jsonData = JsonConvert.SerializeObject(postTicketCalculationRequest);
                    StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                    HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                    var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                    result = JsonConvert.DeserializeObject<PostTicketCloseSaleResult>(respuesta);
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
}
