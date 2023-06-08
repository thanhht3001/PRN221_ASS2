using System;
using System.Collections.Generic;
using System.Linq;

using BusinessObject.Context;
using BusinessObject.Models;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        public static IList<Category> GetCategories()
        {
            var list = new List<Category>();
            try
            {
                using (var context = new FUFlowerBouquetManagementContext())
                {
                    list = context.Categories.ToList();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return list;
        }
    }
}
