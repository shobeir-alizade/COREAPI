using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace WebAPI.Service.DTOs
{

  
    public class PictureUploadBase64DTO : BaseDTO
    {
        public string File { get; set; }
        public string ContentType { get; set; }
        public string fileExtension { get; set; }

    }
  
}
