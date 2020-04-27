using Mahzan.Mobile.API.Filters.Tickets;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Results.Tickets;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Sales.Tickets
{
    public class ListTicketsPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly ITicketsService _ticketsService;

        //ListView
        private ObservableCollection<API.Entities.Tickets> _listViewTickets { get; set; }
        public ObservableCollection<API.Entities.Tickets> ListViewTickets
        {
            get => _listViewTickets;
            set
            {
                _listViewTickets = value;
                OnPropertyChanged(nameof(ListViewTickets));

            }
        }

        private API.Entities.Tickets _selectedTicket { get; set; }
        public API.Entities.Tickets SelectedTicket
        {
            get
            {
                return _selectedTicket;
            }
            set
            {
                if (_selectedTicket != value)
                {
                    _selectedTicket = value;
                    HandleSelectedTicket();
                }
            }
        }

        //DatePicker
        private DateTime _createdAt = DateTime.Now.Date;
        public DateTime CreatedAt
        {
            get
            {
                return _createdAt;
            }
            set
            {
                if (_createdAt != value)
                {
                    _createdAt = value;
                    HandleSelectedCreatedAt();
                }
            }
        }

        private async void HandleSelectedCreatedAt()
        {
            GetTicketsResult result = await _ticketsService
                                            .Get(new GetTicketsFilter
                                            {
                                                CreatedAt = _createdAt
                                            });

            if (result.IsValid)
            {
                ListViewTickets = new ObservableCollection<API.Entities.Tickets>(result.Tickets);
            }
            else
            {
                ListViewTickets.Clear();

                await Application
                      .Current
                      .MainPage
                      .DisplayAlert(result.Title,
                                    result.Message,
                                    "ok");
            }
        }

        private void HandleSelectedTicket()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("ticketsId", SelectedTicket.TicketsId);
            _navigationService.NavigateAsync("TicketPage", navigationParams);
        }

        public ListTicketsPageViewModel(
            INavigationService navigationService,
            ITicketsService ticketsService)
        {
            _navigationService = navigationService;
            _ticketsService = ticketsService;

            Task.Run(() => GetTickets());
        }


        private async Task GetTickets() 
        {
            GetTicketsResult result = await _ticketsService
                                            .Get(new GetTicketsFilter { 
                                                CreatedAt = DateTime.Now.Date
                                            });

            if (result.IsValid)
            {
                ListViewTickets = new ObservableCollection<API.Entities.Tickets>(result.Tickets);
            }
            else 
            {
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert(result.Title,
                                    result.Message,
                                    "ok");
            }
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {

        }
    }
}
