using Mahzan.Mobile.API.Filters.ProductCategories;
using Mahzan.Mobile.API.Interfaces.ProductCategories;
using Mahzan.Mobile.API.Requests.ProductCategories;
using Mahzan.Mobile.API.Results.ProductCategories;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Products.Categories
{
    public class AdminCategoriesPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IProductCategoriesService _productCategoriesService;

        public Guid ProductCategoriesId { get; set; }

        //Description
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description)); // Notify that there was a change on this property
            }
        }

        //Commands
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        public AdminCategoriesPageViewModel(
            INavigationService navigationService,
            IProductCategoriesService productCategoriesService)
        {
            //Services
            _navigationService = navigationService;
            _productCategoriesService = productCategoriesService;

            //Commands
            SaveCommand = new Command(async () => await OnSaveCommand());
            DeleteCommand = new Command(async () => await OnDeleteCommand());

        }

        private async Task OnDeleteCommand()
        {

            var answer = await Application
                   .Current
                   .MainPage
                   .DisplayAlert("Atención!",
                                 "¿Estas seguro de borrar la Categoría?", "Si", "No");
            if (answer)
            {
                DeleteProductCategoriesResult result = await _productCategoriesService
                                                             .Delete(ProductCategoriesId);

                if (result.IsValid)
                {
                    Description = string.Empty;
                }

                await Application
                        .Current
                        .MainPage
                        .DisplayAlert(result.Title,
                                      result.Message, "ok");
            }



        }

        private async Task OnSaveCommand()
        {
            if (ProductCategoriesId == Guid.Empty)
            {
                PostProductCategoriesResult result = await _productCategoriesService
                                                          .Add(new PostProductCategoriesRequest
                                                          {
                                                              Description = Description
                                                          });
                await Application
                        .Current
                        .MainPage
                        .DisplayAlert(result.Title,
                                      result.Message, "ok");
            }
            else 
            {
                PutProductCategoriesResult result = await _productCategoriesService
                                                          .Put(new PutProductCategoriesRequest
                                                          {
                                                              ProductCategoriesId = ProductCategoriesId,
                                                              Description = Description
                                                          });
                await Application
                        .Current
                        .MainPage
                        .DisplayAlert(result.Title,
                                      result.Message, "ok");
            }

        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            ProductCategoriesId = parameters.GetValue<Guid>("productCategoriesId");
            if (ProductCategoriesId != Guid.Empty)
            {
                await GetProductCategories(ProductCategoriesId);
            }
        }

        private async Task GetProductCategories(Guid productCategoriesId)
        {
            GetProductCategoriesResult result = await _productCategoriesService
                                                      .Get(new GetProductCategoriesFilter
                                                      {
                                                          ProductCategoriesId = productCategoriesId
                                                      });
            if (result.IsValid)
            {
                Description = result.ProductCategories.FirstOrDefault().Description;
            }
            else{

                await Application
                        .Current
                        .MainPage
                        .DisplayAlert(result.Title,
                                      result.Message, "ok");
            }
        }
    }
}
