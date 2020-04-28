using Mahzan.Mobile.API.Filters.PointsOfSales;
using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Interfaces.PointsOfSales;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Results.PointsOfSales;
using Mahzan.Mobile.API.Results.Stores;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.WorkEnviroment.PointsOfSales
{
    public class ListPointsOfSalesPageViewModel : BindableBase, INavigationAware 
    {
        private readonly INavigationService _navigationService;

        private readonly IPageDialogService _pageDialogService;

        private readonly IStoresService _storesService;

        private readonly IPointsOfSalesService _pointsOfSalesService;

        private ObservableCollection<API.Entities.Stores> _listViewStores { get; set; }
        public ObservableCollection<API.Entities.Stores> ListViewStores
        {
            get => _listViewStores;
            set
            {
                _listViewStores = value;
                OnPropertyChanged(nameof(ListViewStores));

            }
        }

        private API.Entities.Stores _selectedStore { get; set; }
        public API.Entities.Stores SelectedStore
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
            Task.Run(() => GetPointsOfSales());
        }

        private ObservableCollection<API.Entities.PointsOfSales> _listViewPointsOfSales { get; set; }
        public ObservableCollection<API.Entities.PointsOfSales> ListViewPointsOfSales
        {
            get => _listViewPointsOfSales;
            set
            {
                _listViewPointsOfSales = value;
                OnPropertyChanged(nameof(ListViewPointsOfSales));

            }
        }

        private API.Entities.PointsOfSales _selectedPointsOfSales { get; set; }
        public API.Entities.PointsOfSales SelectedPointsOfSales
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

        private void HandleSelectedPointsOfSales()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("storesId", SelectedStore.StoresId);
            navigationParams.Add("pointsOfSalesId", SelectedPointsOfSales.PointsOfSalesId);
            _navigationService.NavigateAsync("AdminPointsOfSalesPage", navigationParams);
        }

        public ICommand AddPointOfSaleCommand { get; set; }

        public ListPointsOfSalesPageViewModel(
            INavigationService navigationService,
            IPageDialogService pageDialogService,
            IStoresService storesService,
            IPointsOfSalesService pointsOfSalesService)
        {
            //Services
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _storesService = storesService;
            _pointsOfSalesService = pointsOfSalesService;

            //Stores
            Task.Run(() => GetStores());


            //Commands
            AddPointOfSaleCommand = new Command(async () => await OnAddPointOfSaleCommand());
        }

        private async Task GetStores()
        {
            GetStoresResult result = await _storesService
                                            .Get(new GetStoresFilter {
                                                  
                                            });

            if (result.IsValid)
            {
                ListViewStores = new ObservableCollection<API.Entities.Stores>(result.Stores);
            }
            else
            {
                await Application
                     .Current
                     .MainPage
                     .DisplayAlert(result.Title,
                                   result.Message, "ok");
            }
        }

        private async Task GetPointsOfSales()
        {
            GetPointsOfSalesResult result = await _pointsOfSalesService
                                                  .Get(new GetPointsOfSalesFilter
                                                  {
                                                      StoresId = SelectedStore.StoresId
                                                  });

            if (result.IsValid)
            {
                ListViewPointsOfSales = new ObservableCollection<API.Entities.PointsOfSales>(result.PointsOfSales);
            }
            else 
            {
                ListViewPointsOfSales.Clear();

                await _pageDialogService.DisplayAlertAsync(result.Title, result.Message, "Ok");
            }
        }

        private async Task OnAddPointOfSaleCommand()
        {
            await _navigationService.NavigateAsync("AdminPointsOfSalesPage");
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetStores();
        }
    }
}
