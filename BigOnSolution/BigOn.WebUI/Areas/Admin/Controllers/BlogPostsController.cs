using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using BigOn.Domain.AppCode.Extensions;
using BigOn.Domain.Migrations;
using static System.Net.Mime.MediaTypeNames;
using BigOn.Domain.Business.BlogPostModule;
using MediatR;
using BigOn.Domain.Business.ProductModule;

namespace BigOn.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogPostsController : Controller
    {
        private readonly BigOnDbContext db;
        private readonly IWebHostEnvironment env;
        private readonly IMediator mediator;

        public BlogPostsController(BigOnDbContext db, IWebHostEnvironment env, IMediator mediator)
        {
            this.db = db;
            this.env = env;
            this.mediator = mediator;
        }

        // GET: Admin/BlogPosts
        public async Task<IActionResult> Index()
        {
            var data = await db.BlogPosts
                .Where(bg => bg.DeletedDate == null)
                .ToListAsync();

            return View(data);
        }

        // GET: Admin/BlogPosts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts
                .Include(bp => bp.Category)
                .Include(bp => bp.TagCloud)
                .ThenInclude(bp => bp.Tag)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }

            return View(blogPost);
        }

        // GET: Admin/BlogPosts/Create
        public IActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name");
            ViewBag.Tag = new SelectList(db.Tags.Where(vb => vb.DeletedDate == null).ToList(), "Id", "Text");
            return View();
        }

        // POST: Admin/BlogPosts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPostPostCommand command)
        {
            if (command.Image == null)
            {
                ModelState.AddModelError("ImagePath", "Shekil Gonderilmelidir");
            }

            if (ModelState.IsValid)
            {
                var response = await mediator.Send(command);
                if (response.Error == false)
                {
                return RedirectToAction(nameof(Index));
                }
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name", command.CategoryId);
            ViewBag.Tag = new SelectList(db.Tags.Where(vb => vb.DeletedDate == null).ToList(), "Id", "Text");

            return View(command);
        }

        // GET: Admin/BlogPosts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blogPost = await db.BlogPosts
                .Include(bp=>bp.TagCloud)
                .FirstOrDefaultAsync(bp=>bp.Id == id);
            if (blogPost == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name", blogPost.CategoryId);
            ViewBag.Tag = new SelectList(db.Tags.Where(vb => vb.DeletedDate == null).ToList(), "Id", "Text");

            var editCommand = new BlogPostPutCommand();
            editCommand.Id = blogPost.Id;
            editCommand.Title = blogPost.Title;
            editCommand.Body = blogPost.Body;
            editCommand.CategoryId = blogPost.CategoryId;
            editCommand.ImagePath = blogPost.ImagePath;
            editCommand.tagIds = blogPost.TagCloud.Select(tc=>tc.Id).ToArray();


            return View(editCommand);
        }

        // POST: Admin/BlogPosts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BlogPostPutCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }

            var response = await mediator.Send(command);

            if (response.Error==false)
            {
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryId = new SelectList(db.Categories.ToList(), "Id", "Name", command.CategoryId);
            ViewBag.Tag = new SelectList(db.Tags.Where(vb => vb.DeletedDate == null).ToList(), "Id", "Text");
            return View(command);
        }


        // POST: Admin/BlogPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(ProductRemoveCommand command)
        {
            var response = await mediator.Send(command);
            if(response == null)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool BlogPostExists(int id)
        {
            return db.BlogPosts.Any(e => e.Id == id);
        }
    }
}
