using Mahzan.Mobile.API.Enums.Taxes;
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

        private string _typeTax;
        public string TypeTax
        {
            get { return _typeTax; }
            set { SetProperty(ref _typeTax, value); }
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

        public AdminTaxesPageViewModel(
            ITaxesService taxesService,
            IPageDialogService pageDialogService) 
        {
            _taxesService = taxesService;

            _pageDialogService = pageDialogService;

            SaveCommand = new Command(async () => await OnSaveCommand());
        }

        private async Task OnSaveCommand()
        {
            CreateTaxCommand command = new CreateTaxCommand
            {
                Name = Name,
                TaxRatePercentage = TaxRatePercentage.Value,
                TaxRateVariable = TaxRateVariable,
                Active = Active,
                Printed = Printed
            };

            if (TypeTax == "" || TypeTax == null)
            {
                await _pageDialogService
                    .DisplayAlertAsync(
                    "Crea Impuesto",
                    "Debes seleccionar el tipo de impuesto",
                    "OK");
                return;
            }
            else 
            {
                if (TypeTax == "Incluido en el precio")
                {
                    command.Type = TaxTypeEnum.INCLUDED_IN_PRICE;
                }

                if (TypeTax == "Añadir al precio")
                {
                    command.Type = TaxTypeEnum.ADD_TO_PRICE;
                }
            }

            CreateTaxResult result = await _taxesService.CreateTax(command);

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
