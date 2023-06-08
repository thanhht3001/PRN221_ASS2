using System.Collections.Generic;
using BusinessObject.Models;
using DataAccess.DAO;

namespace DataAccess.Repositories
{
    public class FlowerRepo : IFlowerRepo
    {
        public FlowerBouquet GetFlower(int id) => FlowerBouquetDAO.GetFlower(id);
        public IList<FlowerBouquet> GetFlowers() => FlowerBouquetDAO.GetFlowers();
        public void Save(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.Save(flowerBouquet);
        public void Update(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.Update(flowerBouquet);
        public void Delete(FlowerBouquet flowerBouquet) => FlowerBouquetDAO.Delete(flowerBouquet);
        public IList<FlowerBouquet> SearchByName(string name) => FlowerBouquetDAO.SearchByName(name);
        public bool Exist(int id) => FlowerBouquetDAO.Exist(id);
    }
}
