using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.PaymentTypes;
using Mahzan.Mobile.API.Interfaces.PaymentTypes;
using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Requests.Tickets;
using Mahzan.Mobile.API.Results.PaymentTypes;
using Mahzan.Mobile.API.Results.Tickets;
using Mahzan.Mobile.Models.Members.Sales.NewSale;
using Mahzan.Mobile.SqLite.Entities;
using Mahzan.Mobile.SqLite.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Mahzan.Mobile.ViewModels.Members.Sales.NewSale
{
    public class ChargeTicketPageViewModel : BindableBase, INavigationAware
    {
        private readonly INavigationService _navigationService;

        private readonly IPaymentTypesService _paymentTypesService;

        private readonly ITicketsService _ticketsService;

        //SQlite
        private readonly IRepository<AspNetUsers> _aspNetUsersRepository;
        private AspNetUsers aspNetUser;

        //Models
        private List<TicketDetail> ListTicketDetail = new List<TicketDetail>();

        private ChargeTicketDetail ChargeTicketDetail = new ChargeTicketDetail();

        //CashPayment
        public decimal CashPayment { get; set; }


        private string _total;
        public string Total
        {
            get => _total;
            set
            {
                if (_total != value)
                {
                    _total = value;
                    OnPropertyChanged(nameof(Total)); // Notify that there was a change on this property
                }
            }
        }

        //Client
        private Guid? ClientsId { get; set; }

        //Picker
        private ObservableCollection<PaymentTypes> _paymentTypes;
        public ObservableCollection<PaymentTypes> PaymentTypes
        {
            get => _paymentTypes;
            set => SetProperty(ref _paymentTypes, value);
        }
        //Selected ProductUnit
        private PaymentTypes _selectedPaymentType;
        public PaymentTypes SelectedPaymentType
        {
            get => _selectedPaymentType;
            set
            {
                if (_selectedPaymentType != value)
                {
                    _selectedPaymentType = value;
                }
            }
        }

        public ICommand EndSaleCommand { get; set; }

        public ChargeTicketPageViewModel(
            INavigationService navigationService,
            IPaymentTypesService paymentTypesService,
            ITicketsService ticketsService,
            IRepository<AspNetUsers> aspNetUsersRepository)
        {
            //Services
            _navigationService = navigationService;
            _paymentTypesService = paymentTypesService;
            _ticketsService = ticketsService;

            //SQlite
            _aspNetUsersRepository = aspNetUsersRepository;

            //Comands
            EndSaleCommand = new Command(async () => await OnEndSaleCommand());

            //Pickers
            Task.Run(() => GetPaymentTypes());

            Initialize();
        }

        private async void Initialize()
        {
            List<AspNetUsers> listAspNetUsers;
            listAspNetUsers = await _aspNetUsersRepository
                                    .Get();

            aspNetUser = listAspNetUsers.FirstOrDefault();
        }


        private async Task GetPaymentTypes()
        {
            GetPaymentTypesResult getPaymentTypesResult;
            getPaymentTypesResult = await _paymentTypesService
                                          .Get(new GetPaymentTypesFilter
                                          {

                                          });

            if (getPaymentTypesResult.IsValid)
            {
                PaymentTypes = new ObservableCollection<PaymentTypes>(getPaymentTypesResult.PaymentTypes);

            }
            else
            {
                await Application
                      .Current
                      .MainPage
                      .DisplayAlert(getPaymentTypesResult.Title,
                                    getPaymentTypesResult.Message,
                                    "ok");
            }
        }

        private async Task OnEndSaleCommand()
        {
            PostTicketCloseSaleResult postTicketCloseSaleResult;
            postTicketCloseSaleResult = await _ticketsService
                                             .TicketCloseSale(BuildPostTicketCalculationRequest());

            if (postTicketCloseSaleResult.IsValid)
            {
                await BuildChargeTicketDetail(postTicketCloseSaleResult);

                var navigationParams = new NavigationParameters();
                navigationParams.Add("chargeTicketDetail", ChargeTicketDetail);
                navigationParams.Add("closeTicketsId", ChargeTicketDetail);
                await _navigationService.NavigateAsync("EndSalePage", navigationParams);

                //await Application.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new EndSalePage()));
            }
            else
            {

                await Application
                        .Current
                        .MainPage
                        .DisplayAlert(postTicketCloseSaleResult.Title,
                                      postTicketCloseSaleResult.Message,
                                      "ok");
            }


        }

        private PostTicketCalculationRequest BuildPostTicketCalculationRequest()
        {
            PostTicketCalculationRequest result = new PostTicketCalculationRequest();


            //Info de Tienda y TPV
            result.StoresId = aspNetUser.StoresId;
            result.PointsOfSalesId = aspNetUser.PointsOfSalesId;
            result.PaymentTypesId = _selectedPaymentType.PaymentTypesId;
            result.CashPayment = CashPayment;
            result.ClientsId = ClientsId;


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

        private async Task BuildChargeTicketDetail(PostTicketCloseSaleResult postTicketCloseSaleResult)
        {
            ChargeTicketDetail.TicketsClosedId = postTicketCloseSaleResult.Ticket.TicketsId;
            ChargeTicketDetail.Total = postTicketCloseSaleResult.Ticket.Total;
            ChargeTicketDetail.CashPayment = postTicketCloseSaleResult.Ticket.CashPayment;
            ChargeTicketDetail.CashExchange = postTicketCloseSaleResult.Ticket.CashExchange;
            ChargeTicketDetail.TotalProducts = postTicketCloseSaleResult.Ticket.TotalProducts;
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            //Debug.WriteLine(parameters);
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ListTicketDetail = parameters.GetValue<List<TicketDetail>>("ticketDetail");
            Total = parameters.GetValue<string>("ticketTotal");
            ClientsId = parameters.GetValue<Guid?>("clientsId");

            if (ListTicketDetail != null)
            {
                //await GetCompany(StoresId.Value);
            }
        }


    }
}
