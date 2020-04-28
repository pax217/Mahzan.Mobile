using Mahzan.Mobile.API.Filters.ProductUnits;
using Mahzan.Mobile.API.Interfaces.ProductUnits;
using Mahzan.Mobile.API.Requests.ProductUnits;
using Mahzan.Mobile.API.Results.ProductUnits;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Products.Units
{
    public class AdminUnitsPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IProductUnitsService _productUnitsService;

        private Guid ProductUnitsId { get; set; }

        //Abbreviation
        private string _abbreviation;
        public string Abbreviation
        {
            get { return _abbreviation; }
            set
            {
                _abbreviation = value;
                OnPropertyChanged(nameof(Abbreviation)); // Notify that there was a change on this property
            }
        }

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

        public AdminUnitsPageViewModel(
            INavigationService navigationService,
            IProductUnitsService productUnitsService)
        {
            //Services
            _navigationService = navigationService;
            _productUnitsService = productUnitsService;

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
                                 "¿Estas seguro de borrar la Unidad de Venta?", "Si", "No");
            if (answer)
            {
                DeleteProductUnitsResult result = await _productUnitsService
                                                        .Delete(ProductUnitsId);

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
            if (ProductUnitsId == Guid.Empty)
            {
                PostProductUnitsResult result = await _productUnitsService
                                                        .Post(new PostProductUnitsRequest 
                                                        {
                                                            Abbreviation = Abbreviation,
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
                PutProductUnitsResult result = await _productUnitsService
                                                          .Put(new PutProductUnitsRequest
                                                          {
                                                              ProductUnitsId = ProductUnitsId,
                                                              Abbreviation = Abbreviation,
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
            ProductUnitsId = parameters.GetValue<Guid>("productUnitsId");
            if (ProductUnitsId != Guid.Empty)
            {
                await GetProductUnits(ProductUnitsId);
            }
        }

        private async Task GetProductUnits(Guid productUnitsId)
        {
            GetProductUnitsResult result = await _productUnitsService
                                                  .Get(new GetProductUnitsFilter
                                                  {
                                                      ProductUnitsId = productUnitsId
                                                  });
            if (result.IsValid)
            {
                Abbreviation = result.ProductUnits.FirstOrDefault().Abbreviation;
                Description = result.ProductUnits.FirstOrDefault().Description;
            }
        }
    }
}
