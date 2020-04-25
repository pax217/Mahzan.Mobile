using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.ProductCategories;
using Mahzan.Mobile.API.Filters.Products;
using Mahzan.Mobile.API.Filters.ProductUnits;
using Mahzan.Mobile.API.Interfaces.ProductCategories;
using Mahzan.Mobile.API.Interfaces.Products;
using Mahzan.Mobile.API.Interfaces.ProductUnits;
using Mahzan.Mobile.API.Requests.Products.Post;
using Mahzan.Mobile.API.Results.ProductCategories;
using Mahzan.Mobile.API.Results.Products;
using Mahzan.Mobile.API.Results.ProductUnits;
using Mahzan.Mobile.QrScanning;
using Mahzan.Mobile.Utils.Images;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Products.Inventory
{
    public class AddProductPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IProductCategoriesService _productCategoriesService;

        private readonly IProductUnitsService _productUnitsService;

        private readonly IProductsService _productsService;

        #region Properties

        private Guid? ProductsId { get; set; }

        //Image
        private ImageSource _productImageSource;
        public ImageSource ProductImageSource
        {
            get => _productImageSource;
            set => SetProperty(ref _productImageSource, value);
        }

        //BarCode
        private string _barCode;
        public string BarCode
        {
            get => _barCode;
            set => SetProperty(ref _barCode, value);
        }

        //SKU
        private string _sku;
        public string SKU
        {
            get => _sku;
            set => SetProperty(ref _sku, value);
        }


        //Description

        private string _description;
        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        //Price
        private decimal _price;
        public decimal Price
        {
            get => _price;
            set => SetProperty(ref _price, value);
        }

        //Cost

        private decimal? _cost;
        public decimal? Cost
        {
            get => _cost;
            set => SetProperty(ref _cost, value);
        }

        //Pickers
        private ObservableCollection<ProductCategories> _productCategories;
        public ObservableCollection<ProductCategories> ProductCategories
        {
            get => _productCategories;
            set => SetProperty(ref _productCategories, value);
        }

        private ObservableCollection<ProductUnits> _productUnits;
        public ObservableCollection<ProductUnits> ProductUnits
        {
            get => _productUnits;
            set => SetProperty(ref _productUnits, value);
        }

        //Selected ProductUnitCategory
        private ProductCategories _selectedProductCategory;
        public ProductCategories SelectedProductCategory
        {
            get => _selectedProductCategory;
            set
            {
                if (_selectedProductCategory != value)
                {
                    _selectedProductCategory = value;
                    HandleSelectedProductCategory();
                }
            }
        }

        //Selected ProductUnit
        private ProductUnits _selectedProductUnit;
        public ProductUnits SelectedProductUnit
        {
            get => _selectedProductUnit;
            set
            {
                if (_selectedProductUnit != value)
                {
                    _selectedProductUnit = value;
                    HandleSelectedProductUnit();
                }
            }
        }

        //Path Image Product

        private string PathImageProduct { get; set; }

        private void HandleSelectedProductUnit()
        {
            Debug.WriteLine(_selectedProductUnit.ProductUnitsId);
        }


        #endregion


        #region Commands

        public ICommand OpenCameraCommand { get; set; }
        public ICommand OpenBarCodeCommand { get; set; }
        public ICommand CreateProductCommand { get; set; }


        #endregion

        public AddProductPageViewModel(
            INavigationService navigationService,
            IProductCategoriesService productCategoriesService,
            IProductUnitsService productUnitsService,
            IProductsService productsService)
        {
            _navigationService = navigationService;

            //Service
            _productCategoriesService = productCategoriesService;
            _productUnitsService = productUnitsService;
            _productsService = productsService;

            //Pickers
            Task.Run(() => GetProductCategories());
            Task.Run(() => GetProductUnits());

            //Commands
            OpenCameraCommand = new Command(async () => await OnOpenCameraCommand());
            OpenBarCodeCommand = new Command(async () => await OnOpenBarCodeCommand());
            CreateProductCommand = new Command(async () => await OnCreateProductCommand());

            //Initialize
            Initialize();
        }

        #region Private Methods
        private async Task OnCreateProductCommand()
        {
            PostProductsRequest postProductsRequest = new PostProductsRequest
            {
                PostProductPhotoRequest = new PostProductPhotoRequest
                {
                    Title = Path.GetFileNameWithoutExtension(PathImageProduct).Replace("_", ""),
                    MIMEType = ImagesUtil.GetMIMEType(Path.GetExtension(PathImageProduct)),
                    Base64 = ImagesUtil.ConvertImageBase64(PathImageProduct)
                },
                PostProductDetailRequest = new PostProductDetailRequest
                {
                    ProductCategoriesId = _selectedProductCategory.ProductCategoriesId,
                    ProductUnitsId = _selectedProductUnit.ProductUnitsId,
                    SKU = SKU,
                    Barcode = BarCode,
                    Description = Description,
                    Price = Price,
                    Cost = Cost
                }
            };

            if (ProductsId == Guid.Empty)
            {
                //Insert
                PostProductsResult postProductsResult;
                postProductsResult = await _productsService
                                           .Post(postProductsRequest);

                if (postProductsResult.IsValid)
                {

                    var navigationParams = new NavigationParameters();
                    navigationParams.Add("productsId", postProductsResult.Product.ProductsId);

                    await _navigationService.NavigateAsync("AddProductInventoryPage", navigationParams);
                }
                else
                {
                    await Application
                    .Current
                    .MainPage
                    .DisplayAlert(postProductsResult.Title,
                                  postProductsResult.Message,
                                  "ok");
                }
            }
            else 
            {
                var navigationParams = new NavigationParameters();
                navigationParams.Add("productsId", ProductsId.Value);

                await _navigationService.NavigateAsync("AddProductInventoryPage", navigationParams);
            }




        }

        private async Task GetProductUnits()
        {
            GetProductUnitsResult getProductUnitsResult;
            getProductUnitsResult = await _productUnitsService
                                    .Get(new GetProductUnitsFilter
                                    {

                                    });

            if (getProductUnitsResult.IsValid)
            {
                ProductUnits = new ObservableCollection<ProductUnits>(getProductUnitsResult.ProductUnits);

            }
            else
            {
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert(getProductUnitsResult.Title,
                                    getProductUnitsResult.Message,
                                    "ok");
            }

        }

        private void Initialize()
        {
            var noAvailableImage = new Image { Aspect = Aspect.AspectFit };
            noAvailableImage.Source = ImageSource.FromFile("image_no_available.png");

            ProductImageSource = noAvailableImage.Source;
        }

        private async Task OnOpenBarCodeCommand()
        {
            var scanner = DependencyService.Get<IQrScanningService>();
            var result = await scanner.ScanAsync();

            if (result != null)
            {
                BarCode = result;
            }
        }

        private async Task OnOpenCameraCommand()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert("No Camera", "No Camera available", "Ok");
            }

            var file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                    PhotoSize = PhotoSize.Small,
                    SaveToAlbum = false
                }
            );

            if (file == null)
            {
                return;
            }
            else
            {
                PathImageProduct = file.AlbumPath;
            }

            ProductImageSource = ImageSource.FromStream(() => {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            }
            );
        }

        private async Task GetProductCategories()
        {
            GetProductCategoriesResult getProductCategoriesResult;
            getProductCategoriesResult = await _productCategoriesService
                                               .Get(new GetProductCategoriesFilter
                                               {

                                               });

            if (getProductCategoriesResult.IsValid)
            {
                ProductCategories = new ObservableCollection<ProductCategories>(getProductCategoriesResult.ProductCategories);

            }
            else
            {
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert(getProductCategoriesResult.Title,
                                    getProductCategoriesResult.Message,
                                    "ok");
            }
        }

        private void HandleSelectedProductCategory()
        {
            Debug.WriteLine(_selectedProductCategory.ProductCategoriesId);
        }

        public async void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            ProductsId = parameters.GetValue<Guid>("productsId");

            if (ProductsId !=Guid.Empty)
            {
                await GetProduct(ProductsId.Value);
            }
        }

        public async Task GetProduct(Guid productsId) 
        {
            GetProductsResult result = await _productsService
                                             .Get(new GetProductsFilter
                                            {
                                               ProductsId = productsId
                                             });

            if (result.IsValid)
            {
                API.Entities.Products product = result.Products.FirstOrDefault();

                var bytes = Convert.FromBase64String(product.ProductsPhotos.Base64);
                ProductImageSource = ImageSource.FromStream(() => new MemoryStream(bytes));

                SKU = product.SKU;
                BarCode = product.Barcode;
                Description = product.Description;
                SelectedProductCategory = ProductCategories
                                          .SingleOrDefault(x => x.ProductCategoriesId == product.ProductCategoriesId);
                SelectedProductUnit = ProductUnits
                                      .SingleOrDefault(x => x.ProductUnitsId == product.ProductUnitsId);

                Price = product.Price;
                Cost = product.Cost;
            }
        }

        #endregion
    }
}
