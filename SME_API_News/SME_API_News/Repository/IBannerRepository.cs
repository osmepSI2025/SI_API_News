using Microsoft.AspNetCore.Mvc;
using SME_API_News.Entities;
using SME_API_News.Models;


namespace SME_API_News.Repository
{
    public interface IBannerRepository
    {
        Task<IEnumerable<MBanner>> GetAllAsync();
        Task<MBanner?> GetByIdAsync(int id);
        Task<IEnumerable<MBanner>> GetActiveSortedAsync();
        Task AddAsync(MBanner banner);
        Task UpdateAsync(MBanner banner);
         Task<ActionResult<int>>DeleteAsync(int id);
        Task<ViewBannerNewsModels> GetBanner(BannerModels models);
        Task<bool> UpdateStatusBanner(BannerModels models);
    }
}
