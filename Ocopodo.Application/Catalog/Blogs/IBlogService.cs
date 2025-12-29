using Ocopodo.ViewModels.Catalog.Blogs;
using Ocopodo.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Application.Catalog.Blogs
{
    public interface IBlogService
    {
        Task<int> Create(BlogCreateRequest request);

        Task<int> Update(BlogUpdateRequest request);
        
        Task<int> Delete(int productId);

        Task AddViewCount(int productID);

        Task<PagedResult<BlogViewModel>> GetAllPaging(GetBlogPagingRequest request);
    }
}
