using System.Collections.Generic;
using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        public Customer Login(string email, string password) => CustomerDAO.Login(email, password);
        public Customer GetCustomer(int id) => CustomerDAO.GetCustomer(id);
        public IList<Customer> GetCustomers() => CustomerDAO.GetCustomers();
        public void Save(Customer customer) => CustomerDAO.SaveCustomer(customer);
        public void Update(Customer customer) => CustomerDAO.Update(customer);
        public void Delete(Customer customer) => CustomerDAO.Delete(customer);
        public IList<Customer> SearchByName(string name) => CustomerDAO.SearchByName(name);
        public bool doExist(int id) => CustomerDAO.doExist(id);
        public bool doEmailExist(string email) => CustomerDAO.doEmailExist(email);
    }
}
