using Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class BaseEntity : IEntity
    {
        public int Id { get; set; }
        public int CreatedUserId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int LastUpdatedUserId { get; set; }
        public DateTime LastUpdatedDate { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public bool isDeleted { get; set; } = false;
    }
}
