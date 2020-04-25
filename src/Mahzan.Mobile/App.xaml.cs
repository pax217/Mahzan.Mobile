using Prism;
using Prism.Ioc;
using Mahzan.Mobile.ViewModels;
using Mahzan.Mobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Mahzan.Mobile.API.Interfaces.AspNetUsers;
using Mahzan.Mobile.API.Implementations.AspNetUsers;
using Mahzan.Mobile.SqLite.Interfaces;
using Mahzan.Mobile.SqLite.Implementations;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.Views.Members;
using Mahzan.Mobile.ViewModels.Members;
using Prism.DryIoc;
using Prism.Navigation;
using Mahzan.Mobile.Views.Members.WorkEnviroment;
using Mahzan.Mobile.ViewModels.Members.WorkEnviroment;
using Mahzan.Mobile.Views.Members.WorkEnviroment.Stores;
using Mahzan.Mobile.ViewModels.Members.WorkEnviroment.Stores;
using Mahzan.Mobile.API.Interfaces.Stores;
using Mahzan.Mobile.API.Implementations.Stores;
using Mahzan.Mobile.API.Interfaces.Companies;
using Mahzan.Mobile.API.Implementations.Companies;
using Mahzan.Mobile.Views.Members.Products;
using Mahzan.Mobile.ViewModels.Members.Products;
using Mahzan.Mobile.Views.Members.Sales;
using Mahzan.Mobile.ViewModels.Members.Sales;
using Mahzan.Mobile.Views.Members.Products.Inventory;
using Mahzan.Mobile.ViewModels.Members.Products.Inventory;
using Mahzan.Mobile.API.Implementations.Products;
using Mahzan.Mobile.API.Interfaces.Products;
using Mahzan.Mobile.API.Implementations.ProductCategories;
using Mahzan.Mobile.API.Interfaces.ProductCategories;
using Mahzan.Mobile.API.Implementations.ProductUnits;
using Mahzan.Mobile.API.Interfaces.ProductUnits;
using Mahzan.Mobile.API.Implementations.ProductsStore;
using Mahzan.Mobile.API.Interfaces.ProductsStore;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Mahzan.Mobile
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            //await NavigationService.NavigateAsync(nameof(Mobile.Views.MainPage) + "/" + nameof(NavigationPage) + "/" + nameof(LoginPage));
            await NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            //Services
            containerRegistry.Register<IAspNetUsersService, AspNetUsersService>();
            containerRegistry.Register<IStoresService, StoresService>();
            containerRegistry.Register<ICompaniesService, CompaniesService>();
            containerRegistry.Register<IProductsService, ProductsService>();
            containerRegistry.Register<IProductCategoriesService, ProductCategoriesService>();
            containerRegistry.Register<IProductUnitsService, ProductUnitsService>();
            containerRegistry.Register<IStoresService, StoresService>();
            containerRegistry.Register<IProductsStoreService, ProductsStoreService>();

            //Repository
            containerRegistry.Register<IRepository<AspNetUsers>, Repository<AspNetUsers>>();

            //Navigation
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<DashboardPage, DashboardPageViewModel>();
            containerRegistry.RegisterForNavigation<ViewA, ViewAViewModel>();
            containerRegistry.RegisterForNavigation<ViewB, ViewBViewModel>();
            containerRegistry.RegisterForNavigation<IndexWorkEnviromentPage, IndexWorkEnviromentPageViewModel>();
            containerRegistry.RegisterForNavigation<ListStoresPage, ListStoresPageViewModel>();
            containerRegistry.RegisterForNavigation<AdminStorePage, AdminStorePageViewModel>();
            containerRegistry.RegisterForNavigation<IndexProductsPage, IndexProductsPageViewModel>();
            containerRegistry.RegisterForNavigation<IndexSalesPage, IndexSalesPageViewModel>();
            containerRegistry.RegisterForNavigation<ListProductsPage, ListProductsPageViewModel>();
            containerRegistry.RegisterForNavigation<AddProductPage, AddProductPageViewModel>();
            containerRegistry.RegisterForNavigation<AddProductInventoryPage, AddProductInventoryPageViewModel>();
        }
    }
}
