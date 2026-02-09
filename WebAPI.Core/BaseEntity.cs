using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Core
{
    internal class BaseEntity<Type>: IEntity, IDateEntity
    {
         
        public Type Id { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
 
    }
}
