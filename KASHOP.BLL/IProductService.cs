using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL
{
    public interface IProductService
    {
        Task<bool> CreateAsync(ProductRequest request, CancellationToken cancellationToken = default);
        Task<List<ProductResponse>> GetAllAsync(CancellationToken cancellationToken = default);
    }
}
