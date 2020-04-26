using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.PointsOfSales;
using Mahzan.Mobile.API.Interfaces.PointsOfSales;
using Mahzan.Mobile.API.Results.PointsOfSales;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
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
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels
{
    public class SelectPointsOfSalesPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IPointsOfSalesService _pointsOfSalesService;

        //SQLite
        private readonly IRepository<AspNetUsers> _aspNetUsersRepository;
        private AspNetUsers aspNetUser;

        private ObservableCollection<PointsOfSales> _listPointsOfSales { get; set; }
        public ObservableCollection<PointsOfSales> ListPointsOfSales
        {
            get => _listPointsOfSales;
            set
            {
                _listPointsOfSales = value;
                OnPropertyChanged(nameof(ListPointsOfSales));

            }
        }

        private PointsOfSales _selectedPointsOfSales { get; set; }

        public PointsOfSales PointsOfSales
        {
            get
            {
                return _selectedPointsOfSales;
            }
            set
            {
                if (_selectedPointsOfSales != value)
                {
                    _selectedPointsOfSales = value;
                    HandleSelectedPointsOfSales();
                }
            }
        }

        private Guid? StoresId { get; set; }


        public SelectPointsOfSalesPageViewModel(
            INavigationService navigationService,
            IPointsOfSalesService pointsOfSalesService,
            IRepository<AspNetUsers> aspNetUsersRepository)
        {
            _navigationService = navigationService;
            _pointsOfSalesService = pointsOfSalesService;

            _aspNetUsersRepository = aspNetUsersRepository;

            Initialize();
        }

        private async void Initialize()
        {
            List<AspNetUsers> listAspNetUsers;
            listAspNetUsers = await _aspNetUsersRepository
                                    .Get();

            aspNetUser = listAspNetUsers.FirstOrDefault();
        }
        private void HandleSelectedPointsOfSales()
        {
            UpdateSelectedStore(_selectedPointsOfSales.PointsOfSalesId,
                                _selectedPointsOfSales.Name);

            _navigationService.NavigateAsync(nameof(MainPage) + "/" + nameof(NavigationPage) + "/" + nameof(DashboardPage));

        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            StoresId = parameters.GetValue<Guid>("storesId");
            if (StoresId != Guid.Empty)
            {
                await GetPointsOfSales(StoresId.Value);
            }
        }

        private async Task GetPointsOfSales(Guid storesId)
        {
            GetPointsOfSalesResult result = await _pointsOfSalesService
                                                  .Get(new GetPointsOfSalesFilter
                                                  {
                                                      StoresId = storesId
                                                  });

            if (result.IsValid)
            {
                ListPointsOfSales = new ObservableCollection<PointsOfSales>(result.PointsOfSales);
            }
            else 
            {
                await Application.Current.MainPage.DisplayAlert(result.Title, result.Message, "ok");

            }

        }

        private void UpdateSelectedStore(Guid pointsOfSalesId,
                                         string pointOfSaleName)
        {


            aspNetUser.PointsOfSalesId = pointsOfSalesId;
            aspNetUser.PointOfSaleName = pointOfSaleName;

            _aspNetUsersRepository.Update(aspNetUser);

        }
    }
}
