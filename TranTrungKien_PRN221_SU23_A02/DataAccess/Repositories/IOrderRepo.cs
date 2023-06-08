using System;
using System.Collections.Generic;
using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface IOrderRepo
    {
        Order GetOrder(int id);
        IList<Order> GetOrders();
        IList<Order> GetOrdersForReport(DateTime startDate, DateTime endDate);
        IList<Order> GetOrderByCustomerId(int customerId);
        void Save(Order order);
        void Delete(Order order);
        void Update(Order order);
        bool Exist(int id);
    }
}
