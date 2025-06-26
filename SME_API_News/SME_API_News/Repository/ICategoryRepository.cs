using SME_API_News.Entities;
using SME_API_News.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SME_API_News.Repository
{
    public interface ICategoryRepository
    {
        Task<ViewCategoryNewsModels> GetAllAsync(CategoryNewsModels models);
        Task<MCategory?> GetByIdAsync(int id);
        Task AddAsync(MCategory category);
        Task UpdateAsync(MCategory category);
        Task DeleteAsync(int id);
    }
}
