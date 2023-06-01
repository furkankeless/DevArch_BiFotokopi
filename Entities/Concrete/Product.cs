using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class Product:BaseEntity,IEntity
    {
        public string ProductName { get; set; }
        public string ProductColor { get; set; }
        public string Size { get; set; } 
    }
}
