using Mahzan.Mobile.API.Interfaces.Clients;
using Mahzan.Mobile.API.Requests.Clients;
using Mahzan.Mobile.API.Results.Clients;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Sales.NewSale.NewClient
{
    public class CreateClientPageViewModel : BindableBase
    {
        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _pageDialogService;

        private readonly IClientsService _clientsService;

        //Models
        private string _RFC;
        public string RFC
        {
            get { return _RFC; }
            set
            {
                _RFC = value;
                OnPropertyChanged(nameof(RFC)); // Notify that there was a change on this property
            }
        }

        private string _commercialName;
        public string CommercialName
        {
            get { return _commercialName; }
            set
            {
                _commercialName = value;
                OnPropertyChanged(nameof(CommercialName)); // Notify that there was a change on this property
            }
        }

        private string _businessName;
        public string BusinessName
        {
            get { return _businessName; }
            set
            {
                _businessName = value;
                OnPropertyChanged(nameof(BusinessName)); // Notify that there was a change on this property
            }
        }

        private string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email)); // Notify that there was a change on this property
            }
        }

        private string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone)); // Notify that there was a change on this property
            }
        }

        private string _notes;
        public string Notes
        {
            get { return _notes; }
            set
            {
                _notes = value;
                OnPropertyChanged(nameof(Notes)); // Notify that there was a change on this property
            }
        }

        public ICommand CreateClientCommand { get; private set; }

        public ICommand CleanCommand { get; private set; }



        public CreateClientPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IClientsService clientsService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _clientsService = clientsService;

            CreateClientCommand = new Command(()=> OnCreateClientCommand());

            CleanCommand = new Command(async () => await OnCleanCommand());
        }

        private async Task OnCleanCommand()
        {
            RFC = null;
            CommercialName = null;
            BusinessName = null;
            Email = null;
            Phone = null;
            Notes = null;
        }

        private async Task OnCreateClientCommand()
        {
            PostClientsResult result = await _clientsService
                                             .Post(new PostClientsRequest
                                             {
                                                 RFC = RFC,
                                                 CommercialName = CommercialName,
                                                 BusinessName = BusinessName,
                                                 Email = Email,
                                                 Phone = Phone,
                                                 Notes = Notes
                                             });

            if (result.IsValid)
            {
                await _pageDialogService
                      .DisplayAlertAsync("Nuevo Cliente",
                                         $"El Cliente con RFC {RFC} se ha agregado correctamente",
                                         "Ok");
            }
            else 
            {
                await _pageDialogService
                      .DisplayAlertAsync(result.Title,
                                         result.Message,
                                         "Ok");
            }
        }
    }
}
