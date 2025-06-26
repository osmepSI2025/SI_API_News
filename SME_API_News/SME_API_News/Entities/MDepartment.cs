using System;
using System.Collections.Generic;

namespace SME_API_News.Entities;

public partial class MDepartment
{
    public int Id { get; set; }

    public string? DepartmentCode { get; set; }

    public string? DepartmentNameEn { get; set; }

    public string? DepartmentNameTh { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public string? CreateBy { get; set; }

    public string? UpdateBy { get; set; }
}
