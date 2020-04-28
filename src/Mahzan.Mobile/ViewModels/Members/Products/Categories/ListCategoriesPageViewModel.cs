using Mahzan.Mobile.API.Filters.ProductCategories;
using Mahzan.Mobile.API.Interfaces.ProductCategories;
using Mahzan.Mobile.API.Results.ProductCategories;
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

namespace Mahzan.Mobile.ViewModels.Members.Products.Categories
{
    public class ListCategoriesPageViewModel : BindableBase,INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IProductCategoriesService _productCategoriesService;

        private ObservableCollection<API.Entities.ProductCategories> _listViewProductCategories { get; set; }
        public ObservableCollection<API.Entities.ProductCategories> ListViewProductCategories
        {
            get => _listViewProductCategories;
            set
            {
                _listViewProductCategories = value;
                OnPropertyChanged(nameof(ListViewProductCategories));

            }
        }

        private API.Entities.ProductCategories _selectedProductCategories { get; set; }

        public API.Entities.ProductCategories SelectedProductCategories
        {
            get
            {
                return _selectedProductCategories;
            }
            set
            {
                if (_selectedProductCategories != value)
                {
                    _selectedProductCategories = value;
                    HandleSelectedProductCategories();
                }
            }
        }

        private void HandleSelectedProductCategories()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("productCategoriesId", SelectedProductCategories.ProductCategoriesId);
            _navigationService.NavigateAsync("AdminCategoriesPage", navigationParams);
        }
        //Commands
        public ICommand AddProductCategoryCommand { get; set; }

        public ListCategoriesPageViewModel(
            INavigationService navigationService,
            IProductCategoriesService productCategoriesService)
        {
            _navigationService = navigationService;
            _productCategoriesService = productCategoriesService;

            //Stores
            Task.Run(() => GetProductCategories());

            //Commands
            AddProductCategoryCommand = new Command(async () => await OnAddProductCategoryCommand());
        }

        private async Task OnAddProductCategoryCommand()
        {

           await _navigationService.NavigateAsync("AdminCategoriesPage");
        }

        private async Task GetProductCategories()
        {
            GetProductCategoriesResult result = await _productCategoriesService
                                                      .Get(new GetProductCategoriesFilter
                                                      {

                                                      });

            if (result.IsValid)
            {
                ListViewProductCategories = new ObservableCollection<API.Entities.ProductCategories>(result.ProductCategories);
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
            await GetProductCategories();
        }
    }
}
