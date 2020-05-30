using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Results.Tickets;
using Mahzan.Mobile.Models.Members.Sales.NewSale;
using Mahzan.Mobile.Services.Interfaces;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Sales.NewSale
{
    public class EndSalePageViewModel : BindableBase, INavigationAware
    {
        private INavigationService _navigationService;

        private readonly IPageDialogService _pageDialogService;

        private ChargeTicketDetail ChargeTicketDetail = new ChargeTicketDetail();

        private readonly ITicketsService _ticketsService; 

        private readonly IBlueToothService _blueToothService;

        private readonly IRepository<BluetoothDevice> _bluetoothDeviceRepository;


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

        public ICommand PrintTicketCommand { get; private set; }

        public EndSalePageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IRepository<BluetoothDevice> bluetoothDeviceRepository, 
            ITicketsService ticketsService)
        {


            //Repository
            _bluetoothDeviceRepository = bluetoothDeviceRepository;

            //Commands
            NewSaleCommand = new Command(() => OnNewSaleCommand());
            PrintTicketCommand = new Command(async () => await OnPrintTicketCommand());


            //BlueTooth
            _blueToothService = Xamarin.Forms.DependencyService.Get<IBlueToothService>();

            //Services
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _ticketsService = ticketsService;
        }

        private async Task OnPrintTicketCommand()
        {
            List<BluetoothDevice> bluetoothDevice = await _bluetoothDeviceRepository
                                              .Get();

            if (!bluetoothDevice.Any())
            {
                await _pageDialogService
                      .DisplayAlertAsync("Impresora no encontrada",
                                         "Debes configurar una impresora bluetooth, en el menu de Configuración.",
                                         "Ok");
                return;
            }

            GetTicketToPrintResult result = await _ticketsService
                .GetTicketToPrint(ChargeTicketDetail.TicketsClosedId);

            if (result.IsValid)
            {
                await _blueToothService
                      .Print(bluetoothDevice.FirstOrDefault().DeviceName,
                             result.Ticket);
            }
            else 
            {
                await _pageDialogService
                      .DisplayAlertAsync(
                        result.Title,
                        result.Message,
                        "Ok");
                return;
            }

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
