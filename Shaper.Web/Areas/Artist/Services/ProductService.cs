﻿using Shaper.DataAccess.IdentityContext;
using Shaper.Models.Entities;
using Shaper.Models.ViewModels.ProductComponentsVM;
using Shaper.Models.ViewModels.ProductVM;
using Shaper.Utility;
using Shaper.Web.ApiService.IService;
using Shaper.Web.Areas.Artist.Services.IService;
using System.Security.Claims;

namespace Shaper.Web.Areas.Admin.Services
{
    public class ProductService : IProductService
    {
        private readonly IShaperApiService _apiService;
        private readonly IdentityAppDbContext _db;

        public ProductService(IShaperApiService apiService, IdentityAppDbContext db)
        {
            _apiService = apiService;
            _db = db;        }

        public async Task<Product> GetProductAsync(int id, string token)
        {
            return await _apiService.ProductApi.GetFirstOrDefaultAsync(ApiPaths.ApiPath.Products.GetEndpoint(id), token);
        }

        public async Task<IEnumerable<Product>> GetProductsAsync(string token)
        {
            return await _apiService.ProductApi.GetAllAsync(ApiPaths.ApiPath.Products.GetEndpoint(null), token);
        }

        public async Task CreateProductAsync(Product product, string token)
        {
            await _apiService.ProductApi.AddAsync(product, ApiPaths.ApiPath.Products.GetEndpoint(null), token);
        }

        public async Task UpdateProductAsync(Product product, string token)
        {
            await _apiService.ProductApi.UpdateAsync(product, ApiPaths.ApiPath.Products.GetEndpoint(product.Id), token);
        }

        public async Task DeleteProductAsync(int id, string token)
        {
            await _apiService.ProductApi.RemoveAsync(ApiPaths.ApiPath.Products.GetEndpoint(id), token);
        }

        public async Task<ProductUpsertVM> GetProductVMsAsync(string token, int? id = null)
        {
            ProductUpsertVM product = new ProductUpsertVM();
            if (id is null || id == 0)
            {
                product = await _apiService.ProductApi.FetchVMAsync(ApiPaths.ApiPath.ProductsVM.GetEndpoint(), token);
            }
            else
            {
                product = await _apiService.ProductApi.FetchVMAsync(ApiPaths.ApiPath.ProductsVM.GetEndpoint(id), token);
            }

            return product;

        }

        public async Task<Product> GetProductWithComponentsAsync(ProductUpsertVM upsertVM, string token)
        {
            ProductReqComponentsVM requestingComponents = new(upsertVM.ColorId, upsertVM.ShapeId, upsertVM.TransparencyId);
            var components = await _apiService.ProductApi.FetchProductComponentsAsync(requestingComponents, ApiPaths.ApiPath.ProductComponents.GetEndpoint());
            var product = upsertVM.VmToNewProduct(components);
            return product;

        }
    }
}
