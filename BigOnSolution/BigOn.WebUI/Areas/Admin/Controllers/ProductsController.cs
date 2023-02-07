using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;
using BigOn.Domain.Business.ProductModule;
using MediatR;
using BigOn.Domain.Business.BlogPostModule;
using BigOn.Domain.Migrations;
using BigOn.Domain.AppCode.Extensions;
using System.Threading;

namespace BigOn.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly BigOnDbContext db;
        private readonly IMediator mediator;

        public ProductsController(BigOnDbContext db, IMediator mediator)
        {
            this.db = db;
            this.mediator = mediator;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index(ProductGetAllQuery query)
        {
            var response = await mediator.Send(query);
            return View(response);
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(ProductGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if(response == null)
            {
                return NotFound();
            }
            return View(response);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name");
            ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductPostCommand command)
        {
            var response = await mediator.Send(command); 
            if(response == null)
            {
                ViewBag.CategoryId = new SelectList(db.Categories, "Id", "Name", command.CategoryId);
                ViewBag.BrandId = new SelectList(db.Brands, "Id", "Name", command.BrandId);

              return View(command);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(ProductGetSingleQuery query)
        {
            var response = await mediator.Send(query);
            if (response == null)
            {
                return NotFound();
            }

            ViewData["BrandId"] = new SelectList(db.Brands, "Id", "Name", response.BrandId);
            ViewData["CategoryId"] = new SelectList(db.Categories, "Id", "Name", response.CategoryId);

            var editCommand = new ProductPutCommand();
            editCommand.Id = response.Id;
            editCommand.Name = response.Name;
            editCommand.Rate = response.Rate;
            editCommand.Price = response.Price;
            editCommand.ShortDescription = response.ShortDescription;
            editCommand.Description = response.Description;
            editCommand.StockKeepingUnit = response.StockKeepingUnit;
            editCommand.CategoryId = response.CategoryId;
            editCommand.BrandId = response.BrandId;

            editCommand.Images = response.Images.Select(x => new ImageItem
            {
                Id = x.Id,
                TempPath = x.Name,
                IsMain = x.IsMain
            }).ToArray();
            return View(editCommand);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductPutCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }
            var response = await mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

       

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            //var response = await mediator.Send(command);
            
            //if(response == null)
            //{
            //    return NotFound();
            //}
            var data = db.Products.FirstOrDefault(m => m.Id == id && m.DeletedDate == null);

            if (data == null)
            {
                return null;
            }
            data.DeletedDate = DateTime.UtcNow.AddHours(4);
            data.DeletedByUserId = User.GetCurrentUserId();
            await db.SaveChangesAsync();
            var product = await mediator.Send(new ProductGetAllQuery());
            return PartialView("_ProductList", product);
        }

        private bool ProductExists(int id)
        {
            return db.Products.Any(e => e.Id == id);
        }
    }
}
