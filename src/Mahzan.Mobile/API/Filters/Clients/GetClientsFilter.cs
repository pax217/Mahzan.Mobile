
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Filters.Clients
{
    public class GetClientsFilter
    {
        public Guid? ClientsId { get; set; }

        public string RFC { get; set; }

        public string BusinessName { get; set; }
    }
}
