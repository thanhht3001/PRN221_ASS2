using System.Collections.Generic;
using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface ICategoryRepo
    {
        IList<Category> GetCategories();
    }
}
