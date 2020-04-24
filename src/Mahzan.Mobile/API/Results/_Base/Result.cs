using Mahzan.Mobile.API.Enums.Results;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Results._Base
{
    public class Result
    {
        public bool IsValid { get; set; }
        public int StatusCode { get; set; }
        public ResultTypeEnum ResultTypeEnum { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}
