using Microsoft.AspNetCore.Http;
using Ocopodo.Data.Entities;
using Ocopodo.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.ViewModels.Catalog.Products
{
    public class ProductCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int ViewCount { set; get; }
        public int CategoryId { get; set; }
        //public List<SelectItem> ProductCategories { get; set; } = new List<SelectItem>();
        public IFormFile MainImageUrl { get; set; }
    }
}
