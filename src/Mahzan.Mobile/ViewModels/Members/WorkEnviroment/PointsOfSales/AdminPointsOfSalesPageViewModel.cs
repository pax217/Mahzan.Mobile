using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.PointsOfSales;
using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Interfaces.PointsOfSales;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Requests.PointsOfSales;
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
    public class AdminPointsOfSalesPageViewModel : BindableBase, INavigationAware
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
                    OnPropertyChanged(nameof(SelectedStore));
                    HandleSelectedStore();
                }
            }
        }

        //Code
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

        //Nombre
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

        //Commands
        public ICommand DeleteCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        private void HandleSelectedStore()
        {
        }

        private Guid StoresId { get; set; }
        private Guid PointsOfSalesId { get; set; }

        public AdminPointsOfSalesPageViewModel(
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
            SaveCommand = new Command(async () => await OnSaveCommand());
            DeleteCommand = new Command(async () => await OnDeleteCommand());
        }

        private async Task OnDeleteCommand()
        {
            var answer = await Application
                   .Current
                   .MainPage
                   .DisplayAlert("Atención!",
                                 "¿Estas seguro de borrar el TPV?", "Si", "No");
            if (answer)
            {
                DeletePointsOfSalesResult result = await _pointsOfSalesService
                                                         .Delete(PointsOfSalesId);

                if (result.IsValid)
                {
                    SelectedStore = null;
                    Code = string.Empty;
                    Name = string.Empty;
                }

                await _pageDialogService.DisplayAlertAsync(result.Title, result.Message, "Ok");
            }
        }

        private async Task OnSaveCommand()
        {
            if (PointsOfSalesId == Guid.Empty)
            {
                PostPointOfSalesResult result = await _pointsOfSalesService
                                                      .Post(new PostPointOfSalesRequest
                                                          {
                                                            StoresId = SelectedStore.StoresId,
                                                            Code = Code,
                                                            Name = Name
                                                          });
                await _pageDialogService.DisplayAlertAsync(result.Title, result.Message, "Ok");

            }
            else
            {
                PutPointsOfSalesResult result = await _pointsOfSalesService
                                                          .Put(new PutPointsOfSalesRequest
                                                          {
                                                              PointOfSalesId = PointsOfSalesId,
                                                              Code = Code,
                                                              Name = Name
                                                          });

                await _pageDialogService.DisplayAlertAsync(result.Title, result.Message, "Ok");

            }
        }

        private async Task GetStores()
        {
            GetStoresResult result = await _storesService
                                            .Get(new GetStoresFilter
                                            {

                                            });

            if (result.IsValid)
            {
                ListViewStores = new ObservableCollection<API.Entities.Stores>(result.Stores);
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync(result.Title, result.Message, "Ok");
            }
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            StoresId = parameters.GetValue<Guid>("storesId");
            PointsOfSalesId = parameters.GetValue<Guid>("pointsOfSalesId");

            if (StoresId!=Guid.Empty && PointsOfSalesId!=Guid.Empty)
            {
                await GetPoitOfSale(StoresId, PointsOfSalesId);
            }
        }

        private async Task GetPoitOfSale(Guid storesId, Guid pointsOfSalesId)
        {
            GetPointsOfSalesResult result = await _pointsOfSalesService
                                                  .Get(new GetPointsOfSalesFilter
                                                  {
                                                      StoresId = storesId,
                                                      PointsOfSales = pointsOfSalesId
                                                  });

            if (result.IsValid)
            {
                SelectedStore = ListViewStores.SingleOrDefault(x => x.StoresId == storesId);
                Code = result.PointsOfSales.FirstOrDefault().Code;
                Name = result.PointsOfSales.FirstOrDefault().Name;
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync(result.Title, result.Message, "Ok");
            }
        }
    }
}
