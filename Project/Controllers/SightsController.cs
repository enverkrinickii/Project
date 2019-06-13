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
using Project.Models.ViewModels;

namespace Project.Controllers
{
    public class SightsController : ApiController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Sights
        public IEnumerable<SightViewModel> GetSights(string language)
        {
            var sights = new List<SightViewModel>();

            foreach (var sight in _db.Sights)
            {
                var sightViewModel = (SightViewModel)sight;
                sightViewModel.Title = _db.Localizations.FirstOrDefault(x => x.Key == sight.TitleKey && x.Culture == language)?
                    .Value;
                sightViewModel.Description = _db.Localizations.FirstOrDefault(x => x.Key == sight.DescriptionKey && x.Culture == language)?
                    .Value;
                sights.Add(sightViewModel);
            }

            return sights;
        }

        // GET: api/Sights/5
        [ResponseType(typeof(SightViewModel))]
        public async Task<IHttpActionResult> GetSight(int id, string language)
        {
            var sight = await _db.Sights.FindAsync(id);
            if (sight == null)
            {
                return NotFound();
            }

            var sightViewModel = (SightViewModel)sight;
            sightViewModel.Title = _db.Localizations.FirstOrDefault(x => x.Key == sight.TitleKey && x.Culture == language)?
                .Value;
            sightViewModel.Description = _db.Localizations.FirstOrDefault(x => x.Key == sight.DescriptionKey && x.Culture == language)?
                .Value;

            return Ok(sightViewModel);
        }

        // PUT: api/Sights/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutSight(int id, SightViewModel sightViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != sightViewModel.SigthId)
            {
                return BadRequest();
            }

            var sight = (Sight)sightViewModel;

            var title = _db.Localizations.FirstOrDefault(x => x.Key == sight.TitleKey && x.Culture == language);

            var description =
                _db.Localizations.FirstOrDefault(x => x.Key == sight.DescriptionKey && x.Culture == language);

            if (description == null)
            {
                description = new Localization
                {
                    Value = sightViewModel.Description,
                    Key = sightViewModel.DescriptionKey,
                    Culture = language,
                };
                _db.Localizations.Add(description);
            }
            else
            {
                if (description.Value != sightViewModel.Description)
                {
                    _db.Entry(description).State = EntityState.Modified;
                }
            }

            if (title == null)
            {
                title = new Localization
                {
                    Value = sightViewModel.Title,
                    Key = sightViewModel.TitleKey,
                    Culture = language,
                };
                _db.Localizations.Add(title);
            }
            else
            {
                if (title.Value != sightViewModel.Title)
                {
                    _db.Entry(title).State = EntityState.Modified;
                }
            }

            _db.Entry(sight).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
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
        [ResponseType(typeof(SightViewModel))]
        public async Task<IHttpActionResult> PostSight(SightViewModel sightViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var sight = (Sight)sightViewModel;

            sight.TitleKey = Guid.NewGuid().ToString() + nameof(sightViewModel.Title);
            sight.DescriptionKey = Guid.NewGuid().ToString() + nameof(sightViewModel.Description);

            var description = new Localization
            {
                Value = sightViewModel.Description,
                Key = sightViewModel.DescriptionKey,
                Culture = language,
            };
            _db.Localizations.Add(description);

            var title = new Localization
            {
                Value = sightViewModel.Title,
                Key = sightViewModel.TitleKey,
                Culture = language,
            };
            _db.Localizations.Add(title);

            _db.Sights.Add(sight);
            await _db.SaveChangesAsync();

            sightViewModel.SigthId = sight.SigthId;

            return CreatedAtRoute("DefaultApi", new { id = sightViewModel.SigthId }, sightViewModel);
        }

        // DELETE: api/Sights/5
        [ResponseType(typeof(SightViewModel))]
        public async Task<IHttpActionResult> DeleteSight(int id)
        {
            var sight = await _db.Sights.FindAsync(id);
            if (sight == null)
            {
                return NotFound();
            }

            var titles = _db.Localizations.Where(x => x.Key == sight.TitleKey);
            var descriptions = _db.Localizations.Where(x => x.Key == sight.DescriptionKey);

            if (titles.Any())
            {
                _db.Localizations.RemoveRange(titles);
            }

            if (descriptions.Any())
            {
                _db.Localizations.RemoveRange(descriptions);
            }

            _db.Sights.Remove(sight);
            await _db.SaveChangesAsync();

            return Ok(sight);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SightExists(int id)
        {
            return _db.Sights.Count(e => e.SigthId == id) > 0;
        }
    }
}