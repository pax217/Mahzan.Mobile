using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.Products;
using Mahzan.Mobile.API.Interfaces.Products;
using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Requests.Tickets;
using Mahzan.Mobile.API.Results.Products;
using Mahzan.Mobile.API.Results.Tickets;
using Mahzan.Mobile.QrScanning;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
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

namespace Mahzan.Mobile.ViewModels.Members.Sales.NewSale
{
    public class NewSalePageViewModel : BindableBase
    {
        //Services
        private readonly INavigationService _navigationService;

        private readonly IProductsService _productsService;

        private readonly ITicketsService _ticketsService;



        //Models
        private List<TicketDetail> ListTicketDetail = new List<TicketDetail>();

        //SQLite
        private readonly IRepository<AspNetUsers> _aspNetUsersRepository;
        private AspNetUsers aspNetUser;

        private ObservableCollection<ListViewTicketDetail> _listViewTicketDetail { get; set; }

        public ObservableCollection<ListViewTicketDetail> ListViewTicketDetail
        {
            get => _listViewTicketDetail;
            set
            {
                _listViewTicketDetail = value;
                OnPropertyChanged(nameof(ListViewTicketDetail));
            }
        }

        private string _total { get; set; }

        public string Total
        {
            get => _total;
            set
            {
                _total = value;
                OnPropertyChanged(nameof(Total));
            }
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

        public ICommand ButtonBarCodeCommand { get; private set; }

        public ICommand ButtonEraseTiketCommand { get; private set; }

        public ICommand ChargeTicketCommand { get; private set; }

        public NewSalePageViewModel(
            INavigationService navigationService,
            IProductsService productsService,
            ITicketsService ticketsService,
            IRepository<AspNetUsers> aspNetUsersRepository)
        {
            //Services
            _navigationService = navigationService;
            _productsService = productsService;
            _ticketsService = ticketsService;

            //Repository
            _aspNetUsersRepository = aspNetUsersRepository;

            //Comands
            ButtonBarCodeCommand = new Command(async () => await OnButtonBarCodeCommand());
            ButtonEraseTiketCommand = new Command(async () => await OnButtonEraseTiketCommand());
            ChargeTicketCommand = new Command(async () => await OnChargeTicketCommand());

            ListViewTicketDetail = new ObservableCollection<ListViewTicketDetail>();

            Task.Run(() => Initialize());
            //Task.Run(() => UpdateTicket());
        }
        private async void Initialize()
        {
            List<AspNetUsers> listAspNetUsers;
            listAspNetUsers = await _aspNetUsersRepository
                                    .Get();

            aspNetUser = listAspNetUsers.FirstOrDefault();
        }

        private async Task OnChargeTicketCommand()
        {
            var navigationParams = new NavigationParameters();
            navigationParams.Add("ticketDetail", ListTicketDetail);
            navigationParams.Add("ticketTotal", Total);
            await _navigationService.NavigateAsync("ChargeTicketPage", navigationParams);
            //await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ChargeTicketPage()));
        }

        private async Task OnButtonEraseTiketCommand()
        {
            Device.BeginInvokeOnMainThread(async () => {
                var result = await Application.Current.MainPage.DisplayAlert("Atención!", "¿Estas seguro de borrar el ticket?", "Si", "No");
                if (result)
                {

                    //_ticketDetailSqlite.DeleteAll();


                    ListViewTicketDetail.Clear();

                    Device.BeginInvokeOnMainThread(() => {
                        Total = "";
                        TotalProducts = "";
                    });
                }
            });
        }

        private async Task OnButtonBarCodeCommand()
        {

            var scanner = DependencyService.Get<IQrScanningService>();
            var result = await scanner.ScanAsync();

            if (result != "")
            {
                GetProductsResult getProductsResult = await _productsService
                                                            .Get(new GetProductsFilter
                                                            {
                                                                Barcode = result
                                                            });

                if (getProductsResult.IsValid)
                {
                    //Agrega Producto a tikcet

                    AddProductToTicket(getProductsResult.Products.FirstOrDefault());

                    //await Application.Current.MainPage.DisplayAlert("Producto Encontrado",
                    //                                                getProductsResult.Products.FirstOrDefault().Description,
                    //                                                "ok");

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Producto No Encontrado",
                                                                    string.Format("El producto con código de barras {0} no ha sido encontrado.", result),
                                                                    "ok");
                }
            }


        }

