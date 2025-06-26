using SME_API_News.Entities;
using System;
using System.Collections.Generic;

namespace SME_API_News.Models;

public partial class MTagModels
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }

    public ICollection<TNewsTag> NewsTags { get; set; } = new List<TNewsTag>();
}
