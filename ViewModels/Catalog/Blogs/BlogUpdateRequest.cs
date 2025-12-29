using Ocopodo.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.ViewModels.Catalog.Blogs
{
    public class BlogUpdateRequest
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        public string ThumbnailUrl { get; set; }
        public Status Status { get; set; }
        public int ViewCount { set; get; }
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedById { get; set; }
    }
}
