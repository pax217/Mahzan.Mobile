using Mahzan.Mobile.Models.Members.Sales.NewSale;
using Mahzan.Mobile.Services.Interfaces;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
using Mahzan.Mobile.Views;
using Mahzan.Mobile.Views.Members.Sales;
using Prism.Commands;
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

        private readonly IPrintTicketService _printTicketService;

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
            IPrintTicketService printTicketService,
            IRepository<BluetoothDevice> bluetoothDeviceRepository)
        {
            //Services
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _printTicketService = printTicketService;

            //Repository
            _bluetoothDeviceRepository = bluetoothDeviceRepository;

            //Commands
            NewSaleCommand = new Command(() => OnNewSaleCommand());
            PrintTicketCommand = new Command(async () => await OnPrintTicketCommand());


            //BlueTooth
            _blueToothService = Xamarin.Forms.DependencyService.Get<IBlueToothService>();
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

            StringBuilder ticket = await _printTicketService
                                  .GetTicketToPrint(new Guid("B8742F54-79D0-4280-2F98-08D7E3BB9FC8"),
                                                    ChargeTicketDetail.TicketsClosedId);

            await _blueToothService
                  .Print(bluetoothDevice.FirstOrDefault().DeviceName,
                         ticket.ToString());
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
