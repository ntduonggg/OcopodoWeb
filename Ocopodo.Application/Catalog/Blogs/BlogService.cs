using Microsoft.EntityFrameworkCore;
using Ocopodo.Data.EF;
using Ocopodo.Data.Entities;
using Ocopodo.ViewModels.Catalog.Blogs;
using Ocopodo.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ocopodo.Application.Catalog.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly OcopodoDBContext _context;
        //private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public BlogService(OcopodoDBContext context)
        {
            _context = context;
            //_storageService = storageService;
        }
        public async Task<int> Create(BlogCreateRequest request)
        {
            var blog = new Blog()
            {
                Title = request.Title,
                Content = request.Content,
                ThumbnailUrl = request.ThumbnailUrl,
                Status = Data.Enums.Status.Active,
                ViewCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();
            return blog.Id;
        }

        public async Task<int> Update(BlogUpdateRequest request)
        {
            var blog = await _context.Blogs
            .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (blog == null)
                throw new Exception("Blog not found");

            blog.Title = request.Title;
            blog.Content = request.Content;
            blog.ThumbnailUrl = request.ThumbnailUrl;
            blog.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();
            return blog.Id;
        }

        public async Task<int> Delete(int blogId)
        {
            var blog = _context.Products.FindAsync(blogId);
            if (blog == null) throw new Exception($"Cannot find a blog with id: {blogId}");
            _context.Products.Remove(await blog);
            return await _context.SaveChangesAsync();

        }

        public async Task AddViewCount(int blogId)
        {
            var blog = await _context.Products.FindAsync(blogId);
            blog.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<BlogViewModel>> GetAllPaging(GetBlogPagingRequest request)
        {
            var query = from b in _context.Blogs
                        select new { b };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.b.Title.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.b.CreatedAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new BlogViewModel
                {
                    Id = x.b.Id,
                    Title = x.b.Title,
                    Content = x.b.Content,
                    ViewCount = x.b.ViewCount,
                    CreatedAt = x.b.CreatedAt,
                    UpdatedAt = x.b.UpdatedAt,
                    CreatedBy = x.b.CreatedBy,
                }).ToListAsync();

            var pagedResult = new PagedResult<BlogViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }
    }
}
