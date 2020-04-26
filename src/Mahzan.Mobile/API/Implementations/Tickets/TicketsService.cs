using Mahzan.Mobile.API.Enums.Results;
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

namespace Mahzan.Mobile.API.Implementations.Tickets
{
    public class TicketsService : BaseService, ITicketsService
    {
        public TicketsService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository)
            : base(aspNetUsersRepository)
        {

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
