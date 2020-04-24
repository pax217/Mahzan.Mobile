using Mahzan.Mobile.API.Filters.Companies;
using Mahzan.Mobile.API.Interfaces.Companies;
using Mahzan.Mobile.API.Results.Companies;
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
        private readonly ICompaniesService _companiesService;
        private ObservableCollection<API.Entities.Companies> _companies;
        public ObservableCollection<API.Entities.Companies> Companies
        {
            get => _companies;
            set => SetProperty(ref _companies, value);
        }

        //Selected ProductUnitCategory
        private API.Entities.Companies _selectedCompanies;
        public API.Entities.Companies SelectedCompanies
        {
            get => _selectedCompanies;
            set
            {
                if (_selectedCompanies != value)
                {
                    _selectedCompanies = value;
                }
            }
        }


        public ICommand SaveStoreCommand { get; set; }

        public AdminStorePageViewModel(
            INavigationService navigationService,
            ICompaniesService companiesService)
        {
            _companiesService = companiesService;

            Task.Run(() => GetCompanies());

            //Commands
            SaveStoreCommand = new Command(async () => await OnSaveStoreCommand());
        }

        private async Task OnSaveStoreCommand()
        {
            //Identifica si es una nueva  tienda 
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

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Guid storesId = parameters.GetValue<Guid>("storesId");
        }

    }
}
