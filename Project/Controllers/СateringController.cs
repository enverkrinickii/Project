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
    public class СateringController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Сatering
        public IQueryable<Сatering> GetСaterings()
        {
            return db.Сaterings;
        }

        // GET: api/Сatering/5
        [ResponseType(typeof(Сatering))]
        public async Task<IHttpActionResult> GetСatering(int id)
        {
            Сatering сatering = await db.Сaterings.FindAsync(id);
            if (сatering == null)
            {
                return NotFound();
            }

            return Ok(сatering);
        }

        // PUT: api/Сatering/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutСatering(int id, Сatering сatering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != сatering.CateringId)
            {
                return BadRequest();
            }

            db.Entry(сatering).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!СateringExists(id))
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

        // POST: api/Сatering
        [ResponseType(typeof(Сatering))]
        public async Task<IHttpActionResult> PostСatering(Сatering сatering)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Сaterings.Add(сatering);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = сatering.CateringId }, сatering);
        }

        // DELETE: api/Сatering/5
        [ResponseType(typeof(Сatering))]
        public async Task<IHttpActionResult> DeleteСatering(int id)
        {
            Сatering сatering = await db.Сaterings.FindAsync(id);
            if (сatering == null)
            {
                return NotFound();
            }

            db.Сaterings.Remove(сatering);
            await db.SaveChangesAsync();

            return Ok(сatering);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool СateringExists(int id)
        {
            return db.Сaterings.Count(e => e.CateringId == id) > 0;
        }
    }
}