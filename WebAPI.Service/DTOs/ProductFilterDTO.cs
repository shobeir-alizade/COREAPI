using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Service.DTOs
{
    public class ProductFilterDTO : BaseDTO
    {
        public string ProductName { get; set; }
        public  int? FromPrice { get; set; }
        public int? ToPrice { get; set; }
        public string Sku { get; set; }
        public bool? IsAvailable { get; set; }
        public DateTime? FromPublishDate { get; set; }
        public DateTime? ToPublishDate { get; set; }
        public int? CategoryId { get; set; }
    }

}
