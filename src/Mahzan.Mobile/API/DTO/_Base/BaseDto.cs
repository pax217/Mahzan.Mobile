using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.DTO._Base
{
    public class BaseDto
    {
        public Guid MembersId { get; set; }
        public Guid AspNetUserId { get; set; }
        //public TableAuditEnum TableAuditEnum { get; set; }

        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
    }
}
