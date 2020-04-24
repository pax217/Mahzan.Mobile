using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Requests.Products.Post
{
    public class PostProductPhotoRequest
    {
        public string Title { get; set; }

        public string MIMEType { get; set; }

        public string Base64 { get; set; }
    }
}
