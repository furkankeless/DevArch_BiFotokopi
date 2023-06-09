
using System;
using System.Linq;
using Core.DataAccess.EntityFramework;
using Entities.Concrete;
using DataAccess.Concrete.EntityFramework.Contexts;
using DataAccess.Abstract;
using System.Threading.Tasks;
using System.Collections.Generic;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class ProductRepository : EfEntityRepositoryBase<Product, ProjectDbContext>, IProductRepository
    {
        public ProductRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<Product> Delete2(int productId)
        {
            var product = await Context.Products.FindAsync(productId); // Ürünü veritabanından bul

            if (product != null)
            {
                product.isDeleted = true; // isDeleted sütununu true olarak güncelle
                await Context.SaveChangesAsync(); // Değişiklikleri veritabanına kaydet
            }

            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetProductsDtos()
        {
            var list = await(from p in Context.Products
                             join user in Context.Users on p.CreatedUserId equals user.UserId
                             where p.isDeleted == false
                             select new ProductDto
                             {
                                 Id = p.Id,
                                 CreatedUserId = p.CreatedUserId,
                                 UserName = user.FullName,
                                 CreatedDate = p.CreatedDate,
                                 isDeleted = p.isDeleted,
                                 LastUpdatedDate = p.LastUpdatedDate,
                                 LastUpdatedUserId = p.LastUpdatedUserId,
                                 Status = p.Status,
                                 UserId=user.UserId,
                                 ProductColor=p.ProductColor,
                                 ProductName=p.ProductName,
                                 Size=p.Size,
                             }).ToListAsync();

            return list;
        }

        public async Task<bool> GetSize(int productId, string size)
        {
            var isExist = await Context.Products.AnyAsync(u=> u.Id == productId && u.Size != size);
             
            return isExist;
                           

        }
    }
    
}
