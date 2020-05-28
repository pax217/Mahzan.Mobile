using Mahzan.Mobile.API.Commands.Products.CreateProduct;
using Mahzan.Mobile.API.Enums.Results;
using Mahzan.Mobile.API.Filters.Products;
using Mahzan.Mobile.API.Implementations._Base;
using Mahzan.Mobile.API.Interfaces.Products;
using Mahzan.Mobile.API.Requests.Products.Post;
using Mahzan.Mobile.API.Results.Products;
using Mahzan.Mobile.SqLite.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mahzan.Mobile.API.Implementations.Products
{
    public class ProductsService:BaseService, IProductsService
    {
        public ProductsService(
            IRepository<SqLite.Entities.AspNetUsers> aspNetUsersRepository
            ) : base(aspNetUsersRepository)
        {

        }

        public async Task<GetProductsResult> Get(GetProductsFilter getProductsFilter)
        {
            GetProductsResult result = new GetProductsResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Products");

            try
            {
                var query = HttpUtility.ParseQueryString(uriBuilder.Query);

                if (getProductsFilter.ProductsId != null)
                {
                    query["ProductsId"] = getProductsFilter.ProductsId.ToString();
                }

                if (getProductsFilter.Barcode!=null)
                {
                    query["Barcode"] = getProductsFilter.Barcode;
                }


                uriBuilder.Query = query.ToString();

                HttpClient httpClient = new HttpClient();


                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(uriBuilder.ToString());

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<GetProductsResult>(respuesta);
            }
            catch (Exception ex)
            {
                result.IsValid = false;
                result.ResultTypeEnum = ResultTypeEnum.ERROR;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<CreateProductResult> CreateProduct(CreateProductCommand command)
        {
            CreateProductResult result = new CreateProductResult();
            UriBuilder uriBuilder = new UriBuilder(URL_API + "/v1/Products/create");
            try
            {
                HttpClient httpClient = new HttpClient();

                string jsonData = JsonConvert.SerializeObject(command);
                StringContent stringContent = new StringContent(jsonData, UnicodeEncoding.UTF8, "application/json");

                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TOKEN);

                HttpResponseMessage httpResponseMessage = await httpClient.PostAsync(uriBuilder.ToString(), stringContent);

                var respuesta = await httpResponseMessage.Content.ReadAsStringAsync();

                result = JsonConvert.DeserializeObject<CreateProductResult>(respuesta);
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
