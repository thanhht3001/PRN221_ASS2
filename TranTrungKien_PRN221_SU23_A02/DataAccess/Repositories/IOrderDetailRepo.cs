using System.Collections.Generic;
using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface IOrderDetailRepo
    {
        OrderDetail SearchOrderDetailByOrderIdAndByFlowerBouquetId(int orderId, int flowerBouquetId);
        IList<OrderDetail> GetOrderDetailByOrderId(int id);
        void Save(OrderDetail orderDetail);
        void Delete(OrderDetail orderDetail);
        void Update(OrderDetail orderDetail);
        bool Exist(int id);
    }
}
