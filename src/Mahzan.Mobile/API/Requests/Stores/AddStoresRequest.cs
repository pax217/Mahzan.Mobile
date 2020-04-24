using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Stores
{
    public class AddStoresRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Comment { get; set; }
        public Guid CompaniesId { get; set; }
    }
}
