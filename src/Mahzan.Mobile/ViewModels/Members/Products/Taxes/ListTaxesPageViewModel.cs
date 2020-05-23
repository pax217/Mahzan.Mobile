using Mahzan.Mobile.API.Filters.Taxes;
using Mahzan.Mobile.API.Interfaces.Taxes;
using Mahzan.Mobile.API.Results.Taxes;
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

namespace Mahzan.Mobile.ViewModels.Members.Products.Taxes
{
    public class ListTaxesPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly ITaxesService _taxesService;

        private ObservableCollection<API.Entities.Taxes> _listViewTaxes { get; set; }
        public ObservableCollection<API.Entities.Taxes> ListViewTaxes
        {
            get => _listViewTaxes;
            set
            {
                _listViewTaxes = value;
                OnPropertyChanged(nameof(ListViewTaxes));

            }
        }

        private API.Entities.Taxes _selectedTaxes { get; set; }

        public API.Entities.Taxes SelectedTaxes
        {
            get
            {
                return _selectedTaxes;
            }
            set
            {
                if (_selectedTaxes != value)
                {
                    _selectedTaxes = value;
                    HandleSelectedTaxes();
                }
            }
        }


        //Command
        public ICommand AddCommand { get; set; }

        private void HandleSelectedTaxes()
        {

        }

        public ListTaxesPageViewModel(
            INavigationService navigationService,
            ITaxesService taxesService)
        {
            _navigationService = navigationService;
            _taxesService = taxesService;

            //Command
            AddCommand = new Command(async () => await OnAddCommand());

            //Taxes
            Task.Run(() => GetTaxes());
        }

        private async Task GetTaxes()
        {
            GetTaxesResult result= await _taxesService.GetWhere(new GetTaxesFilter
            {

            });

            if (result.IsValid)
            {
                ListViewTaxes = new ObservableCollection<API.Entities.Taxes>(result.Taxes);
            }
        }

        private async Task OnAddCommand()
        {
           await _navigationService
                  .NavigateAsync("AdminTaxesPage");
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await GetTaxes();
        }
    }
}
