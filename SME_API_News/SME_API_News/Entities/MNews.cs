using System;
using System.Collections.Generic;

namespace SME_API_News.Entities;

public partial class MNews
{
    public int Id { get; set; }

    public string? ArticlesTitle { get; set; }

    public string? ArticlesShortDescription { get; set; }

    public string? ArticlesContent { get; set; }

    public string? ArticlesAutherName { get; set; }

    public string? CatagoryCode { get; set; }

    public bool? IsPin { get; set; }

    public DateTime? PublishDate { get; set; }

    public bool? IsPublished { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public string? BusinessUnitId { get; set; }

    public string? CoverFilePath { get; set; }

    public string? PicNewsFilePath { get; set; }

    public string? NewsFilePath { get; set; }

    public string? FileNameOriginal { get; set; }

    public int? OrderId { get; set; }
}
