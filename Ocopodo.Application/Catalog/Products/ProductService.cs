using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ocopodo.Application.Common;
using Ocopodo.Data.EF;
using Ocopodo.Data.Entities;
using Ocopodo.ViewModels.Catalog.ProductImages;
using Ocopodo.ViewModels.Catalog.Products;
using Ocopodo.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Ocopodo.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly OcopodoDBContext _context;
        private readonly IStorageService _storageService;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";
        public ProductService(OcopodoDBContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Status = Data.Enums.Status.Active,
                ViewCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };
            if (request.MainImageUrl != null)
            {
                product.MainImageUrl = await this.SaveFile(request.MainImageUrl);
                //product.Images = new List<ProductImage>()
                //{
                //    new ProductImage()
                //    {
                //        ImageUrl = await this.SaveFile(request.MainImageUrl),
                //        SortOrder = 1
                //    }
                //};
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products
            .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (product == null)
                throw new Exception("Product not found");

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;

            if (request.MainImageUrl != null)
            {
                var MainImageUrl = await _context.ProductImages.FirstOrDefaultAsync(i => i.IsDefault == true && i.ProductId == request.Id);
                if (MainImageUrl != null)
                {
                    product.MainImageUrl = await this.SaveFile(request.MainImageUrl);
                    _context.ProductImages.Update(MainImageUrl);
                }
            }

            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = _context.Products.FindAsync(productId);
            if (product == null) throw new Exception($"Cannot find a product with id: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImageUrl);
            }

            _context.Products.Remove(await product);
            return await _context.SaveChangesAsync();

        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            if (newPrice <= 0)
                return false;

            var product = await _context.Products
                .FirstOrDefaultAsync(x => x.Id == productId);

            if (product == null)
                return false;

            product.Price = newPrice;
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task AddViewCount(int productID)
        {
            var product = await _context.Products.FindAsync(productID);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<PagedResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            var query = from p in _context.Products
                        select new { p };

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword));
            }

            int totalRow = await query.CountAsync();

            var data = await query
                .OrderByDescending(x => x.p.CreatedAt)
                .Skip((request.PageIndex - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new ProductViewModel
                {
                    Id = x.p.Id,
                    Name = x.p.Name,
                    Price = x.p.Price,
                    ViewCount = x.p.ViewCount
                }).ToListAsync();

            var pagedResult = new PagedResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data
            };
            return pagedResult;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return "/" + USER_CONTENT_FOLDER_NAME + "/" + fileName;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var productImage = new ProductImage()
            {
                IsDefault = request.IsDefault,
                ProductId = productId,
                SortOrder = request.SortOrder
            };

            if (request.ImageFile != null)
            {
                productImage.ImageUrl = await this.SaveFile(request.ImageFile);
            }
            _context.ProductImages.Add(productImage);
            await _context.SaveChangesAsync();
            return productImage.Id;
        }

        public async Task<int> RemoveImage(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new Exception($"Không tìm thấy ảnh có ID {imageId}");
            _context.ProductImages.Remove(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new Exception($"Không tìm thấy ảnh có ID {imageId}");

            if (request.ImageFile != null)
            {
                productImage.ImageUrl = await this.SaveFile(request.ImageFile);
            }
            _context.ProductImages.Update(productImage);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImage(int productId)
        {
            return await _context.ProductImages.Where(x => x.ProductId == productId)
                .Select(i => new ProductImageViewModel()
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    IsDefault = i.IsDefault,
                    ProductId = i.ProductId,
                    SortOrder = i.SortOrder
                }).ToListAsync();
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null)
                throw new Exception($"Không tìm thấy ảnh có ID {imageId}");

            var viewModel = new ProductImageViewModel()
            {
                Id = image.Id,
                ImageUrl = image.ImageUrl,
                IsDefault = image.IsDefault,
                ProductId = image.ProductId,
                SortOrder = image.SortOrder
            };
            return viewModel;
        }

        public async Task<List<ProductViewModel>> GetAll()
        {
            var query = from p in _context.Products
                        select new { p };
            var data = await query.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                Name = x.p.Name,
                Price = x.p.Price,
                ViewCount = x.p.ViewCount
            }).ToListAsync();
            return data;
        }

        public async Task<ProductViewModel> GetProductById(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new Exception($"Không thể tìm thấy sản phẩm có ID: {productId}");
            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                MainImageUrl = product.MainImageUrl,
                ViewCount = product.ViewCount,
                CreatedAt = product.CreatedAt,
                UpdatedAt = product.UpdatedAt
            };
            return productViewModel;
        }
    }
}
