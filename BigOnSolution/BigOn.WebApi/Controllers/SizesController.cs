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
    public class SizesController : ControllerBase
    {
        private readonly BigOnDbContext db;

        public SizesController(BigOnDbContext db)
        {
            this.db = db;
        }

        // GET: api/Sizes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductSize>>> GetProductSizes()
        {
            return await db.ProductSizes.ToListAsync();
        }

        // GET: api/Sizes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductSize>> GetProductSize(int id)
        {
            var productSize = await db.ProductSizes.FindAsync(id);

            if (productSize == null)
            {
                return NotFound();
            }

            return productSize;
        }

        // PUT: api/Sizes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductSize(int id, [FromBody]ProductSize productSize)
        {
            if (id!= productSize.Id)
            {
                return BadRequest();
            }

            db.Entry(productSize).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductSizeExists(id))
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

        // POST: api/Sizes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductSize>> PostProductSize(ProductSize productSize)
        {
            db.ProductSizes.Add(productSize);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetProductSize", new { id = productSize.Id }, productSize);
        }

        // DELETE: api/Sizes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSize(int id)
        {
            var productSize = await db.ProductSizes.FindAsync(id);
            if (productSize == null)
            {
                return NotFound();
            }

            db.ProductSizes.Remove(productSize);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductSizeExists(int id)
        {
            return db.ProductSizes.Any(e => e.Id == id);
        }
    }
}
