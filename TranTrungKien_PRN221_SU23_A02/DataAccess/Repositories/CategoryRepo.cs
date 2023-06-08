using System.Collections.Generic;
using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class CategoryRepo : ICategoryRepo
    {
        public IList<Category> GetCategories() => CategoryDAO.GetCategories();
    }
}
