using Mahzan.Mobile.API.Filters.Stores;
using Mahzan.Mobile.API.Results.Stores;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.Stores
{
    public interface IStoresService
    {
        Task<GetStoresResult> Get(GetStoresFilter filter);
    }
}
