using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BigOn.Domain.Models.DataContents;
using BigOn.Domain.Models.Entities;

namespace BigOn.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ColorsController : ControllerBase
    {
        private readonly BigOnDbContext db;

        public ColorsController(BigOnDbContext db)
        {
            this.db = db;
        }

        // GET: api/Colors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductColor>>> GetProductColors()
        {
            return await db.ProductColors.ToListAsync();
        }

        // GET: api/Colors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductColor>> GetProductColor(int id)
        {
            var productColor = await db.ProductColors.FindAsync(id);

            if (productColor == null)
            {
                return NotFound();
            }

            return productColor;
        }

        // PUT: api/Colors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductColor(int id, [FromBody]ProductColor productColor)
        {
            if (id != productColor.Id)
            {
                return BadRequest();
            }

            db.Entry(productColor).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductColorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Colors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductColor>> PostProductColor(ProductColor productColor)
        {
            db.ProductColors.Add(productColor);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetProductColor", new { id = productColor.Id }, productColor);
        }

        // DELETE: api/Colors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductColor(int id)
        {
            var productColor = await db.ProductColors.FindAsync(id);
            if (productColor == null)
            {
                return NotFound();
            }

            db.ProductColors.Remove(productColor);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductColorExists(int id)
        {
            return db.ProductColors.Any(e => e.Id == id);
        }
    }
}
