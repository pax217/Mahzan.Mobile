using System;
using System.Collections.Generic;
using System.Text;

namespace Mahzan.Mobile.API.Commands.Products.CreateProduct
{
    public class CreateProductTaxesCommand
    {
        public decimal TaxRate { get; set; }
        public Guid TaxesId { get; set; }
    }
}
