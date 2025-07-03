using SME_API_News.Entities;
using SME_API_News.Models;

namespace SME_API_News.Repository
{
    public interface INewsRepository
    {
        Task<ViewMNewsModels> GetAllAsync(MNewsModels param);
        Task<MNewsModels> GetByIdAsync(int id);
        Task<int> AddAsyncNews(MNewsModels news);
        Task UpdateAsync(MNews news);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task AddTagAsync(int newsId, int tagId);
        Task RemoveTagAsync(int newsId, int tagId);

        Task UpdateRangeAsync(MNews news);
        Task<bool> UpdateStatusActiveNews(MNewsModels param);

    }
}
