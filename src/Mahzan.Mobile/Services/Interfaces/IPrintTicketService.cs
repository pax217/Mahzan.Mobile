using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.Services.Interfaces
{
    public interface IPrintTicketService
    {
        Task<StringBuilder> GetTicketToPrint(Guid companiesId,
                                             Guid ticketsId);
    }
}
