using System;
using System.Collections.Generic;
using System.Linq;

using BusinessObject.Models;
using BusinessObject.Context;

namespace DataAccess.DAO
{
    public class SupplierDAO
    {
        public static IList<Supplier> GetSuppliers()
        {
            var list = new List<Supplier>();
            try
            {
                using (var context = new FUFlowerBouquetManagementContext())
                {
                    list = context.Suppliers.ToList();
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
