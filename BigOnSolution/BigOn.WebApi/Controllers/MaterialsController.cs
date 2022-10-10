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
    public class MaterialsController : ControllerBase
    {
        private readonly BigOnDbContext db;

        public MaterialsController(BigOnDbContext db)
        {
            this.db = db;
        }

        // GET: api/Materials
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductMaterial>>> GetProductMaterials()
        {
            return await db.ProductMaterials.ToListAsync();
        }

        // GET: api/Materials/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductMaterial>> GetProductMaterial(int id)
        {
            var productMaterial = await db.ProductMaterials.FindAsync(id);

            if (productMaterial == null)
            {
                return NotFound();
            }

            return productMaterial;
        }

        // PUT: api/Materials/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProductMaterial(int id, ProductMaterial productMaterial)
        {
            if (id != productMaterial.Id)
            {
                return BadRequest();
            }

            db.Entry(productMaterial).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductMaterialExists(id))
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

        // POST: api/Materials
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ProductMaterial>> PostProductMaterial(ProductMaterial productMaterial)
        {
            db.ProductMaterials.Add(productMaterial);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetProductMaterial", new { id = productMaterial.Id }, productMaterial);
        }

        // DELETE: api/Materials/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductMaterial(int id)
        {
            var productMaterial = await db.ProductMaterials.FindAsync(id);
            if (productMaterial == null)
            {
                return NotFound();
            }

            db.ProductMaterials.Remove(productMaterial);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductMaterialExists(int id)
        {
            return db.ProductMaterials.Any(e => e.Id == id);
        }
    }
}
