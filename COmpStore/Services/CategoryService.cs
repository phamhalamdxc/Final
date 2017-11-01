using COmpStore.QueryParameters;
using COmpStore.Schema;
using COmpStore.Schema.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace COmpStore.Services
{
    public class CategoryService : ICategoryService
    {
        private StoreDbContext _context;

        public CategoryService(StoreDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> GetAll(CategoryQueryParameters categoryQueryParameters)
        {
            // return _context.Categories;

            IQueryable<Category> _allCategories = _context.Categories.OrderBy(x=>x.CategoryName);

            if (categoryQueryParameters.HasSearch)
            {
                _allCategories = _allCategories.Where(x => x.CategoryName.ToLower().Contains(categoryQueryParameters.Search.ToLower()));
            }

            return _allCategories
                .Skip(categoryQueryParameters.PageCount * (categoryQueryParameters.Page - 1))
                .Take(categoryQueryParameters.PageCount);
        }

        public Category GetSingle(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id);
        }

        public void Add(Category item)
        {
            _context.Categories.Add(item);
        }

        public void Delete(int id)
        {
            Category Category = GetSingle(id);
            _context.Categories.Remove(Category);
        }

        public void Update(Category item)
        {
            _context.Categories.Update(item);
        }
        public int Count()
        {
            return _context.Categories.Count();
        }

        public bool Save()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
