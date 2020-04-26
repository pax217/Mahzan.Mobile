using Mahzan.Mobile.Models;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
using Mahzan.Mobile.Views;
using Mahzan.Mobile.Views.Members.Products;
using Mahzan.Mobile.Views.Members.Sales;
using Mahzan.Mobile.Views.Members.WorkEnviroment;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private INavigationService _navigationService;

        private readonly IRepository<AspNetUsers> _aspNetUsersRepository;
        public ObservableCollection<MyMenuItem> MenuItems { get; set; }

        private MyMenuItem selectedMenuItem;
        public MyMenuItem SelectedMenuItem
        {
            get => selectedMenuItem;
            set => SetProperty(ref selectedMenuItem, value);
        }

        public DelegateCommand NavigateCommand { get; private set; }

        private string _role { get; set; }
        public string Role
        {
            get => _role;
            set
            {
                _role = value;
                OnPropertyChanged(nameof(Role));
            }
        }

        private string _userName { get; set; }
        public string UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged(nameof(UserName));
            }
        }

        private string _storeName { get; set; }
        public string StoreName
        {
            get => _storeName;
            set
            {
                _storeName = value;
                OnPropertyChanged(nameof(StoreName));
            }
        }

        private string _pontOfSale { get; set; }
        public string PontOfSale
        {
            get => _pontOfSale;
            set
            {
                _pontOfSale = value;
                OnPropertyChanged(nameof(PontOfSale));
            }
        }

        public MainPageViewModel(
            INavigationService navigationService,
            IRepository<AspNetUsers> aspNetUsersRepository)
        {
            //Navigation
            _navigationService = navigationService;

            //Repository
            _aspNetUsersRepository = aspNetUsersRepository;



            Task.Run(() => BuildMenu());


            NavigateCommand = new DelegateCommand(Navigate);
        }

        async void BuildMenu() 
        {
            MenuItems = new ObservableCollection<MyMenuItem>();

            List<AspNetUsers> aspNetUsers = await _aspNetUsersRepository.Get();

            Role = aspNetUsers.FirstOrDefault().Role;
            UserName = aspNetUsers.FirstOrDefault().UserName;
            StoreName = aspNetUsers.FirstOrDefault().StoreName;
            PontOfSale = aspNetUsers.FirstOrDefault().PointOfSaleName;

            switch (aspNetUsers.FirstOrDefault().Role)
            {
                case "MEMBER":
                    MenuItems = BuildMenuMember();
                    break;
                default:
                    break;
            }
        }


        private ObservableCollection<MyMenuItem> BuildMenuMember()
        {
            ObservableCollection<MyMenuItem> result = new ObservableCollection<MyMenuItem>();

            result.Add(new MyMenuItem()
            {
                Icon = "ic_viewa",
                PageName = nameof(IndexSalesPage),
                Title = "Ventas",
            });

            result.Add(new MyMenuItem()
            {
                Icon = "ic_viewb",
                PageName = nameof(IndexProductsPage),
                Title = "Productos",
            });


            result.Add(new MyMenuItem()
            {
                Icon = "ic_viewb",
                PageName = nameof(IndexWorkEnviromentPage),
                Title = "Entorno de Trabajo",
            });

            result.Add(new MyMenuItem()
            {
                Icon = "ic_viewb",
                PageName = nameof(IndexWorkEnviromentPage),
                Title = "Configuración",
            });

            result.Add(new MyMenuItem()
            {
                Icon = "ic_viewb",
                PageName = nameof(LoginPage),
                Title = "Salir",
                ExitAplication = true
            });

            return result;
        }

        async void Navigate()
        {
            if (!SelectedMenuItem.ExitAplication)
            {
                await _navigationService.NavigateAsync(nameof(NavigationPage) + "/" + SelectedMenuItem.PageName);
            }
            else 
            {
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}
