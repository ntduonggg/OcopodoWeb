using Ocopodo.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.ViewModels.Catalog.Blogs
{
    public class BlogCreateRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string ThumbnailUrl { get; set; }
        public Status Status { get; set; }
    }
}
