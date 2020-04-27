using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Results.Tickets;
using Mahzan.Mobile.ViewModels.Members.Sales.NewSale;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Mahzan.Mobile.ViewModels.Members.Sales.Tickets
{
    public class TicketPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly ITicketsService _ticketsService;

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
                OnPropertyChanged(nameof(_createdAt));
            }
        }



        public TicketPageViewModel(
            INavigationService navigationService,
            ITicketsService ticketsService)
        {
            _navigationService = navigationService;
            _ticketsService = ticketsService;
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            Guid ticketsId = parameters.GetValue<Guid>("ticketsId");

            if (ticketsId!=Guid.Empty)
            {
                GetTicketResult result = await _ticketsService.GetById(ticketsId);

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
