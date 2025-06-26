using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SME_API_News.Entities;
using SME_API_News.Models;

namespace SME_API_News.Repository
{
    public class MPopupRepository : IMPopupRepository
    {
        private readonly NewsDBContext _context;

        public MPopupRepository(NewsDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MPopup>> GetActivePopupsAsync()
        {
            var now = DateTime.Now;
            return await _context.MPopups
                .Where(p => p.FlagActive == true && p.StartDateTime <= now && p.EndDateTime >= now)
                .ToListAsync();
        }

        public async Task<MPopup?> GetByIdAsync(int id)
        {
            return await _context.MPopups.FindAsync(id);
        }

        public async Task AddAsync(MPopup popup)
        {
            popup.CreateDate = DateTime.Now;
            _context.MPopups.Add(popup);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MPopup popup)
        {
            popup.UpdateDate = DateTime.Now;
            _context.MPopups.Update(popup);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
            try
            {
                var popup = await _context.MPopups.FindAsync(id);
                if (popup != null)
                {
                    _context.MPopups.Remove(popup);
                    int result = await _context.SaveChangesAsync();
                    return result;
                }
                else
                {
                    return 0; // Return 0 if the popup is not found
                }
            }
            catch (Exception ex)
            {
                return 0; // Return 0 in case of an exception
            }
        }

        //Task<ViewPopupModels> GetPopup(PopupModels models);
        public async Task<ViewPopupModels> GetPopup(PopupModels param)
        {
            ViewPopupModels result = new ViewPopupModels();

            try
            {

                var query = (
                             from c in _context.MPopups

                             select new PopupModels
                             {
                                 Id = c.Id,
                                 Title = c.Title,
                                 Description = c.Description,
                                 CreateBy = c.CreateBy,
                                 CreateDate = c.CreateDate,
                                 UpdateBy = c.UpdateBy,
                                 UpdateDate = c.UpdateDate,
                                 ImageUrl = c.ImageUrl,

                                 StartDateTime = c.StartDateTime,
                                 EndDateTime = c.EndDateTime,
                                 FlagActive = c.FlagActive,



                             });
                if (param != null)
                {
                    if (param.Id != null && param.Id != 0)
                    {
                        query = query.Where(item => item.Id == param.Id);
                    }


                    if (param.FlagActive != null)
                    {
                        query = query.Where(item => item.FlagActive == param.FlagActive);
                    }
                    if (param.Title != null && param.Title != "")
                    {
                        query = query.Where(item => item.Title.Contains(param.Title));
                    }
                    if (param.StartDateTime != null)
                    {
                        query = query.Where(item => item.StartDateTime.Value.Date<= param.StartDateTime.Value.Date);
                        query = query.Where(item => item.EndDateTime.Value.Date >= param.StartDateTime.Value.Date);
                    }
                    //find total
                    result.TotalRowsList = query.Count();

                    if (param.rowFetch != 0)
                    {
                        query = query.Skip<PopupModels>(param.rowOFFSet).Take(param.rowFetch);
                    }
                }
                // ldata = query.ToList();

                result.listPopupModels = query.ToList();
                return result;
            }
            catch
            {
                return result;
            }
        }
    }


}
