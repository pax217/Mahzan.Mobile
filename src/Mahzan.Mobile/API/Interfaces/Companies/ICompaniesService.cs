using Mahzan.Mobile.API.Filters.Companies;
using Mahzan.Mobile.API.Results.Companies;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Mahzan.Mobile.API.Interfaces.Companies
{
    public interface ICompaniesService
    {
        Task<GetCompaniesResult> Get(GetCompaniesFilter filter);
    }
}
