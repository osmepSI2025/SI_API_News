using SME_API_News.Entities;
using SME_API_News.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace SME_API_News.Repository
{
    public class DropdownRepository : ControllerBase, IDropdownRepository
    {
        private readonly NewsDBContext _context;
        public DropdownRepository(NewsDBContext context)
        {
            _context = context;
        }


        public List<DropdownModels> GetDropdownLookUp(string LookUptype)
        {
            try
            {
                var query = (from u in _context.MLookUps
                             where u.LookupType == LookUptype && u.FlagActive == "Y" && u.FlagDelete == "N"
                             select new DropdownModels
                             {
                                 Code = u.LookupCode,
                                 Name = u.LookupValue,
                             }
                             );
                return query.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //public List<DropdownModels> GetDropdownRole()
        //{
        //    try
        //    {
        //        var query = (from u in _context.MRoles
        //                     where u.FlagActive == "Y" && u.FlagDelete == "N"
        //                     select new DropdownModels
        //                     {
        //                         Code = u.RoleCode,
        //                         Name = u.RoleName,
        //                     }
        //                     );
        //        return query.OrderBy(x => x.Name).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public List<DropdownModels> GetDropdownCategory()
        {
            try
            {
                var query = (from u in _context.MCategories
                         //    where u.FlagActive == "Y" && u.FlagDelete == "N"
                             select new DropdownModels
                             {
                                 Code = u.CategorieCode,
                                 Name = u.CategorieNameTh,
                             }
                             );
                return query.OrderBy(x => x.Name).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
