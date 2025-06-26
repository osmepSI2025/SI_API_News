using Microsoft.AspNetCore.Mvc;
using SME_API_News.Entities;
using SME_API_News.Models;

namespace SME_API_News.Repository
{
    public interface IMPopupRepository
    {
        Task<IEnumerable<MPopup>> GetActivePopupsAsync();
        Task<MPopup?> GetByIdAsync(int id);
        Task AddAsync(MPopup popup);
        Task UpdateAsync(MPopup popup);
         Task<ActionResult<int>>DeleteAsync(int id);
        Task<ViewPopupModels> GetPopup(PopupModels models);
    }
}
