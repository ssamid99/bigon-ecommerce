using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.AppCode.Infracture;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace BigOn.Domain.Business.BlogPostModule
{
    public class BlogPostPutCommand : IRequest<JsonResponse>
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int CategoryId { get; set; }
        public IFormFile Image { get; set; }
        public int[] tagIds { get; set; }
        public string ImagePath { get; set; }

        public class BlogPostPutCommandHandler : IRequestHandler<BlogPostPutCommand, JsonResponse>
        {
            private readonly BigOnDbContext db;
            private readonly IHostEnvironment env;
            private readonly IConfiguration configuration;

            public BlogPostPutCommandHandler(BigOnDbContext db, IHostEnvironment env, IConfiguration configuration)
            {
                this.db = db;
                this.env = env;
                this.configuration = configuration;
            }
            public async Task<JsonResponse> Handle(BlogPostPutCommand request, CancellationToken cancellationToken)
            {
                var entity = db.BlogPosts
                     .Include(bp => bp.TagCloud)
                     .FirstOrDefault(bg => bg.Id == request.Id && bg.DeletedDate == null);

                if (entity == null)
                {
                    return null;
                }

                entity.Title = request.Title;
                entity.Body = request.Body;
                entity.CategoryId = request.CategoryId; // ?? error verirdi
                if (request.Image == null)
                    goto end;

                string extension = Path.GetExtension(request.Image.FileName); //.jpg-ni goturur
                request.ImagePath = $"blogpost-{Guid.NewGuid().ToString().ToLower()}{extension}";

                string folder = configuration["uploads:folder"];

                string fullPath = null;

                if (!string.IsNullOrWhiteSpace(folder))
                {
                    fullPath = folder.GetImagePhysicalPath(request.ImagePath);
                }
                else
                {
                    fullPath = env.GetImagePhysicalPath(request.ImagePath);
                }

                using (var fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
                {
                    request.Image.CopyTo(fs);
                }

                string oldPath = null;

                if (!string.IsNullOrWhiteSpace(folder))
                {
                    oldPath = folder.GetImagePhysicalPath(entity.ImagePath);
                    System.IO.File.Move(oldPath, folder.GetImagePhysicalPath($"archive{DateTime.Now:yyyyMMdd}-{entity.ImagePath}"));
                }
                else
                {
                    oldPath = env.GetImagePhysicalPath(entity.ImagePath);
                    System.IO.File.Move(oldPath, env.GetImagePhysicalPath($"archive{DateTime.Now:yyyyMMdd}-{entity.ImagePath}"));
                }

                //if (System.IO.File.Exists(oldPath))
                //{
                //    System.IO.File.Delete(oldPath);
                //}

                entity.ImagePath = request.ImagePath;

            end:
                if (string.IsNullOrWhiteSpace(entity.Slug))
                {
                    entity.Slug = request.Title.ToSlug();
                }

                if (request.tagIds == null && entity.TagCloud.Any())
                {
                    foreach (var tagItem in entity.TagCloud)
                    {
                        db.BlogPostTagCloud.Remove(tagItem);
                    }
                }
                else if (request.tagIds != null)
                {
                    #region database de evvel olub indi olmayan tag-lerin silinmesi
                    var exceptedIds = db.BlogPostTagCloud.Where(tc => tc.BlogPostId == entity.Id).Select(tc => tc.TagId).ToList()
                                        .Except(request.tagIds).ToArray();

                    if (exceptedIds.Length > 0)
                    {
                        foreach (var exceptedId in exceptedIds)
                        {
                            var tagItem = db.BlogPostTagCloud.FirstOrDefault(tc => tc.TagId == exceptedId
                                                         && tc.BlogPostId == entity.Id);
                            if (tagItem != null)
                            {
                                db.BlogPostTagCloud.Remove(tagItem);
                            }
                        }
                    }
                    #endregion

                    #region database de evvel olmayan indi elave olunan tag-lerin add olunmasi
                    var newExceptedIds = request.tagIds.Except(db.BlogPostTagCloud.Where(tc => tc.BlogPostId == entity.Id).Select(tc => tc.TagId).ToList()).ToArray();

                    if (newExceptedIds.Length > 0)
                    {
                        foreach (var exceptedId in newExceptedIds)
                        {
                            var tagItem = new BlogPostTagItem();
                            tagItem.TagId = exceptedId;
                            tagItem.BlogPostId = entity.Id;

                            await db.BlogPostTagCloud.AddAsync(tagItem);
                        }
                    }
                    #endregion
                }



                await db.SaveChangesAsync(cancellationToken);
                return new JsonResponse
                {
                    Error = false,
                    Message = "Success"
                };
            }
        }
    }
}
