using Mahzan.Mobile.API.Filters.Companies;
using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Interfaces.Companies;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Requests.Companies;
using Mahzan.Mobile.API.Requests.Stores;
using Mahzan.Mobile.API.Results.Companies;
using Mahzan.Mobile.API.Results.Stores;
using Mahzan.Mobile.Views;
using Mahzan.Mobile.Views.Members;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.WorkEnviroment.Stores
{
    public class AdminStorePageViewModel : BindableBase,INavigationAware
    {
        //Services
        private readonly INavigationService _navigationService;

        private readonly ICompaniesService _companiesService;

        private readonly IStoresService _storesService;

        //Picker
        private ObservableCollection<API.Entities.Companies> _companies;
        public ObservableCollection<API.Entities.Companies> Companies
        {
            get => _companies;
            set => SetProperty(ref _companies, value);
        }

        //Selected Company
        private API.Entities.Companies _selectedCompanies;
        public API.Entities.Companies SelectedCompanies
        {
            get => _selectedCompanies;
            set
            {
                if (_selectedCompanies != value)
                {
                    _selectedCompanies = value;
                    //SetProperty(ref _selectedCompanies, value);
                    OnPropertyChanged(nameof(SelectedCompanies));
                }
            }
        }

        //Commands
        public ICommand SaveStoreCommand { get; set; }
        public ICommand DeleteStoreCommand { get; set; }

        //Properties Store
        private Guid? StoresId { get; set; }

        private string _code;
        public string Code
        {
            get { return _code; }
            set
            {
                _code = value;
                OnPropertyChanged(nameof(Code)); // Notify that there was a change on this property
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name)); // Notify that there was a change on this property
            }
        }

        private string _comment;
        public string Comment
        {
            get { return _comment; }
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment)); // Notify that there was a change on this property
            }
        }


        public AdminStorePageViewModel(
            INavigationService navigationService,
            ICompaniesService companiesService,
            IStoresService storesService)
        {
            _navigationService = navigationService;
            _companiesService = companiesService;
            _storesService = storesService;

            Task.Run(() => GetCompanies());

            //Commands
            SaveStoreCommand = new Command(async () => await OnSaveStoreCommand());
            DeleteStoreCommand = new Command(async () => await OnDeleteStoreCommand());
            
        }

        private async Task OnDeleteStoreCommand()
        {
            var answer = await Application
                               .Current
                               .MainPage
                               .DisplayAlert("Atención!", 
                                             "¿Estas seguro de borrar el ticket?", "Si", "No");
            if (answer)
            {
                DeleteStoresResult result = await _storesService
                                                  .Delete(StoresId.Value);

                await Application
                        .Current
                        .MainPage
                        .DisplayAlert(result.Title,
                                      result.Message, "ok");
            }
        }

        private async Task OnSaveStoreCommand()
        {
            //Identifica si es una nueva  tienda 
            if (StoresId == Guid.Empty)
            {
                //Nuevo

                AddStoresResult result = await _storesService
                                               .Add(new AddStoresRequest
                                               {
                                                   Code = Code,
                                                   Name = Name,
                                                   Phone = Phone,
                                                   Comment = Comment,
                                                   CompaniesId = _selectedCompanies.CompaniesId
                                               });

                await Application
                        .Current
                        .MainPage
                        .DisplayAlert(result.Title, 
                                      result.Message, "ok");
            }
            else 
            {
                //Actualiza
                PutStoresResult result = await _storesService
                                                .Update(new PutStoresRequest
                                                {
                                                    StoresId = StoresId.Value,
                                                    Code = Code,
                                                    Name = Name,
                                                    Phone = Phone,
                                                    Comment = Comment
                                                });
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert(result.Title,
                                    result.Message, "ok");
            }
        }

        private async Task GetCompanies()
        {
            GetCompaniesResult result = await _companiesService.Get(new GetCompaniesFilter { });

            if (result.IsValid)
            {
                Companies = new ObservableCollection<API.Entities.Companies>(result.Companies);
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

        private async Task GetCompany(Guid storesId) 
        {

            GetStoresResult result = await _storesService
                                           .Get(new GetStoresFilter
                                           {
                                               StoresId = storesId
                                           });


            if (result.IsValid)
            {
                API.Entities.Stores store = result.Stores.FirstOrDefault();

                SelectedCompanies = Companies
                                    .SingleOrDefault(x=> x.CompaniesId == store.CompaniesId);
                Code = store.Code;
                Name = store.Name;
                Phone = store.Phone;
                Comment = store.Comment;
            }
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {
            //throw new NotImplementedException();
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            StoresId = parameters.GetValue<Guid>("storesId");
            if (StoresId!=Guid.Empty)
            {
                await GetCompany(StoresId.Value);
            }

        }
    }
}
