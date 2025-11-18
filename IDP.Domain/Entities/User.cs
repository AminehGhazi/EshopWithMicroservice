using IDP.Domain.Entities.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDP.Domain.Entities
{
    public  class User :BaseEntity 
    {
        public required string FullName;
        public required string NumberCode;
        public required string UserName;
        public required string Password;
        public required string Salt;
    }
}
