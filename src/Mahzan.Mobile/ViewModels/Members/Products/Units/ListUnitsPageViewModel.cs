using Mahzan.Mobile.API.Filters.ProductUnits;
using Mahzan.Mobile.API.Interfaces.ProductUnits;
using Mahzan.Mobile.API.Results.ProductUnits;
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

namespace Mahzan.Mobile.ViewModels.Members.Products.Units
{
    public class ListUnitsPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IProductUnitsService _productUnitsService;

        private ObservableCollection<API.Entities.ProductUnits> _listViewProductUnits { get; set; }
        public ObservableCollection<API.Entities.ProductUnits> ListViewProductUnits
        {
            get => _listViewProductUnits;
            set
            {
                _listViewProductUnits = value;
                OnPropertyChanged(nameof(ListViewProductUnits));

            }
        }

        private API.Entities.ProductUnits _selectedProductUnits { get; set; }

        public API.Entities.ProductUnits SelectedProductUnits
        {
            get
            {
                return _selectedProductUnits;
            }
            set
            {
                if (_selectedProductUnits != value)
                {
                    _selectedProductUnits = value;
                    HandleSelectedProductUnits();
                }
            }
        }

        private void HandleSelectedProductUnits()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("productUnitsId", SelectedProductUnits.ProductUnitsId);
            _navigationService.NavigateAsync("AdminUnitsPage", navigationParams);
        }

        public ICommand AddUnitCommand { get; set; }

        public ListUnitsPageViewModel(
            INavigationService navigationService,
            IProductUnitsService productUnitsService)
        {
            //Services
            _navigationService = navigationService;
            _productUnitsService = productUnitsService;

            //Stores
            Task.Run(() => GetProductUnits());

            //Commands
            AddUnitCommand = new Command(async () => await OnAddUnitCommand());
        }

        private async Task OnAddUnitCommand()
        {
            await _navigationService.NavigateAsync("AdminUnitsPage");
        }


        private async Task GetProductUnits()
        {
            GetProductUnitsResult result =  await _productUnitsService
                                                  .Get(new GetProductUnitsFilter
                                                  {

                                                  });

            if (result.IsValid)
            {
                ListViewProductUnits = new ObservableCollection<API.Entities.ProductUnits>(result.ProductUnits);
            }
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetProductUnits();
        }
    }
}
