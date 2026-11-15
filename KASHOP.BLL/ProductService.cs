using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepo;
        private readonly IFileService _fileService;

        public ProductService(IGenericRepository<Product> productRepo, IFileService fileService)
        {
            _productRepo = productRepo;
            _fileService = fileService;
        }

        public async Task<bool> CreateAsync(ProductRequest request, CancellationToken cancellationToken = default)
        {
            var imageName = await _fileService.UploadAsync(request.MainImage, cancellationToken);

            if (imageName is null) 
                return false;

            var product = request.Adapt<Product>();
            product.MainImage = imageName;

            try
            {
                await _productRepo.CreateAsync(product, cancellationToken);
            } catch
            {
                _fileService.Delete(imageName);
                return false;
            }

            return true;
        }

        public async Task<List<ProductResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var products = await _productRepo.GetAsync(
                includes: [p => p.Translations, products => products.Category.Translations],
                cancellationToken: cancellationToken);

            return products.Adapt<List<ProductResponse>>();

            
        }
    }
}
