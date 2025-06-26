using Microsoft.EntityFrameworkCore;
using SME_API_News.Entities;
using SME_API_News.Models;
using SME_API_News.Repository;
using System;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly NewsDBContext _context;

    public DepartmentRepository(NewsDBContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MDepartment>> GetAllAsync() =>
      await _context.MDepartments.ToListAsync();

    public async Task<MDepartment?> GetByIdAsync(int id) =>
        await _context.MDepartments.FindAsync(id);

    public async Task AddAsync(MDepartment param)
    {
        _context.MDepartments.Add(param);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(MDepartment param)
    {
        _context.MDepartments.Update(param);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var DepartmentId = await _context.MDepartments.FindAsync(id);
        if (DepartmentId != null)
        {
            _context.MDepartments.Remove(DepartmentId);
            await _context.SaveChangesAsync();
        }
    }
}
