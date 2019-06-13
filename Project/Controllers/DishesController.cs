using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Project.Models;
using Project.Models.ViewModels;

namespace Project.Controllers
{
    public class DishesController : ApiController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Dishes
        public IEnumerable<DishViewModel> GetDishes(string language)
        {
            var dishes = new List<DishViewModel>();

            foreach (var dish in _db.Dishes)
            {
                var dishViewModel = (DishViewModel)dish;
                dishViewModel.Title = _db.Localizations.FirstOrDefault(x => x.Key == dish.TitleKey && x.Culture == language)?
                    .Value;
                dishViewModel.Description = _db.Localizations.FirstOrDefault(x => x.Key == dish.DescriptionKey && x.Culture == language)?
                    .Value;
                dishes.Add(dishViewModel);
            }

            return dishes;
        }

        // GET: api/Dishes/5
        [ResponseType(typeof(DishViewModel))]
        public IHttpActionResult GetDish(int id, string language)
        {
            var dish = _db.Dishes.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            var dishViewModel = (DishViewModel)dish;
            dishViewModel.Title = _db.Localizations.FirstOrDefault(x => x.Key == dish.TitleKey && x.Culture == language)?
                .Value;
            dishViewModel.Description = _db.Localizations.FirstOrDefault(x => x.Key == dish.DescriptionKey && x.Culture == language)?
                .Value;

            return Ok(dishViewModel);
        }

        // PUT: api/Dishes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDish(int id, DishViewModel dishViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dishViewModel.DishId)
            {
                return BadRequest();
            }

            var dish = (Dish) dishViewModel;

            var title = _db.Localizations.FirstOrDefault(x => x.Key == dish.TitleKey && x.Culture == language);

            var description =
                _db.Localizations.FirstOrDefault(x => x.Key == dish.DescriptionKey && x.Culture == language);

            if (description == null)
            {
                description = new Localization
                {
                    Value = dishViewModel.Description,
                    Key = dishViewModel.DescriptionKey,
                    Culture = language,
                };
                _db.Localizations.Add(description);
            }
            else
            {
                if (description.Value != dishViewModel.Description)
                {
                    _db.Entry(description).State = EntityState.Modified;
                }
            }

            if (title == null)
            {
                title = new Localization
                {
                    Value = dishViewModel.Title,
                    Key = dishViewModel.TitleKey,
                    Culture = language,
                };
                _db.Localizations.Add(title);
            }
            else
            {
                if (title.Value != dishViewModel.Title)
                {
                    _db.Entry(title).State = EntityState.Modified;
                }
            }

            _db.Entry(dish).State = EntityState.Modified;

            try
            {
                _db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DishExists(id))
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

        // POST: api/Dishes
        [ResponseType(typeof(DishViewModel))]
        public IHttpActionResult PostDish(DishViewModel dishViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dish = (Dish) dishViewModel;

            dish.TitleKey = Guid.NewGuid().ToString() + nameof(dishViewModel.Title);
            dish.DescriptionKey = Guid.NewGuid().ToString() + nameof(dishViewModel.Description);

            var description = new Localization
            {
                Value = dishViewModel.Description,
                Key = dishViewModel.DescriptionKey,
                Culture = language,
            };
            _db.Localizations.Add(description);

            var title = new Localization
            {
                Value = dishViewModel.Title,
                Key = dishViewModel.TitleKey,
                Culture = language,
            };
            _db.Localizations.Add(title);

            _db.Dishes.Add(dish);
            _db.SaveChanges();

            dishViewModel.DishId = dish.DishId;

            return CreatedAtRoute("DefaultApi", new { id = dish.DishId }, dishViewModel);
        }

        // DELETE: api/Dishes/5
        [ResponseType(typeof(Dish))]
        public IHttpActionResult DeleteDish(int id)
        {
            var dish = _db.Dishes.Find(id);
            if (dish == null)
            {
                return NotFound();
            }

            var titles = _db.Localizations.Where(x => x.Key == dish.TitleKey);
            var descriptions = _db.Localizations.Where(x => x.Key == dish.DescriptionKey);

            if (titles.Any())
            {
                _db.Localizations.RemoveRange(titles);
            }

            if (descriptions.Any())
            {
                _db.Localizations.RemoveRange(descriptions);
            }

            _db.Dishes.Remove(dish);
            _db.SaveChanges();

            return Ok(dish);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DishExists(int id)
        {
            return _db.Dishes.Count(e => e.DishId == id) > 0;
        }
    }
}