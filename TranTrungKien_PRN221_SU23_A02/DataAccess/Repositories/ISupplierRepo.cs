using System.Collections.Generic;
using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface ISupplierRepo
    {
        IList<Supplier> GetSuppliers();
    }
}
