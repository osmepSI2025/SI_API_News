using System;
using System.Collections.Generic;

namespace SME_API_News.Models;

public partial class MNewsModels
{
    public int Id { get; set; }

    public string? ArticlesTitle { get; set; }

    public string? ArticlesContent { get; set; }
    public string? ArticlesShortDescription { get; set; }
    public string? ArticlesAutherName { get; set; }

    public string? CatagoryCode { get; set; }
    public string? CatagoryName { get; set; }

    public bool? IsPin { get; set; }

    public DateTime? PublishDate { get; set; }

    public bool? IsPublished { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
    public int rowOFFSet { get; set; }
    public int rowFetch { get; set; }
   
    public string? Token { get; set; }
    public string? BusinessUnitId { get; set; }
    public string? CoverFilePath { get; set; }

    public string? PicNewsFilePath { get; set; }

    public string? NewsFilePath { get; set; }

    public int? OrderId { get; set; }
    public string? FileNameOriginal { get; set; }

    public string? FlagPage { get; set; }

}
public class ViewMNewsModels
{
    public MNewsModels SearchMNewsModels { get; set; }
    public MNewsModels MNewsModels { get; set; }

    public List<MNewsModels> ListTMNewsModels { get; set; }
    public int? TotalRowsList { get; set; }

    public PagingModel PageModel { get; set; }


}