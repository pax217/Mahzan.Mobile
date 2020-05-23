using ImTools;
using Mahzan.Mobile.API.Entities;
using Mahzan.Mobile.API.Filters.Companies;
using Mahzan.Mobile.API.Filters.Tickets;
using Mahzan.Mobile.API.Interfaces.Companies;
using Mahzan.Mobile.API.Interfaces.Tickets;
using Mahzan.Mobile.API.Results.Companies;
using Mahzan.Mobile.API.Results.Tickets;
using Mahzan.Mobile.Services.Interfaces;
using Mahzan.Mobile.Utils.Tickets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.Services.Implementations
{
    public class PrintTicketService : IPrintTicketService
    {
        private const int LENGTH_ROW = 32;

        private readonly ICompaniesService _companiesService; 

        private readonly ITicketsService _ticketsService;

        public PrintTicketService(
            ICompaniesService companiesService,
            ITicketsService ticketsService)
        {
            _companiesService = companiesService;
            _ticketsService = ticketsService;
        }

        public async Task<StringBuilder> GetTicketToPrint(Guid companiesId,
                                                          Guid ticketsId)
        {
            StringBuilder stringBuilder = new StringBuilder();
            StringBuilder commercialName = new StringBuilder();
            StringBuilder productsDetail = new StringBuilder();
            try
            {
                //Nombre Comercial
                commercialName = await GetCommercialName(companiesId);

                //Dirección y Contacto

                //Detalle productos
                productsDetail = await GetProductsDetail(ticketsId);

                stringBuilder = BuildTicketString(commercialName.ToString(),
                                                  productsDetail.ToString());


            }
            catch (Exception ex)
            {

                throw;
            }


            return stringBuilder;
        }

        private StringBuilder BuildTicketString(string commercialName,
                                                string productsDetail) 
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("-------------------------------\n"); //Separador
            stringBuilder.Append(commercialName); //Nombre Comercial
            stringBuilder.Append("-------------------------------\n"); //Separador
            stringBuilder.Append("Cerrada del Jaguey N.1         \n"); //Calle y Nuemro
            stringBuilder.Append("San Andres Totoltepec          \n"); //Colonia
            stringBuilder.Append("Tlalpan,CDMX,C.P. 14400        \n"); //Delegación, Ciudad, CP
            stringBuilder.Append("Tel. +52 1 55 2020 5008        \n"); //Telefofno
            stringBuilder.Append("www.mahzan.com                 \n"); //Contacto
            stringBuilder.Append("-------------------------------\n"); //Separador
            stringBuilder.Append("Descripcion        C.     Monto\n"); //Cabecero Detalle de Ticket
            stringBuilder.Append("-------------------------------\n"); //Separador
            stringBuilder.Append(productsDetail); //Descripción de Articulo
            //stringBuilder.Append("-------------------------------\n");
            //stringBuilder.Append("              Subotal    109.65\n");
            //stringBuilder.Append("                  IVA      8.55\n");
            //stringBuilder.Append("                Total    118.20\n");
            //stringBuilder.Append("-------------------------------\n");
            //stringBuilder.Append("CIENTO DIEZ Y OCHO PESOS 20    \n");
            //stringBuilder.Append("/100MN                         \n");
            //stringBuilder.Append("-------------------------------\n");
            //stringBuilder.Append("EFECTIVO                       \n");
            //stringBuilder.Append("                Pago     200.00\n");
            //stringBuilder.Append("                Cambio    81.80\n");
            //stringBuilder.Append("-------------------------------\n");
            //stringBuilder.Append("# ARTICULOS. VENDIDOS 2        \n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append("     ***COPIA DE CLIENTE***    \n");
            stringBuilder.Append("-------------------------------\n");
            stringBuilder.Append(" *** Gracias por su compra *** \n");
            stringBuilder.Append("-------------------------------\n");

            return stringBuilder;
        }

        private async Task<StringBuilder> GetCommercialName(Guid companiesId)
        {
            StringBuilder result = new StringBuilder();

            GetCompaniesResult getCompaniesResult = await _companiesService
                                                          .Get(new GetCompaniesFilter
                                                          {
                                                              CompaniesId = companiesId
                                                          });

            if (getCompaniesResult.IsValid)
            {
                Companies company = getCompaniesResult.Companies.FirstOrDefault();

                //Commercial Name
                result.Append(FormatRow(company.CommercialName));
            }

            return result;
        }

        private async Task<StringBuilder> GetProductsDetail(Guid ticketsId) 
        {
            StringBuilder result = new StringBuilder();

            GetTicketResult getTicketResult = await _ticketsService
                                                    .GetById(ticketsId);
            if (getTicketResult.IsValid)
            {

                foreach (var ticketDetail in getTicketResult.Ticket.TicketDetails)
                {
                    result.Append(ticketDetail.Description + "\n");
                    result.Append(FormatQuantityAmountRow(ticketDetail.Quantity.ToString(), 
                                                         ticketDetail.Amount.ToString()));
                    result.Append("-------------------------------\n");
                }
            }

            //Total
            result.Append(FormatDescriptionMoneyRow("Total",getTicketResult.Ticket.Total.ToString()));

            result.Append("-------------------------------\n");

            //Total En Letra
            string totalLetter = CurrencyToLetter.Convertir(getTicketResult.Ticket.Total.ToString(),
                                                            true);
            result.Append(totalLetter + "\n");

            result.Append("-------------------------------\n");

            //Pago
            result.Append(FormatDescriptionMoneyRow("Pago", getTicketResult.Ticket.CashPayment.ToString()));

            //Cambio
            result.Append(FormatDescriptionMoneyRow("Cambio", getTicketResult.Ticket.CashExchange.ToString()));
            
            result.Append("-------------------------------\n");

            //Articulos Vendidos
            result.Append(FormatTotalProducts(getTicketResult.Ticket.TotalProducts.ToString()));

            return result;

        }


        private string FormatRow(string valueRow) 
        {
            StringBuilder stringBuilder = new StringBuilder();

            int length = valueRow.Length;
            int lenghtEmpty = LENGTH_ROW - length;
            decimal lengthLeftAndRigth = ((lenghtEmpty / 2) - 4);
            //decimal lengthLeftAndRigth = Math.Round(lengthMiddle, 
            //                                  MidpointRounding.AwayFromZero);

            stringBuilder.Append(' ', (int)lengthLeftAndRigth);
            stringBuilder.Append("- " + valueRow + " -");
            stringBuilder.Append(' ', (int)lengthLeftAndRigth);
            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }

        private string FormatQuantityAmountRow(string quantity,
                                              string amount) 
        {
            StringBuilder stringBuilder = new StringBuilder();

            int LENGTH_QUANTITY = 20;
            int WHITE_SAPCE_QUANTITY = LENGTH_QUANTITY - quantity.Length;

            stringBuilder.Append(' ', (int)WHITE_SAPCE_QUANTITY);
            stringBuilder.Append(quantity);

            int LENGTH_PRICE = 11;
            int WHITE_SAPCE_PRICE = LENGTH_PRICE - amount.Length;

            stringBuilder.Append(' ', (int)WHITE_SAPCE_PRICE);
            stringBuilder.Append(amount);

            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }

        private string FormatDescriptionMoneyRow(string description,string money)
        {
            StringBuilder stringBuilder = new StringBuilder();

            int LENGTH_DESCRIPTION = 20;
            int WHITE_SAPCE_DESCRIPTION = LENGTH_DESCRIPTION - description.Length;

            stringBuilder.Append(' ', (int)WHITE_SAPCE_DESCRIPTION);
            stringBuilder.Append(description);

            int LENGTH_TOTAL = 11;
            int WHITE_SAPCE_MONEY = LENGTH_TOTAL - money.Length;

            stringBuilder.Append(' ', (int)WHITE_SAPCE_MONEY);
            stringBuilder.Append(money);
            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }

        private string FormatTotalProducts(string totalProducts) 
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append("# ARTICULOS VENDIDOS");

            int LENGTH_TOTAL_PRODUCTS = 11;
            int WHITE_SAPCE_TOTAL_PRODUCTS = LENGTH_TOTAL_PRODUCTS - totalProducts.Length;

            stringBuilder.Append(' ', (int)WHITE_SAPCE_TOTAL_PRODUCTS);
            stringBuilder.Append(totalProducts);
            stringBuilder.Append("\n");

            return stringBuilder.ToString();
        }
    }
}
