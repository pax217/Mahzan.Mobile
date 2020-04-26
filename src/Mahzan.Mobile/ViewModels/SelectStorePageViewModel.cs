using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.EmployeesStores;
using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Interfaces.EmployeesStores;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Results.EmployeesStores;
using Mahzan.Mobile.API.Results.Stores;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels
{
    public class SelectStorePageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;
        private readonly IStoresService _storesService;
        private readonly IEmployeesStoresService _employeesStoresService;

        //SQLite
        private readonly IRepository<AspNetUsers> _aspNetUsersRepository;

        private AspNetUsers aspNetUser;

        private ObservableCollection<Stores> _listStores { get; set; }
        public ObservableCollection<Stores> ListStores
        {
            get => _listStores;
            set
            {
                _listStores = value;
                OnPropertyChanged(nameof(ListStores));

            }
        }

        private Stores _selectedStore { get; set; }

        public Stores SelectedStore
        {
            get
            {
                return _selectedStore;
            }
            set
            {
                if (_selectedStore != value)
                {
                    _selectedStore = value;
                    HandleSelectedStore();
                }
            }
        }

        private void HandleSelectedStore()
        {
            UpdateSelectedStore(_selectedStore.StoresId, _selectedStore.Name);

            var navigationParams = new NavigationParameters();
            navigationParams.Add("storesId", SelectedStore.StoresId);

            _navigationService.NavigateAsync("SelectPointsOfSalesPage", navigationParams);

        }

        public SelectStorePageViewModel(
            INavigationService navigationService,
            IStoresService storesService,
            IEmployeesStoresService employeesStoresService,
            IRepository<AspNetUsers> aspNetUsersRepository)
        {
            //Services
            _navigationService = navigationService;
            _storesService = storesService;
            _employeesStoresService = employeesStoresService;

            //SQlite
            _aspNetUsersRepository = aspNetUsersRepository; 

            Initialize();
        }

        private async void Initialize()
        {
            List<AspNetUsers> listAspNetUsers = await _aspNetUsersRepository.Get();
            aspNetUser = listAspNetUsers.FirstOrDefault();

            switch (aspNetUser.Role)
            {
                case "MEMBER":
                    await InitializeMember(aspNetUser);
                    break;
                default:
                    break;
            }
        }

        private async Task InitializeMember(AspNetUsers aspNetUsers) 
        {
            GetStoresResult result = await _storesService.Get(new GetStoresFilter { });

            if (result.IsValid)
            {
                ListStores = new ObservableCollection<Stores>(result.Stores);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(result.Title, result.Message, "ok");
            }
        }

        private async void InitializeCashier()
        {
            GetEmployeesStoresResult getEmployeesStoresResult = await _employeesStoresService
                                                                      .Get(new GetEmployeesStoresFilter
                                                                      {
                                                                          EmployeesId = new Guid("8564531A-F060-48CF-AB5E-08D7D373C472")
                                                                      });
            if (getEmployeesStoresResult.IsValid)
            {
                ListStores = new ObservableCollection<Stores>(getEmployeesStoresResult.Stores);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert(getEmployeesStoresResult.Title, getEmployeesStoresResult.Message, "ok");
            }

        }

        private void UpdateSelectedStore(Guid storesId,
                                         string storeName)
        {


            aspNetUser.StoresId = storesId;
            aspNetUser.StoreName = storeName;

            _aspNetUsersRepository.Update(aspNetUser);

        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {

        }
    }
}
