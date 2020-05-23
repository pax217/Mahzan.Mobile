using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Results.Tickets;
using Mahzan.Mobile.Services.Interfaces;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
using Mahzan.Mobile.ViewModels.Members.Sales.NewSale;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Sales.Tickets
{
    public class TicketPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _pageDialogService;

        private readonly ITicketsService _ticketsService;

        private readonly IBlueToothService _blueToothService;

        private readonly IPrintTicketService _printTicketService;

        private readonly IRepository<BluetoothDevice> _bluetoothDeviceRepository;

        private ObservableCollection<TicketDetail> _listViewTicketDetail { get; set; }

        public ObservableCollection<TicketDetail> ListViewTicketDetail
        {
            get => _listViewTicketDetail;
            set
            {
                _listViewTicketDetail = value;
                OnPropertyChanged(nameof(ListViewTicketDetail));
            }
        }

        private string _total { get; set; }

        public string Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
        }

        private string _totalProducts { get; set; }

        public string TotalProducts
        {
            get => _totalProducts;
            set
            {
                _totalProducts = value;
                OnPropertyChanged(nameof(TotalProducts));
            }
        }

        private DateTime _createdAt { get; set; }

        public DateTime CreatedAt
        {
            get => _createdAt;
            set
            {
                _createdAt = value;
                OnPropertyChanged(nameof(CreatedAt));
            }
        }

        public Guid TicketsId { get; set; }

        //Comands
        public ICommand PrintCommand { get; set; }

        public TicketPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            ITicketsService ticketsService,
            IPrintTicketService printTicketService,
            IRepository<BluetoothDevice> bluetoothDeviceRepository)
        {
            _navigationService = navigationService;
            _ticketsService = ticketsService;
            _printTicketService = printTicketService;
            _pageDialogService = pageDialogService;

            //REpository
            _bluetoothDeviceRepository = bluetoothDeviceRepository;

            //Comands 
            PrintCommand = new Command(async () => await OnPrintCommand());

            //BlueTooth
            _blueToothService = Xamarin.Forms.DependencyService.Get<IBlueToothService>();
        }

        private async Task OnPrintCommand()
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
                                                    TicketsId);



            await _blueToothService
                  .Print(bluetoothDevice.FirstOrDefault().DeviceName, 
                         ticket.ToString());

        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            TicketsId = parameters.GetValue<Guid>("ticketsId");

            if (TicketsId != Guid.Empty)
            {
                GetTicketResult result = await _ticketsService.GetById(TicketsId);

                if (result.IsValid)
                {
                    CreatedAt = result.Ticket.CreatedAt;
                    Total = result.Ticket.Total.ToString();
                    TotalProducts = result.Ticket.TotalProducts.ToString();
                    ListViewTicketDetail = new ObservableCollection<TicketDetail>(result.Ticket.TicketDetails);
                }
            }
        }
    }
}
