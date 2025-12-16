using Ocopodo.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Data.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string ThumbnailUrl { get; set; }
        public Status Status { get; set; }
        public int? CategoryId { get; set; }
        public BlogCategory Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? CreatedById { get; set; } // optional link to admin user
        public User CreatedBy { get; set; }
    }
}
