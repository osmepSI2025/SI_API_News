using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SME_API_News.Entities;
using SME_API_News.Models;

namespace SME_API_News.Repository
{
    public class BannerRepository : IBannerRepository
    {
        private readonly NewsDBContext _context;

        public BannerRepository(NewsDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MBanner>> GetAllAsync() =>
            await _context.Set<MBanner>().OrderBy(b => b.SortOrder).ToListAsync();

        public async Task<MBanner?> GetByIdAsync(int id) =>
            await _context.Set<MBanner>().FindAsync(id);

        public async Task<IEnumerable<MBanner>> GetActiveSortedAsync()
        {
            var now = DateTime.Now;
            return await _context.Set<MBanner>()
                .Where(b => b.FlagActive == true && b.StartDateTime <= now && b.EndDateTime >= now)
                .OrderBy(b => b.SortOrder)
                .ToListAsync();
        }

        public async Task AddAsync(MBanner banner)
        {
            banner.CreateDate = DateTime.Now;
            _context.Set<MBanner>().Add(banner);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MBanner banner)
        {
            banner.UpdateDate = DateTime.Now;
            _context.Set<MBanner>().Update(banner);
            await _context.SaveChangesAsync();
        }

        public async Task<ActionResult<int>> DeleteAsync(int id)
        {
          
            try
            {
                var banner = await GetByIdAsync(id);
                if (banner != null)
                {
                    _context.Set<MBanner>().Remove(banner);
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
        public async Task<ViewBannerNewsModels> GetBanner(BannerModels param)
        {
            ViewBannerNewsModels result = new ViewBannerNewsModels();
            List<BannerModels> ldata = new List<BannerModels>();

            try
            {

                var query = (
                             from c in _context.MBanners

                             select new BannerModels
                             {
                                 Id = c.Id,
                                 Title = c.Title,
                                 Description = c.Description,
                                 CreateBy = c.CreateBy,
                                 CreateDate = c.CreateDate,
                                 UpdateBy = c.UpdateBy,
                                 UpdateDate = c.UpdateDate,
                                 ImageUrl = c.ImageUrl,
                                 LinkUrl = c.LinkUrl,
                                 StartDateTime = c.StartDateTime,
                                 EndDateTime = c.EndDateTime,
                                 FlagActive = c.FlagActive,
                                 PublishDate = c.PublishDate,


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

                    //find total
                    result.TotalRowsList = query.Count();

                    if (param.rowFetch != 0)
                    {
                        query = query.Skip<BannerModels>(param.rowOFFSet).Take(param.rowFetch);
                    }
                }
                // ldata = query.ToList();

                result.listBannerModels = query.ToList();
                return result;
            }
            catch
            {
                return result;
            }
        }

        public async Task<bool> UpdateStatusBanner(BannerModels param)
        {
            try
            {
                var existing = await _context.MBanners.FindAsync(param.Id);
                if (existing != null)
                {
                    existing.FlagActive = param.FlagActive;
                    existing.UpdateBy = param.UpdateBy;
                    existing.UpdateDate = DateTime.Now;
                    await _context.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

    }

}
