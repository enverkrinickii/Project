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
using Newtonsoft.Json;
using Project.Models;
using Project.Models.ViewModels;

namespace Project.Controllers
{
    public class СateringController : ApiController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Сatering
        public IEnumerable<CateringViewModel> GetСaterings(string language)
        {
            var cateringList = new List<CateringViewModel>();

            foreach (var catering in _db.Сaterings)
            {
                var cateringViewModel = (CateringViewModel) catering;
                cateringViewModel.Title = _db.Localizations.FirstOrDefault(x => x.Key == catering.TitleKey && x.Culture == language)?
                    .Value;
                cateringViewModel.Description = _db.Localizations.FirstOrDefault(x => x.Key == catering.DescriptionKey && x.Culture == language)?
                    .Value;
                cateringList.Add(cateringViewModel);
            }

            return cateringList;
        }

        // GET: api/Сatering/5
        [ResponseType(typeof(Catering))]
        public async Task<IHttpActionResult> GetСatering(int id, string language)
        {
            var сatering = await _db.Сaterings.FindAsync(id);
            if (сatering == null)
            {
                return NotFound();
            }
            var cateringViewModel = (CateringViewModel)сatering;
            cateringViewModel.Title = _db.Localizations.FirstOrDefault(x => x.Key == сatering.TitleKey && x.Culture == language)?
                .Value;
            cateringViewModel.Description = _db.Localizations.FirstOrDefault(x => x.Key == сatering.DescriptionKey && x.Culture == language)?
                .Value;

            return Ok(сatering);
        }

        // PUT: api/Сatering/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutСatering(int id, CateringViewModel сateringViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != сateringViewModel.CateringId)
            {
                return BadRequest();
            }

            var catering = (Catering)сateringViewModel;

            var title = _db.Localizations.FirstOrDefault(x => x.Key == catering.TitleKey && x.Culture == language);

            var description =
                _db.Localizations.FirstOrDefault(x => x.Key == catering.DescriptionKey && x.Culture == language);

            if (description == null)
            {
                description = new Localization
                {
                    Value = сateringViewModel.Description,
                    Key = сateringViewModel.DescriptionKey,
                    Culture = language,
                };
                _db.Localizations.Add(description);
            }
            else
            {
                if (description.Value != сateringViewModel.Description)
                {
                    _db.Entry(description).State = EntityState.Modified;
                }
            }

            if (title == null)
            {
                title = new Localization
                {
                    Value = сateringViewModel.Title,
                    Key = сateringViewModel.TitleKey,
                    Culture = language,
                };
                _db.Localizations.Add(title);
            }
            else
            {
                if (title.Value != сateringViewModel.Title)
                {
                    _db.Entry(title).State = EntityState.Modified;
                }
            }

            _db.Entry(catering).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
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
        [ResponseType(typeof(CateringViewModel))]
        public async Task<IHttpActionResult> PostСatering(CateringViewModel сateringViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var catering = (Catering)сateringViewModel;

            catering.TitleKey = Guid.NewGuid().ToString() + nameof(сateringViewModel.Title);
            catering.DescriptionKey = Guid.NewGuid().ToString() + nameof(сateringViewModel.Description);

            var description = new Localization
            {
                Value = сateringViewModel.Description,
                Key = сateringViewModel.DescriptionKey,
                Culture = language,
            };
            _db.Localizations.Add(description);

            var title = new Localization
            {
                Value = сateringViewModel.Title,
                Key = сateringViewModel.TitleKey,
                Culture = language,
            };
            _db.Localizations.Add(title);

            _db.Сaterings.Add(сateringViewModel);
            await _db.SaveChangesAsync();

            сateringViewModel.CateringId = catering.CateringId;

            return CreatedAtRoute("DefaultApi", new { id = сateringViewModel.CateringId }, сateringViewModel);
        }

        // DELETE: api/Сatering/5
        [ResponseType(typeof(Catering))]
        public async Task<IHttpActionResult> DeleteСatering(int id)
        {
            var сatering = await _db.Сaterings.FindAsync(id);
            if (сatering == null)
            {
                return NotFound();
            }

            var titles = _db.Localizations.Where(x => x.Key == сatering.TitleKey);
            var descriptions = _db.Localizations.Where(x => x.Key == сatering.DescriptionKey);

            if (titles.Any())
            {
                _db.Localizations.RemoveRange(titles);
            }

            if (descriptions.Any())
            {
                _db.Localizations.RemoveRange(descriptions);
            }

            _db.Сaterings.Remove(сatering);
            await _db.SaveChangesAsync();

            return Ok(сatering);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool СateringExists(int id)
        {
            return _db.Сaterings.Count(e => e.CateringId == id) > 0;
        }
    }
}