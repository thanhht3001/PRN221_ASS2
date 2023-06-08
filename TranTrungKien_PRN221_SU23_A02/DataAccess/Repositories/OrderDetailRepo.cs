using System.Collections.Generic;
using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class OrderDetailRepo : IOrderDetailRepo
    {
        public OrderDetail SearchOrderDetailByOrderIdAndByFlowerBouquetId(int orderId, int flowerBouquetId) => OrderDetailDAO.SearchOrderDetailByOrderIdAndByFlowerBouquetId(orderId, flowerBouquetId);
        public IList<OrderDetail> GetOrderDetailByOrderId(int id) => OrderDetailDAO.GetOrderDetailByOrderId(id);
        public void Save(OrderDetail orderDetail) => OrderDetailDAO.Save(orderDetail);
        public void Delete(OrderDetail orderDetail) => OrderDetailDAO.Delete(orderDetail);
        public void Update(OrderDetail orderDetail) => OrderDetailDAO.Update(orderDetail);
        public bool Exist(int id) => OrderDetailDAO.Exist(id);
    }
}
