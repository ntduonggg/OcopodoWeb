using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Data.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Slug { get; set; }
        public int? ParentId { get; set; }
        public ProductCategory Parent { get; set; }
        public ICollection<ProductCategory> Children { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
