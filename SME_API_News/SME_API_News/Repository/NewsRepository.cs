using Microsoft.EntityFrameworkCore;
using SME_API_News.Entities;
using SME_API_News.Models;
using SME_API_News.Repository;

public class NewsRepository : INewsRepository
{
    private readonly NewsDBContext _context;

    public NewsRepository(NewsDBContext context)
    {
        _context = context;
    }

    public async Task<ViewMNewsModels> GetAllAsync(MNewsModels param)
    {
        ViewMNewsModels result = new ViewMNewsModels();
        result.SearchMNewsModels = param;
        List<MNewsModels> news = new List<MNewsModels>();
        try
        {

            var query = (from u in _context.MNews
                         join c in _context.MCategories on u.CatagoryCode equals c.CategorieCode

                         select new MNewsModels
                         {
                             Id = u.Id,
                             ArticlesTitle = u.ArticlesTitle,
                             ArticlesAutherName = u.ArticlesAutherName,
                             CatagoryCode = u.CatagoryCode,
                             ArticlesContent = u.ArticlesContent,
                             IsPublished = u.IsPublished,
                             PublishDate = u.PublishDate,
                             StartDate = u.StartDate,
                             EndDate = u.EndDate,
                             CreateBy = u.CreateBy,
                             CreateDate = u.CreateDate,
                             UpdateBy = u.UpdateBy,
                             UpdateDate = u.UpdateDate,
                             IsPin = u.IsPin,
                             CatagoryName = c.CategorieNameTh
                             ,
                             ArticlesShortDescription = u.ArticlesShortDescription,
                             BusinessUnitId = u.BusinessUnitId
                              ,
                             CoverFilePath = u.CoverFilePath
                              ,
                             PicNewsFilePath = u.PicNewsFilePath
                              ,
                             NewsFilePath = u.NewsFilePath
                              ,
                             FileNameOriginal = u.FileNameOriginal,
                             OrderId = u.OrderId,
                         });

            if (param != null)
            {
                if (param.Id != null && param.Id != 0)
                {
                    query = query.Where(item => item.Id == param.Id);
                }

                if (param.CatagoryCode != null && param.CatagoryCode != "")
                {
                    query = query.Where(item => item.CatagoryCode == param.CatagoryCode);
                }
                if (param.ArticlesAutherName != null && param.ArticlesAutherName != "")
                {
                    query = query.Where(item => item.ArticlesAutherName.Contains(param.ArticlesAutherName));
                }
                if (param.ArticlesTitle != null && param.ArticlesTitle != "")
                {
                    query = query.Where(item => item.ArticlesTitle.Contains(param.ArticlesTitle));
                }
                if (param.StartDate != null && param.EndDate != null)
                {

                    query = query.Where(item => item.StartDate >= param.StartDate.Value.Date && item.EndDate.Value.Date <= param.EndDate.Value.Date);
                }
                if (param.PublishDate != null)
                {
                    query = query.Where(item => item.PublishDate.Value.Date == param.PublishDate.Value.Date);
                }
              
                if (param.BusinessUnitId != null && param.BusinessUnitId != "")
                {
                    query = query.Where(item => item.BusinessUnitId == param.BusinessUnitId);
                }

                if (param.FlagPage == "PIN")
                {

                    if (param.IsPin != null)
                    {
                        query = query.Where(item => item.IsPublished == true);
                        query = query.Where(item => item.IsPin == true);
                        //range query order by orderid
                        query = query
      .OrderBy(x => x.OrderId == null)  // false (has value) มาก่อน true (null)
      .ThenBy(x => x.OrderId);          // จากน้อยไปมาก
                    }
                }
                else if (param.FlagPage == "SEARCH") 
                {
                    //if (param.IsPublished != null)
                    //{
                    //    query = query.Where(item => item.IsPublished == param.IsPublished);
                    //}
                    query = query.OrderByDescending(x => x.CreateDate);
                }

                else
                {
                    query = query.Where(item => item.IsPublished == true);
                    query = query.OrderByDescending(x => x.CreateDate);
                }

                //find total
                result.TotalRowsList = query.Count();

                if (param.rowFetch != 0)
                {
                    query = query.Skip<MNewsModels>(param.rowOFFSet).Take(param.rowFetch);
                }
            }
            news = query.ToList();

            result.ListTMNewsModels = query.ToList();
            return result;
        }
        catch
        {
            return result;
        }

    }

    public async Task<MNewsModels> GetByIdAsync(int id)
    {
        MNewsModels news = new MNewsModels();
        try
        {

            var query = (from u in _context.MNews
                         where u.Id == id
                         select u
                         ).First();

            if (query != null)
            {
                news.Id = query.Id;
                news.ArticlesTitle = query.ArticlesTitle;
                news.ArticlesAutherName = query.ArticlesAutherName;
                news.CatagoryCode = query.CatagoryCode;
                news.ArticlesContent = query.ArticlesContent;
                news.IsPublished = query.IsPublished;
                news.PublishDate = query.PublishDate;
                news.StartDate = query.StartDate;
                news.EndDate = query.EndDate;
                news.CreateBy = query.CreateBy;
                news.CreateDate = query.CreateDate;
                news.UpdateBy = query.UpdateBy;
                news.UpdateDate = query.UpdateDate;
                news.IsPin = query.IsPin;
                news.ArticlesShortDescription = query.ArticlesShortDescription;
                news.BusinessUnitId = query.BusinessUnitId;
                return news;
            }
            else
            {
                return null;
            }




        }
        catch
        {
            return news;
        }
    }

