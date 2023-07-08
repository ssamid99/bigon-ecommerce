using BigOn.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
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

        public static string GetImagePhysicalPath(this string folder, string fileName)
        {
            return Path.Combine(folder, fileName);
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
        public static void ArchiveImages(this string folder, string fileName)
        {
            var imageActualPath = Path.Combine(folder, fileName);
            if (File.Exists(imageActualPath))
            {
                var imageNewPath = Path.Combine(folder, $"archive-{DateTime.Now:yyyyMMdd}-{fileName}");
                File.Move(imageActualPath, imageNewPath);
            }
        }
    }
}
