using Mahzan.Mobile.API.Filters.Clients;
using Mahzan.Mobile.API.Requests.Clients;
using Mahzan.Mobile.API.Results.Clients;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.Clients
{
    public interface IClientsService
    {
        Task<PostClientsResult> Post(PostClientsRequest request);

        Task<GetClientsResult> Get(GetClientsFilter filter);
    }
}
