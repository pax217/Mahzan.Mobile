using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Results.Stores;
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
    public class ListStoresPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IStoresService _storesService;

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
            var navigationParams = new NavigationParameters();
            navigationParams.Add("storesId", SelectedStore.StoresId);
            _navigationService.NavigateAsync("AdminStorePage", navigationParams);
        }

        //Commands
        public ICommand AddStoreCommand { get; set; }
        public ListStoresPageViewModel(
            INavigationService navigationService,
            IStoresService storesService)
        {

            _navigationService = navigationService;
            _storesService = storesService;

            //Stores
            Task.Run(() => GetStores());

            //Commands
            AddStoreCommand = new Command(async () => await OnAddStoreCommand());
        }
        private async Task OnAddStoreCommand()
        {
            await _navigationService.NavigateAsync("AdminStorePage");
        }
        private async Task GetStores()
        {
            GetStoresResult result = await _storesService.Get(new GetStoresFilter { });

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
                                    result.Message,
                                    "ok");
            }
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
