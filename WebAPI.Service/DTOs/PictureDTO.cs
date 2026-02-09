using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace WebAPI.Service.DTOs
{

    public class PictureDTO : BaseEntityDTO
    {
        public string VirtualPath { get; set; }
        public string MimeType { get; set; }
    }
    

}
