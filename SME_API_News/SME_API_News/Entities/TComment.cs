using System;
using System.Collections.Generic;

namespace SME_API_News.Entities;

public partial class TComment
{
    public int Id { get; set; }

    public int? NewsId { get; set; }

    public string? ContentDatail { get; set; }

    public string? EmployeeCode { get; set; }

    public bool? IsApproved { get; set; }

    public string? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
}
