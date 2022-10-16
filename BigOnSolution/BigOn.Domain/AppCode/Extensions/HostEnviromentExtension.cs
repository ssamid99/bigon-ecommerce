using BigOn.Domain.Models.Entities;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigOn.Domain.AppCode.Extensions
{
    public static partial class Extension
    {
        public static string GetImagePhysicalPath(this IHostEnvironment env, string fileName)
        {
            return Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", fileName);
        }

        public static void ArchiveImages(this IHostEnvironment env, string fileName)
        {
            var imageActualPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", fileName);
            if (File.Exists(imageActualPath))
            {
            var imageNewPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", "images", $"archive-{DateTime.Now:yyyyMMdd}-{fileName}");
                File.Move(imageActualPath, imageNewPath);
            }
        }
    }
}
