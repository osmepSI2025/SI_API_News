﻿using Microsoft.EntityFrameworkCore;
using SME_API_News.Entities;
using SME_API_News.Models;
using SME_API_News.Repository;

public class CategoryRepository : ICategoryRepository
{
    private readonly NewsDBContext _context;

    public CategoryRepository(NewsDBContext context)
    {
        _context = context;
    }

    public async Task<ViewCategoryNewsModels> GetAllAsync(CategoryNewsModels param)
    {
        ViewCategoryNewsModels result = new ViewCategoryNewsModels();
        List<CategoryNewsModels> catNews = new List<CategoryNewsModels>();
       
        try
        {
          
            var query = (
                         from c in _context.MCategories

                         select new CategoryNewsModels
                         {
                             CategorieCode = c.CategorieCode,
                             CategorieNameEn = c.CategorieNameEn,
                             CategorieNameTh = c.CategorieNameTh,
                             CreateBy = c.CreateBy,
                             CreateDate = c.CreateDate,
                             UpdateBy = c.UpdateBy,
                             UpdateDate = c.UpdateDate,
                             Description = c.Description,
                             IsActive = c.IsActive,
                             Id = c.Id
                           , OrderId = c.OrderId
                         });
            if (param != null)
            {
                if (param.Id != null && param.Id != 0)
                {
                    query = query.Where(item => item.Id == param.Id);
                }

               
                if (param.IsActive != null)
                {
                    query = query.Where(item => item.IsActive == param.IsActive);
                }
                if (param.CategorieNameTh != null&& param.CategorieNameTh != "")
                {
                    query = query.Where(item => item.CategorieNameTh.Contains(param.CategorieNameTh));
                }

                //find total
                result.TotalRowsList = query.Count();

                if (param.rowFetch != 0)
                {
                    query = query.Skip<CategoryNewsModels>(param.rowOFFSet).Take(param.rowFetch);
                }
            }
            catNews = query.ToList();

            result.listMCategoryModels = query.ToList();
            return result;
        }
        catch
        {
            return result;
        }
    }


    public async Task<MCategory?> GetByIdAsync(int id) =>
        await _context.MCategories.FindAsync(id);

    public async Task AddAsync(MCategory category)
    {
        try
        {
            _context.MCategories.Add(category);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex) 
        {
        
        }
        
        }


    public async Task UpdateAsync(MCategory category)
    {
        var existingCategory = await _context.MCategories.FindAsync(category.Id);
        if (existingCategory == null)
        {
            // You can throw an exception or handle as needed
            throw new InvalidOperationException("Category not found.");
        }

        // Optionally, update only the fields you want to allow to change
        if(category.CategorieCode != null)
        {
            existingCategory.CategorieCode = category.CategorieCode;
        }
        if (category.CategorieNameEn != null)
        {
            existingCategory.CategorieNameEn = category.CategorieNameEn;
        }
        if (category.CategorieNameTh != null)
        {
            existingCategory.CategorieNameTh = category.CategorieNameTh;
        }
        if (category.Description != null)
        {
            existingCategory.Description = category.Description;
        }
        if (category.IsActive != null)
        {
            existingCategory.IsActive = category.IsActive;
        }
        if (category.UpdateBy != null)
        {
            existingCategory.UpdateBy = category.UpdateBy;
        }
        if (category.OrderId != null)
        {
            existingCategory.OrderId = category.OrderId;
        }
        
        existingCategory.UpdateBy = category.UpdateBy;
        existingCategory.UpdateDate = DateTime.UtcNow;     

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var category = await _context.MCategories.FindAsync(id);
        if (category != null)
        {
            _context.MCategories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
