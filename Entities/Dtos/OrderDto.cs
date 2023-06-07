
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class OrderDto: BaseEntity, Core.Entities.IDto
    {
        public string CostumerName { get; set; }

        public string ProductName { get; set; }

        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public string Size { get; set; }
    }
}
