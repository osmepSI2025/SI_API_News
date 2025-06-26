using SME_API_News.Entities;
using SME_API_News.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace SME_API_News.Repository
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<MDepartment>> GetAllAsync();
        Task<MDepartment?> GetByIdAsync(int id);
        Task AddAsync(MDepartment param);
        Task UpdateAsync(MDepartment param);
        Task DeleteAsync(int id);
    }
}
