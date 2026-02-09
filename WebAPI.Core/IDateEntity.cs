using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Core
{
    public interface IDateEntity
    {
         DateTime CreateDate { get; set; }
         DateTime UpdateDate { get; set; }
    }
}
