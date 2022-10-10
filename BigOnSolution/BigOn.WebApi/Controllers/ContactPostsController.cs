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
    public class ContactPostsController : ControllerBase
    {
        private readonly BigOnDbContext db;

        public ContactPostsController(BigOnDbContext db)
        {
            this.db = db;
        }

        // GET: api/ContactPosts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactPost>>> GetContactPosts()
        {
            return await db.ContactPosts.ToListAsync();
        }

        // GET: api/ContactPosts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactPost>> GetContactPost(int id)
        {
            var contactPost = await db.ContactPosts.FindAsync(id);

            if (contactPost == null)
            {
                return NotFound();
            }

            return contactPost;
        }

        // PUT: api/ContactPosts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactPost(int id, ContactPost contactPost)
        {
            if (id != contactPost.Id)
            {
                return BadRequest();
            }

            db.Entry(contactPost).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactPostExists(id))
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

        // POST: api/ContactPosts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactPost>> PostContactPost(ContactPost contactPost)
        {
            db.ContactPosts.Add(contactPost);
            await db.SaveChangesAsync();

            return CreatedAtAction("GetContactPost", new { id = contactPost.Id }, contactPost);
        }

        // DELETE: api/ContactPosts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactPost(int id)
        {
            var contactPost = await db.ContactPosts.FindAsync(id);
            if (contactPost == null)
            {
                return NotFound();
            }

            db.ContactPosts.Remove(contactPost);
            await db.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactPostExists(int id)
        {
            return db.ContactPosts.Any(e => e.Id == id);
        }
    }
}
