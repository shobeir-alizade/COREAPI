using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Service.DTOs
{
   

    public class CategoryDTO:BaseEntityDTO
    {
        public string Name { get; set; }

        public int ParentId { get; set; }

        public string ParentName { get; set; }
    }
}
