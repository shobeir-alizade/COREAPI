using System;
using System.Collections.Generic;
using System.Text;

namespace WebAPI.Service.DTOs
{
    public class ProductListItem : BaseItemDTO
    {
        public string ProductName { get; set; }
        public int Price { get; set; }
        public string Sku { get; set; }
        public int StockQuantity { get; set; }
        public DateTime PublishDate { get; set; }
        public string LocalPublishDate { get; set; }
        public List<int> CategoryIds { get; set; }
        public List<string> CategoryNames { get; set; }
    }

}
