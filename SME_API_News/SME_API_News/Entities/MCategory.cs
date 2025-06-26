using System;
using System.Collections.Generic;

namespace SME_API_News.Entities;

public partial class MCategory
{
    public int Id { get; set; }

    public string? CategorieCode { get; set; }

    public string? CategorieNameEn { get; set; }

    public string? CategorieNameTh { get; set; }

    public string? Description { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public bool? IsActive { get; set; }

    public int? OrderId { get; set; }
}
