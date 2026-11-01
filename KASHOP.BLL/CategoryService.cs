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
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _repo;

        public CategoryService(IGenericRepository<Category> repo)
        {
            _repo = repo;
        }


        public async Task<bool> CreateAsync(CategoryRequest request, CancellationToken cancellationToken = default)
        {
            var exists = await _repo.GetOneAsync(
                filter: cat => cat.Name.ToLower() == request.Name.ToLower(),
                cancellationToken: cancellationToken);

            if (exists is not null)
                return false;

            var category = request.Adapt<Category>();
            await _repo.CreateAsync(category, cancellationToken);
            return true;
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            var categories = await _repo.GetAsync(cancellationToken: cancellationToken);
            return categories.Adapt<List<CategoryResponse>>();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _repo.GetOneAsync(filter: c => c.Id == id, cancellationToken: cancellationToken);
            return category.Adapt<CategoryResponse?>();
        }

        public async Task<bool> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
