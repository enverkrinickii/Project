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
    public class IngredientsController : ApiController
    {
        private ApplicationDbContext _db = new ApplicationDbContext();

        // GET: api/Ingredients
        public IEnumerable<IngredientViewModel> GetIngredients(string language)
        {
            var ingredients = new List<IngredientViewModel>();

            foreach (var ingredient in _db.Ingredients)
            {
                var ingredientViewModel = (IngredientViewModel)ingredient;
                ingredientViewModel.Name = _db.Localizations.FirstOrDefault(x => x.Key == ingredient.NameKey && x.Culture == language)?
                    .Value;
                
                ingredients.Add(ingredientViewModel);
            }

            return ingredients;
        }

        // GET: api/Ingredients/5
        [ResponseType(typeof(IngredientViewModel))]
        public async Task<IHttpActionResult> GetIngredient(string id, string language)
        {
            var ingredient = await _db.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            var ingredientViewModel = (IngredientViewModel)ingredient;
            ingredientViewModel.Name = _db.Localizations.FirstOrDefault(x => x.Key == ingredient.NameKey && x.Culture == language)?
                .Value;

            return Ok(ingredientViewModel);
        }

        // PUT: api/Ingredients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutIngredient(string id, IngredientViewModel ingredientViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ingredientViewModel.IngredientId)
            {
                return BadRequest();
            }

            var ingredient = (Ingredient)ingredientViewModel;

            var title = _db.Localizations.FirstOrDefault(x => x.Key == ingredient.NameKey && x.Culture == language);

            if (title == null)
            {
                title = new Localization
                {
                    Value = ingredientViewModel.Name,
                    Key = ingredientViewModel.NameKey,
                    Culture = language,
                };
                _db.Localizations.Add(title);
            }
            else
            {
                if (title.Value != ingredientViewModel.Name)
                {
                    _db.Entry(title).State = EntityState.Modified;
                }
            }

            _db.Entry(ingredient).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
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

        // POST: api/Ingredients
        [ResponseType(typeof(Ingredient))]
        public async Task<IHttpActionResult> PostIngredient(IngredientViewModel ingredientViewModel, string language)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ingredient = (Ingredient)ingredientViewModel;

            var title = new Localization
            {
                Value = ingredientViewModel.Name,
                Key = ingredientViewModel.NameKey,
                Culture = language,
            };
            _db.Localizations.Add(title);

            _db.Ingredients.Add(ingredient);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (IngredientExists(ingredientViewModel.IngredientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            ingredientViewModel.IngredientId = ingredient.IngredientId;

            return CreatedAtRoute("DefaultApi", new { id = ingredientViewModel.IngredientId }, ingredientViewModel);
        }

        // DELETE: api/Ingredients/5
        [ResponseType(typeof(Ingredient))]
        public async Task<IHttpActionResult> DeleteIngredient(string id)
        {
            var ingredient = await _db.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            var titles = _db.Localizations.Where(x => x.Key == ingredient.NameKey);

            if (titles.Any())
            {
                _db.Localizations.RemoveRange(titles);
            }

            _db.Ingredients.Remove(ingredient);
            await _db.SaveChangesAsync();

            return Ok(ingredient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool IngredientExists(string id)
        {
            return _db.Ingredients.Count(e => e.IngredientId == id) > 0;
        }
    }
}