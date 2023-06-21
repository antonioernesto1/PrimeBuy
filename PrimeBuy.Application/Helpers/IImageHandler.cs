using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PrimeBuy.Application.Helpers
{
    public interface IImageHandler
    {
        public string UploadImage(IFormFile image);
    }
}