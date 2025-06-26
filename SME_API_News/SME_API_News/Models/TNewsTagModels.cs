using System;
using System.Collections.Generic;

namespace SME_API_News.Models;

public partial class TNewsTagModels
{
    public int Id { get; set; }

    public int NewsId { get; set; }

    public int? TagId { get; set; }

    public string? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
}