    public async Task<int> AddAsyncNews(MNewsModels news)
    {
        int newId = 0;
        try
        {
            MNews xdata = new MNews();
            xdata.ArticlesContent = news.ArticlesContent;
            xdata.ArticlesTitle = news.ArticlesTitle;
            xdata.ArticlesAutherName = news.ArticlesAutherName;
            xdata.CatagoryCode = news.CatagoryCode;
            xdata.PublishDate = news.PublishDate;
            xdata.StartDate = news.StartDate;
            xdata.EndDate = news.EndDate;
            xdata.IsPublished = news.IsPublished;
            xdata.CreateDate = DateTime.Now;
            xdata.IsPin = news.IsPin;
            xdata.ArticlesShortDescription = news.ArticlesShortDescription;
            xdata.BusinessUnitId = news.BusinessUnitId;
            xdata.CoverFilePath = news.CoverFilePath;
            xdata.PicNewsFilePath = news.PicNewsFilePath;
            xdata.NewsFilePath = news.NewsFilePath;
            xdata.FileNameOriginal = news.FileNameOriginal;
            xdata.CreateBy = news.CreateBy;

            _context.MNews.Add(xdata);
            await _context.SaveChangesAsync();
            newId = xdata.Id; // Assuming the database generates the ID after saving
        }
        catch (Exception ex)
        {
            // Log the exception if necessary
        }
        return newId;
    }

    public async Task UpdateAsync(MNews news)
    {
        try
        {
            var existing = await _context.MNews.FindAsync(news.Id);
            if (existing == null)
            {
                // Handle not found (throw, return, or log as needed)
                throw new InvalidOperationException("News not found.");
            }


            // Update only the fields you want to allow to change
            if(news.ArticlesAutherName != null) 
            {
                existing.ArticlesAutherName= news.ArticlesAutherName;
            }
            if (news.ArticlesTitle != null) 
            {
                existing.ArticlesTitle = news.ArticlesTitle;
            }
            if (news.CatagoryCode != null) 
            {
                existing.CatagoryCode = news.CatagoryCode;
            }
            if (news.ArticlesContent != null) 
            {
                existing.ArticlesContent = news.ArticlesContent;
            }
            if (news.ArticlesShortDescription != null) 
            {
                existing.ArticlesShortDescription = news.ArticlesShortDescription;
            }
            if (news.BusinessUnitId != null) 
            {
                existing.BusinessUnitId = news.BusinessUnitId;
            }
            if (news.CoverFilePath != null) 
            {
                existing.CoverFilePath = news.CoverFilePath;
            }
            if (news.PicNewsFilePath != null) 
            {
                existing.PicNewsFilePath = news.PicNewsFilePath;
            }
            if (news.NewsFilePath != null) 
            {
                existing.NewsFilePath = news.NewsFilePath;
            }
            if (news.FileNameOriginal != null) 
            {
                existing.FileNameOriginal = news.FileNameOriginal;
            }
            if (news.OrderId != null) 
            {
                existing.OrderId = news.OrderId;
            }
            if(news.StartDate !=null) 
            {
                existing.StartDate = news.StartDate;
            }
            if (news.EndDate != null) 
            {
                existing.EndDate = news.EndDate;
            }
            if (news.PublishDate != null) 
            {
                existing.PublishDate = news.PublishDate;
            }
            if (news.IsPublished != null) 
            {
                existing.IsPublished = news.IsPublished;
            }
            if (news.IsPin != null) 
            {
                existing.IsPin = news.IsPin;
            }



            existing.UpdateBy = news.UpdateBy;
            existing.UpdateDate = DateTime.Now;


            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Optionally log the exception
        }
    }

    public async Task DeleteAsync(int id)
    {
        var news = await _context.MNews.FindAsync(id);
        if (news != null)
        {
            _context.MNews.Remove(news);
            await _context.SaveChangesAsync();
        }
    }
    public async Task UpdateRangeAsync(MNews news)
    {
        try
        {
            var existing = await _context.MNews.FindAsync(news.Id);
            if (existing != null)
            {
                existing.OrderId = news.OrderId;
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {

        }
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.MNews.AnyAsync(e => e.Id == id);
    }
    public async Task AddTagAsync(int newsId, int tagId)
    {
        var exists = await _context.TNewsTags.AnyAsync(nt => nt.NewsId == newsId && nt.TagId == tagId);
        if (!exists)
        {
            _context.TNewsTags.Add(new TNewsTag { NewsId = newsId, TagId = tagId });
            await _context.SaveChangesAsync();
        }
    }

    public async Task RemoveTagAsync(int newsId, int tagId)
    {
        var newsTag = await _context.TNewsTags.FirstOrDefaultAsync(nt => nt.NewsId == newsId && nt.TagId == tagId);
        if (newsTag != null)
        {
            _context.TNewsTags.Remove(newsTag);
            await _context.SaveChangesAsync();
        }
    }

    // creat a function to get all tag by parameter
    public async Task<List<MTag>> GetAllTagsAsync()
    {
        return await _context.MTags.ToListAsync();
    }
    public async Task<bool> UpdateStatusActiveNews(MNewsModels news)
    {
        try
        {
            var existing = await _context.MNews.FindAsync(news.Id);
            if (existing != null)
            {
                if (news.FlagPage =="PIN") 
                {
                    existing.IsPin = news.IsPin;
                }
                if (news.FlagPage == "ACTIVE") 
                {
                    existing.IsPublished = news.IsPublished;
                }
                existing.UpdateBy = news.UpdateBy;
                existing.UpdateDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return true;
        }
        catch (Exception ex)
        {return false;

        }
    }

}
