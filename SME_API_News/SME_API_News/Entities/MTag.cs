using System;
using System.Collections.Generic;

namespace SME_API_News.Entities;

public partial class MTag
{
    public int Id { get; set; }

    public string? TagName { get; set; }

    public string? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
}
