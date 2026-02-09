
using WebAPI.Service.Validators;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace WebAPI.Service.DTOs
{

    public class PictureUploadDTO : BaseDTO
    {
        [ImageValidation(ErrorMessage ="فرمت تصویر صحیح نیست")]
        public IFormFile File { get; set; }
        public string ContentType { get; set; }
        public string fileExtension { get; set; }
        
    }
   

  
}
