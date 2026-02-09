using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Service.DTOs
{
    public class ProductCategoryDTO : BaseDTO
    {
        public int ProductID { get; set; }
        public int CategoryID { get; set; }
    }
}
