using Core.Entities;
using Entities.Concrete;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Dtos
{
    public class StorageDto: Concrete.BaseEntity, IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserName { get; set; }
        public int UnitsInStock { get; set; }

        public bool IsReady { get; set; }

        public string Size { get; set; }
    }
}
