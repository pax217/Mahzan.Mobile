using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.ProductsStore;
using Mahzan.Mobile.API.Requests.ProductsStore;
using Mahzan.Mobile.API.Results.ProductsStore;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Implementations.ProductsStore
{
    public class ProductsStoreService :BaseService, IProductsStoreService
    {
        public ProductsStoreService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
            ) : base(aspNetUsersRepository)
        {

        }

        public async Task<PostProductsStoreResult> Add(PostProductsStoreRequest postProductsStoreRequest)
        {
            PostProductsStoreResult result = new PostProductsStoreResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/ProductsStore");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(postProductsStoreRequest);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PostProductsStoreResult>(respuesta);
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
