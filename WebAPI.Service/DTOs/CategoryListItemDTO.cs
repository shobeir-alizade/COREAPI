using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Service.DTOs
{
    public class CategoryListItemDTO: BaseItemDTO
    {
       
        public string Name { get; set; }

        public int ParentId { get; set; }

        public string ParentName { get; set; }

        public int? ChildCount { get; set; }

        public int? ProductCount { get; set; }

      
    }
}
