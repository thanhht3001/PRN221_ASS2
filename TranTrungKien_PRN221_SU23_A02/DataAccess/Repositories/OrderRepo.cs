using System;
using System.Collections.Generic;
using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class OrderRepo : IOrderRepo
    {
        public Order GetOrder(int id) => OrderDAO.GetOrder(id);
        public IList<Order> GetOrders() => OrderDAO.GetOrders();
        public IList<Order> GetOrderByCustomerId(int customerId) => OrderDAO.GetOrderByCustomerId(customerId);
        public IList<Order> GetOrdersForReport(DateTime startDate, DateTime endDate) => OrderDAO.GetOrdersForReport(startDate, endDate);
        public void Save(Order order) => OrderDAO.Save(order);
        public void Delete(Order order) => OrderDAO.Delete(order);
        public void Update(Order order) => OrderDAO.Update(order);
        public bool Exist(int id) => OrderDAO.Exist(id);
    }
}
