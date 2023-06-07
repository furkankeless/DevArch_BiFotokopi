
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
    public class OrderRepository : EfEntityRepositoryBase<Order, ProjectDbContext>, IOrderRepository
    {
        public OrderRepository(ProjectDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<OrderDto>> OrderDtos()
        {
            var list = await(from order in Context.Orders
                             join costumer in Context.Customers on order.CreatedUserId equals costumer.Id
                             join product in Context.Products on order.ProductId equals product.Id
                             where order.isDeleted == false
                             select new OrderDto
                             {
                                 Id = order.Id,
                                 CostumerName=costumer.CustomerName,
                                 ProductName=product.ProductName,
                                 LastUpdatedUserId=order.Id,
                                 Status=order.Status,
                                 LastUpdatedDate=order.LastUpdatedDate,
                                 isDeleted=order.isDeleted,
                                 CreatedDate = order.CreatedDate,
                                 CreatedUserId = order.CreatedUserId,
                                 Amount=order.Amount,
                                 CustomerId=order.CustomerId,
                                 ProductId = order.ProductId,
                                 Size = order.Size
                                 
                                 
                               
                             }).ToListAsync();

            return list;
        }
    }
}
