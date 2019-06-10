using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Project.Models;

namespace Project.Controllers
{
    public class SightsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Sights
        public IQueryable<Sight> GetSights()
        {
            return db.Sights;
        }

        // GET: api/Sights/5
        [ResponseType(typeof(Sight))]
        public async Task<IHttpActionResult> GetSight(int id)
        {
            Sight sight = await db.Sights.FindAsync(id);
            if (sight == null)
            {
                return NotFound();
            }

            return Ok(sight);
        }

        // PUT: api/Sights/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSight(int id, Sight sight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sight.SightId)
            {
                return BadRequest();
            }

            db.Entry(sight).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SightExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Sights
        [ResponseType(typeof(Sight))]
        public async Task<IHttpActionResult> PostSight(Sight sight)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Sights.Add(sight);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = sight.SightId }, sight);
        }

        // DELETE: api/Sights/5
        [ResponseType(typeof(Sight))]
        public async Task<IHttpActionResult> DeleteSight(int id)
        {
            Sight sight = await db.Sights.FindAsync(id);
            if (sight == null)
            {
                return NotFound();
            }

            db.Sights.Remove(sight);
            await db.SaveChangesAsync();

            return Ok(sight);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SightExists(int id)
        {
            return db.Sights.Count(e => e.SightId == id) > 0;
        }
    }
}