using System.Collections.Generic;
using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface ICustomerRepo
    {
        Customer Login(string email, string password);
        Customer GetCustomer(int id);
        IList<Customer> GetCustomers();
        void Save(Customer customer);
        void Update(Customer customer);
        void Delete(Customer customer);
        IList<Customer> SearchByName(string name);
        bool doExist(int id);
        bool doEmailExist(string email);
    }
}
