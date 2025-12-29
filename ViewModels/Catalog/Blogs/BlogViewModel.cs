using Ocopodo.Data.Entities;
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
    public class BlogViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string ThumbnailUrl { get; set; }
        public Status Status { get; set; }
        public int ViewCount { set; get; }
        public int? CategoryId { get; set; }
        public BlogCategory Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedById { get; set; } // optional link to admin user
        public User CreatedBy { get; set; }
    }
}
