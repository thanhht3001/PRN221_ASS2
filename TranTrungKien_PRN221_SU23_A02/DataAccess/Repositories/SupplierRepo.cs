using System.Collections.Generic;
using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class SupplierRepo : ISupplierRepo
    {
        public IList<Supplier> GetSuppliers() => SupplierDAO.GetSuppliers();
    }
}