        private void AddProductToTicket(API.Entities.Products products)
        {


            TicketDetail existProduct = (from td in ListTicketDetail
                                         where td.ProductsId == products.ProductsId
                                         select td)
                                        .FirstOrDefault();

            if (existProduct == null)
            {
                ListTicketDetail.Add(new TicketDetail
                {
                    TicketDetailId = Guid.NewGuid(),
                    ProductsId = products.ProductsId,
                    Quantity = 1,
                    Description = products.Description,
                    Price = products.Price,
                    Amount = products.Price
                }
                );
            }
            else 
            {
                existProduct.Quantity++;
            }


            //SqLite.Entities.TicketDetail ticketDetail = _ticketDetailSqlite.Get(products.ProductsId);


            //if (ticketDetail == null)
            //{
            //    _ticketDetailSqlite.Insert(new SqLite.Entities.TicketDetail
            //    {
            //        TicketDetailId = Guid.NewGuid(),
            //        ProductsId = products.ProductsId,
            //        Quantity = 1,
            //        Description = products.Description,
            //        Price = products.Price,
            //        Amount = products.Price
            //    });
            //}
            //else
            //{
            //    ticketDetail.Quantity++;

            //    _ticketDetailSqlite.Update(ticketDetail);
            //}

            UpdateTicket();
        }

        private async void UpdateTicket()
        {

            PostTicketCalculationResult postTicketCalculationResult;
            postTicketCalculationResult = await _ticketsService
                                                .TicketCalculation(BuildPostTicketCalculationRequest());

            if (postTicketCalculationResult.IsValid)
            {
                HanldeTicketTotalSqlite(postTicketCalculationResult.Total);

                Device.BeginInvokeOnMainThread(() => {
                    Total = postTicketCalculationResult.Total.ToString();
                    TotalProducts = postTicketCalculationResult.TotalProducts.ToString();
                });


                ListViewTicketDetail.Clear();

                foreach (var ticketDetail in postTicketCalculationResult.PostTicketDetailDto)
                {
                    ListViewTicketDetail.Add(new ListViewTicketDetail
                    {
                        Description = ticketDetail.Description,
                        Quantity = ticketDetail.Quantity,
                        Price = ticketDetail.Price,
                        Amount = ticketDetail.Amount

                    });
                }
            }
            else
            {
                //await Application
                //      .Current
                //      .MainPage
                //      .DisplayAlert(postTicketCalculationResult.Title,
                //                    postTicketCalculationResult.Message, 
                //                    "Ok");
            }




        }

        private PostTicketCalculationRequest BuildPostTicketCalculationRequest()
        {
            PostTicketCalculationRequest result = new PostTicketCalculationRequest();


            //List<AspNetUsers> aspNetUsers = _aspNetUsersSqlite.Get();

            //Info de Tienda y TPV
            result.StoresId = aspNetUser.StoresId;
            result.PointsOfSalesId = aspNetUser.PointsOfSalesId;
            result.PaymentTypesId = Guid.Empty;

            ////Info de Productos
            //List<TicketDetail> ticketDetail = _ticketDetailSqlite.Get();

            if (ListTicketDetail.Any())
            {
                result.PostTicketCalculationDetailRequest = new List<PostTicketCalculationDetailRequest>();
                foreach (var detail in ListTicketDetail)
                {
                    result.PostTicketCalculationDetailRequest.Add(new PostTicketCalculationDetailRequest
                    {
                        ProductsId = detail.ProductsId,
                        Quantity = detail.Quantity
                    });
                }
            }



            return result;
        }

        private void HanldeTicketTotalSqlite(decimal total)
        {
            //List<SqLite.Entities.Tickets> tickets = _ticketSqlite.Get();

            //if (tickets.Any())
            //{
            //    tickets.FirstOrDefault().Total = total;
            //    _ticketSqlite.Update(tickets.FirstOrDefault());

            //}
            //else
            //{
            //    _ticketSqlite.Insert(new Tickets
            //    {
            //        TicketsId = Guid.NewGuid(),
            //        Total = total
            //    }); ;
            //}
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            throw new NotImplementedException();
        }
    }

    public class ListViewTicketDetail
    {

        public int Quantity { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }
        public decimal Amount { get; set; }
    }
}
