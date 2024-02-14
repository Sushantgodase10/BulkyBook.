using Bulky.DataAccess.Data;
using Bulky.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulky.DataAccess.Repositories.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product entity)
        {
            var objFromDb = _db.Products.FirstOrDefault(o => o.CategoryId == entity.CategoryId);
            if (objFromDb != null)
            {
                objFromDb.CategoryId = entity.CategoryId;
                objFromDb.Title = entity.Title;
                objFromDb.Auther = entity.Auther;
                objFromDb.ISBN = entity.ISBN;
                objFromDb.Price = entity.Price;
                objFromDb.ListPrice = entity.ListPrice;
                objFromDb.Price50 = entity.Price50;
                objFromDb.Price100 = entity.Price100;
                objFromDb.Description = entity.Description;
                objFromDb.CategoryId = entity.CategoryId;
                if (entity.ImageUrl != null)
                {
                    objFromDb.ImageUrl= entity.ImageUrl;
                }
            }
        }
    }
}
