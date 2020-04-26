using Mahzan.Mobile.Models.Members.Sales.NewSale;
using Mahzan.Mobile.Views;
using Mahzan.Mobile.Views.Members.Sales;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Sales.NewSale
{
    public class EndSalePageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;

        private ChargeTicketDetail ChargeTicketDetail = new ChargeTicketDetail();

        private decimal _total { get; set; }

        public decimal Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        private decimal _cashExchange { get; set; }

        public decimal CashExchange
        {
            get => _cashExchange;
            set
            {
                _cashExchange = value;
                OnPropertyChanged(nameof(CashExchange));
            }
        }

        public ICommand NewSaleCommand { get; private set; }

        public EndSalePageViewModel(
            INavigationService navigationService)
        {
            _navigationService = navigationService;

            NewSaleCommand = new Command(() => OnNewSaleCommand());

        }

        private void OnNewSaleCommand()
        {
            _navigationService.GoBackToRootAsync();

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Debug.WriteLine(parameters);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ChargeTicketDetail = parameters.GetValue<ChargeTicketDetail>("chargeTicketDetail");

            if (ChargeTicketDetail!=null)
            {
                Total = ChargeTicketDetail.Total;
                CashExchange = ChargeTicketDetail.CashExchange.Value;
            }
        }

    }
}
