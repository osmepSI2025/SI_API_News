namespace SME_API_News.Models
{
    public class MDepartmentModels
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
}
