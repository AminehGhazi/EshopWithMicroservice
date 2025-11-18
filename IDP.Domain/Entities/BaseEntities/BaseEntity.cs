using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace IDP.Domain.Entities.BaseEntities
{
    public  class BaseEntity
    {
        public BaseEntity()
        {
            this.CreateDate = DateTime.UtcNow;
        }
        [Key]
        public int Id { get; set; } 
        public DateTime  CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
    }
}
