using System.Linq;
using COmpStore.Schema.Entities;
using COmpStore.QueryParameters;

namespace COmpStore.Services
{
    public interface ICategoryService
    {
        void Add(Category item);
        void Delete(int id);
        IQueryable<Category> GetAll(CategoryQueryParameters categoryQueryParameters);
        Category GetSingle(int id);
        bool Save();
        int Count();
        void Update(Category item);
    }
}