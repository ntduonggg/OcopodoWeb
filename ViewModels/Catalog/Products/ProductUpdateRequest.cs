using Microsoft.AspNetCore.Http;
using Ocopodo.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.ViewModels.Catalog.Products
{
    public class ProductUpdateRequest
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Status Status { get; set; }
        public int ViewCount { set; get; }
        public DateTime? UpdatedAt { get; set; }
        public IFormFile MainImageUrl { get; set; }
    }
}
