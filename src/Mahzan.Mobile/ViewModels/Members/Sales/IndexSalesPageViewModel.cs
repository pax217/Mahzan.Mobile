using Mahzan.Mobile.Models.Members.Sales;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mahzan.Mobile.ViewModels.Members.Sales
{
    public class IndexSalesPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;

        public ObservableCollection<SalesOptions> ListSalesOptionItems { get; set; }

        private SalesOptions _selectedSaleOption { get; set; }

        public SalesOptions SelectedSaleOption
        {
            get
            {
                return _selectedSaleOption;
            }
            set
            {
                if (_selectedSaleOption != value)
                {
                    _selectedSaleOption = value;
                    HandleSelectedSaleOptions();
                }
            }
        }

        public IndexSalesPageViewModel(
            INavigationService navigationService)
            : base(navigationService)
        {
            _navigationService = navigationService;

            ListSalesOptionItems = new ObservableCollection<SalesOptions>()
            {
                new SalesOptions(){ Option ="Nueva Venta",OptionDetail="Crea una venta para tus Clientes"},
                new SalesOptions(){ Option ="Tickets",OptionDetail="Consulta los tickets de tus Ventas"},

            };
        }

        public void HandleSelectedSaleOptions()
        {
            switch (_selectedSaleOption.Option)
            {
                case "Nueva Venta":
                    _navigationService.NavigateAsync("NewSalePage");
                    break;
                case "Tickets":
                    _navigationService.NavigateAsync("NewSalePage");
                    break;
                default:
                    break;
            }

        }
    }
}
