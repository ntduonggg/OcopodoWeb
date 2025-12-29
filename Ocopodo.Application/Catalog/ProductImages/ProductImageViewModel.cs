using System;
using System.Collections.Generic;
using System.Text;

namespace Ocopodo.ViewModels.Catalog.ProductImages
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
    }
}
