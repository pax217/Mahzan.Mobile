using Mahzan.Mobile.API.Interfaces.Taxes;
using Mahzan.Mobile.API.Requests.Taxes;
using Mahzan.Mobile.API.Results.Taxes;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Products.Taxes
{
    public class AdminTaxesPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationAware _navigationAware;

        private readonly IPageDialogService _pageDialogService;

        private readonly ITaxesService _taxesService;

        public AdminTaxesPageViewModel(
            INavigationAware navigationAware,
            IPageDialogService pageDialogService,
            ITaxesService taxesService)
        {
            _navigationAware = navigationAware;
            _pageDialogService = pageDialogService;
            _taxesService = taxesService;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private decimal? _taxRatePercentage;
        public decimal? TaxRatePercentage
        {
            get { return _taxRatePercentage; }
            set { SetProperty(ref _taxRatePercentage, value); }
        }

        private bool _taxRateVariable;
        public bool TaxRateVariable
        {
            get { return _taxRateVariable; }
            set { SetProperty(ref _taxRateVariable, value); }
        }

        private bool _active;
        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref _active, value); }
        }

        private bool _printed;
        public bool Printed
        {
            get { return _printed; }
            set { SetProperty(ref _printed, value); }
        }

        public ICommand SaveCommand { get; set; }

        public AdminTaxesPageViewModel() 
        {
            SaveCommand = new Command(async () => await OnSaveCommand());
        }

        private async Task OnSaveCommand()
        {
            PostTaxesRequest request = new PostTaxesRequest
            {
                Name = Name,
                TaxRatePercentage = TaxRatePercentage.Value,
                TaxRateVariable = TaxRateVariable,
                Active = Active,
                Printed = Printed
            };

            PostTaxesResult result = await _taxesService.Post(request);

            await _pageDialogService
                .DisplayAlertAsync(
                result.Title,
                result.Message,
                "OK");

        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {

        }

    }
}
