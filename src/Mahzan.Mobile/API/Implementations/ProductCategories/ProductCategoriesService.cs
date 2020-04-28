using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.ProductCategories;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.ProductCategories;
using Mahzan.Mobile.API.Requests.ProductCategories;
using Mahzan.Mobile.API.Results.ProductCategories;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.ProductCategories
{
    public class ProductCategoriesService : BaseService, IProductCategoriesService
    {
        public ProductCategoriesService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
            ) : base(aspNetUsersRepository)
        {

        }

        public async Task<PostProductCategoriesResult> Add(PostProductCategoriesRequest request)
        {
            PostProductCategoriesResult result = new PostProductCategoriesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/ProductCategories");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PostProductCategoriesResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<DeleteProductCategoriesResult> Delete(Guid productCategoriesId)
        {
            DeleteProductCategoriesResult result = new DeleteProductCategoriesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/ProductCategories");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                query["ProductCategoriesId"] = productCategoriesId.ToString();

                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.DeleteAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<DeleteProductCategoriesResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<GetProductCategoriesResult> Get(GetProductCategoriesFilter getProductCategoriesFilter)
        {
            GetProductCategoriesResult result = new GetProductCategoriesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/ProductCategories");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                if (getProductCategoriesFilter.ProductCategoriesId != null)
                {
                    query["ProductCategoriesId"] = getProductCategoriesFilter.ProductCategoriesId.ToString();
                }


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetProductCategoriesResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<PutProductCategoriesResult> Put(PutProductCategoriesRequest request)
        {
            PutProductCategoriesResult result = new PutProductCategoriesResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/ProductCategories");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(request);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PutAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<PutProductCategoriesResult>(respuesta);
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
