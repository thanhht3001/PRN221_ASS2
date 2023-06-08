using System.Collections.Generic;
using BusinessObject.Models;

namespace DataAccess.Repositories
{
    public interface IFlowerRepo
    {
        FlowerBouquet GetFlower(int id);
        IList<FlowerBouquet> GetFlowers();
        void Save(FlowerBouquet flowerBouquet);
        void Update(FlowerBouquet flowerBouquet);
        void Delete(FlowerBouquet flowerBouquet);
        public IList<FlowerBouquet> SearchByName(string name);
        bool Exist(int id);
    }
}
