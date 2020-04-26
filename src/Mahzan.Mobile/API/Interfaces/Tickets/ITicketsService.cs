using Mahzan.Mobile.API.Requests.Tickets;
using Mahzan.Mobile.API.Results.Tickets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.Tickets
{
    public interface ITicketsService
    {
        Task<PostTicketCalculationResult> TicketCalculation(PostTicketCalculationRequest postTicketCalculationRequest);

        Task<PostTicketCloseSaleResult> TicketCloseSale(PostTicketCalculationRequest postTicketCalculationRequest);
    }
}
