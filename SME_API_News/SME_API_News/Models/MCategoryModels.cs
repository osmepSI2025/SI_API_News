using SME_API_News.Entities;

namespace SME_API_News.Models
{
    public class CategoryNewsModels
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
        public int rowOFFSet { get; set; }
        public int rowFetch { get; set; }

        public string? Token { get; set; }
        public int? OrderId { get; set; }
    }
    public class ViewCategoryNewsModels
    {
        public CategoryNewsModels SearchCategoryNewsModels { get; set; }
        public CategoryNewsModels CategoryNewsModels { get; set; }


        public List<CategoryNewsModels> listMCategoryModels { get; set; }
        public int? TotalRowsList { get; set; }
        public PagingModel PageModel { get; set; }

        public vDropdownDTO DDLStatus { get; set; }
        public vDropdownDTO DDLpin { get; set; }
        public vDropdownDTO DDLpublish { get; set; }

        public vDropdownDTO DDLDepartment { get; set; }

    }
}
