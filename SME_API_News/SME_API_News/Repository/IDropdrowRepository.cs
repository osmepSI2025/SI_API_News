using SME_API_News.Models;
namespace SME_API_News.Repository
{
    public interface IDropdownRepository
    {

        List<DropdownModels> GetDropdownLookUp(string LookupType);
        List<DropdownModels> GetDropdownCategory();


    }
}
