using WebAPI.Service.Catalog;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Service.DTOs
{
    public class ProductDTO : BaseEntityDTO
    {
     
        [Required]
     
        public string ProductName { get; set; }
        public int Price { get; set; }
      
        public string Sku { get; set; }
        public int StockQuantity { get; set; }
        public DateTime PublsihDate { get; set; }
    }


   


   
}
