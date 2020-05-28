using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.Products;
using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Interfaces.Products;
using Mahzan.Mobile.API.Interfaces.ProductsStore;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Requests.ProductsStore;
using Mahzan.Mobile.API.Results.Products;
using Mahzan.Mobile.API.Results.ProductsStore;
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

namespace Mahzan.Mobile.ViewModels.Members.Products.Inventory
{
    public class AddProductInventoryPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IStoresService _storesService;

        private readonly IProductsStoreService _productsStoreService;

        private readonly IProductsService _productsService;

        public bool InAllStores { get; set; }

        //Stock
        private Guid? ProductsId { get; set; }
        public decimal? InStock { get; set; }
        public decimal? LowStock { get; set; }
        public decimal? OptimumStock { get; set; }

        private decimal? _price;
        public decimal? Price
        {
            get { return _price; }
            set { SetProperty(ref _price, value); }
        }

        private decimal? _cost;
        public decimal? Cost
        {
            get { return _cost; }
            set { SetProperty(ref _cost, value); }
        }

        private string _totalProducts { get; set; }
        public string TotalProducts
        {
            get => _totalProducts;
            set
            {
                _totalProducts = value;
                OnPropertyChanged(nameof(TotalProducts));
            }
        }

        //Pickers
        private ObservableCollection<Stores> _stores;
        public ObservableCollection<Stores> Stores
        {
            get => _stores;
            set => SetProperty(ref _stores, value);
        }

        //Selected ProductUnitCategory
        private Stores _selectedStores;
        public Stores SelectedStores
        {
            get => _selectedStores;
            set
            {
                if (_selectedStores != value)
                {
                    _selectedStores = value;
                    //HandleSelectedProductCategory();
                }
            }
        }

        //List View
        private ObservableCollection<ListViewInventory> _listViewInventory { get; set; }

        public ObservableCollection<ListViewInventory> ListViewInventory
        {
            get => _listViewInventory;
            set
            {
                _listViewInventory = value;
                OnPropertyChanged(nameof(ListViewInventory));
            }
        }

        private List<Products_Store> ListProductStores = new List<Products_Store>();

        //Commands
        public ICommand AddProductInventoryCommand { get; private set; }
        public ICommand AddInventoryCommand { get; private set; }
        public ICommand ButtonEraseCommand { get; private set; }


        public AddProductInventoryPageViewModel(
            INavigationService navigationService,
            IStoresService storesService,
            IProductsStoreService productsStoreService, 
            IProductsService productsService)
        {
            _navigationService = navigationService;
            _storesService = storesService;
            _productsStoreService = productsStoreService;
            _productsService = productsService;

            //Pickers
            Task.Run(() => GetStores());

            //Commands
            AddProductInventoryCommand = new Command(async () => await OnAddProductInventoryCommand());
            AddInventoryCommand = new Command(async () => await OnAddInventoryCommand());
            ButtonEraseCommand = new Command(async () => await OnButtonEraseCommand());

            //ListView
            ListViewInventory = new ObservableCollection<ListViewInventory>();

        }

        private async Task OnButtonEraseCommand()
        {
            var answer = await Application
                   .Current
                   .MainPage
                   .DisplayAlert("Atención!",
                                 "¿Estas seguro de eliminar el inventario?", "Si", "No");
            if (answer)
            {
                ListProductStores.Clear();
                UpdateInventory();
            }

        }

        private async Task OnAddInventoryCommand()
        {
            PostProductsStoreRequest request = new PostProductsStoreRequest { };
            request.ProductsStoreRequest = new List<ProductsStoreRequest>();

            foreach (var productStores in ListProductStores)
            {
                request.ProductsStoreRequest.Add(new ProductsStoreRequest
                {
                    Price = productStores.Price,
                    Cost = productStores.Cost,
                    InStock = productStores.InStock,
                    LowStock = productStores.LowStock,
                    OptimumStock = productStores.OptimumStock,
                    ProductsId = productStores.ProductsId,
                    StoresId = productStores.StoresId,
                });
            }

            PostProductsStoreResult result = await _productsStoreService.Add(request);

            await Application
                  .Current
                  .MainPage
                  .DisplayAlert(result.Title, result.Message, "ok");
        }

        private async Task OnAddProductInventoryCommand()
        {
            InsertProductStores();
            UpdateInventory();
        }

        private async Task GetStores()
        {
            GetStoresResult result = await _storesService
                                           .Get(new GetStoresFilter
                                           {

                                           });

            if (result.IsValid)
            {
                Stores = new ObservableCollection<Stores>(result.Stores);
            }
            else
            {
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert(result.Title, result.Message, "ok");
            }
        }


        private async void InsertProductStores()
        {

            Products_Store existProductsStore = (from ps in ListProductStores
                                                 where ps.ProductsId == ProductsId
                                                 && ps.StoresId == _selectedStores.StoresId
                                                 select ps)
                                                .FirstOrDefault();

            if (existProductsStore == null)
            {

                if (InAllStores)
                {
                    foreach (var store in Stores)
                    {
                        ListProductStores.Add(new Products_Store
                        {
                            ProductsId = ProductsId.Value,
                            StoresId = store.StoresId,
                            InStock = InStock,
                            LowStock = LowStock,
                            OptimumStock = OptimumStock,
                            Price = Price.Value,
                            Cost = Cost,
                        });
                    };
                }
                else 
                {
                    ListProductStores.Add(new Products_Store
                    {
                        ProductsId = ProductsId.Value,
                        StoresId = _selectedStores.StoresId,
                        InStock = InStock,
                        LowStock = LowStock,
                        OptimumStock = OptimumStock,
                        Price = Price.Value,
                        Cost = Cost,
                    });
                }

            }
            else
            {
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert("Atención",
                                    "El producto ya se encuentra en esta tienda",
                                    "ok");
            }

            UpdateInventory();
        }

        private async void UpdateInventory()
        {
            decimal? totalStock = 0;

            ListViewInventory.Clear();

            foreach (Products_Store product_Store in ListProductStores)
            {
                ListViewInventory.Add(new ListViewInventory
                {
                    StoreName = Stores
                                .SingleOrDefault(x => x.StoresId == product_Store.StoresId)
                                .Name,
                    Cost = product_Store.Cost.Value,
                    Price = product_Store.Price,
                    InStock = product_Store.InStock.Value,
                });

                totalStock += product_Store.InStock;
            }


            Device.BeginInvokeOnMainThread(() =>
            {
                TotalProducts = totalStock.ToString();
            });
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            ProductsId = parameters.GetValue<Guid>("productsId");

            if (ProductsId != Guid.Empty)
            {
                GetProductsResult result = await _productsService.Get(new GetProductsFilter
                {
                    ProductsId = ProductsId.Value
                });

                if (result.IsValid)
                {
                    Price = result.Products.FirstOrDefault().Price;
                    Cost = result.Products.FirstOrDefault().Cost;
                }
            }
        }


        private async Task GetProductStores() 
        {
        
        }
    }

    public class ListViewInventory
    {
        public Guid StoresId { get; set; }
        public string StoreName { get; set; }
        public decimal Cost { get; set; }
        public decimal Price { get; set; }
        public decimal InStock { get; set; }

    }
}
