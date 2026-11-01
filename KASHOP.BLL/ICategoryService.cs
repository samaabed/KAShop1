using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL
{
    public interface ICategoryService
    {
        Task<List<CategoryResponse>> GetAllAsync(CancellationToken cancellationToken = default);
        Task<CategoryResponse?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> UpdateAsync(int id, CategoryRequest request, CancellationToken cancellationToken = default);
        Task<bool> DeleteAsync(int id, CancellationToken cancellationToken = default);
        Task<bool> CreateAsync(CategoryRequest request, CancellationToken cancellationToken = default);
    }
}
