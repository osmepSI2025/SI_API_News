using SME_API_News.Models;
using SME_API_News.Entities;
using SME_API_News.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace SME_API_News.Controllers
{
    [Route("api/SME_NEWS_V1/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class DropdownController : ControllerBase
    {
        private readonly NewsDBContext _context;
        private readonly IDropdownRepository _Eg;
        public DropdownController(NewsDBContext context, IDropdownRepository DropdownRepository)
        {
            _context = context;
            _Eg = DropdownRepository;
        }


        [HttpGet]
        [Route("LookUp/{LookUpType}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public vDropdownDTO GetDropdownLookUp(string LookUpType)
        {
            vDropdownDTO vList = new vDropdownDTO();
            try
            {
                if (ModelState.IsValid)
                {
                    var lDropdown = _Eg.GetDropdownLookUp(LookUpType);
                    if (lDropdown.Count > 0)
                    {
                        vList.responseCode = "RES-200";
                        vList.responseDesc = "Resquest processes successfully";
                        vList.DropdownList = lDropdown;
                        return vList;
                    }
                    else
                    {
                        vList.responseCode = "RES-200";
                        vList.responseDesc = "Resquest processes successfully";
                        vList.DropdownList = null;
                        return vList;
                    }
                }
                else
                {
                    vList.responseCode = "RES-500";
                    vList.responseDesc = "Resquest processes Unsuccess";
                    return vList;
                }
            }
            catch (Exception ex)
            {
                vList.responseCode = "RES-404";
                vList.responseDesc = ex.Message;
                ;
                return vList;
            }
        }

        [HttpGet]
        [Route("LookUp/GetDropdownCategory")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public vDropdownDTO GetDropdownCategory()
        {
            vDropdownDTO vList = new vDropdownDTO();
            try
            {
                if (ModelState.IsValid)
                {
                    var lDropdown = _Eg.GetDropdownCategory();
                    if (lDropdown.Count > 0)
                    {
                        vList.responseCode = "RES-200";
                        vList.responseDesc = "Resquest processes successfully";
                        vList.DropdownList = lDropdown;
                        return vList;
                    }
                    else
                    {
                        vList.responseCode = "RES-200";
                        vList.responseDesc = "Resquest processes successfully";
                        vList.DropdownList = null;
                        return vList;
                    }
                }
                else
                {
                    vList.responseCode = "RES-500";
                    vList.responseDesc = "Resquest processes Unsuccess";
                    return vList;
                }
            }
            catch (Exception ex)
            {
                vList.responseCode = "RES-404";
                vList.responseDesc = ex.Message;
                ;
                return vList;
            }
        }


    }
}
