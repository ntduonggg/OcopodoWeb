using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ocopodo.ViewModels.Catalog.ProductImages
{
    public class ProductImageCreateRequest
    {
        public bool IsDefault { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
