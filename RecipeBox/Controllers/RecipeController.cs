using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;

namespace RecipeBox.Controllers
{
    public class RecipeController : Controller
    {

        [HttpGet("/recipes")]
        public ActionResult Index()
        {
            List<Recipe> allRecipes = Recipe.GetAll();
            return View(allRecipes);
        }

        [HttpGet("/recipes/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost("/recipes")]
        public ActionResult Create(string name, string ingredients, string instructions)
        {
            Recipe newRecipe = new Recipe(name, ingredients, instructions);
            newRecipe.Save();
            List<Recipe> allRecipes = Recipe.GetAll();
            return View("Index", allRecipes);
        }


        [HttpGet("/cuisines/{cuisineId}/recipes/new")]
        public ActionResult New(int cuisineId)
        {
            Cuisine cuisine = Cuisine.Find(cuisineId);
            return View(cuisine);
        }

      

        // [HttpGet("/cuisines/{cuisineId}/recipes/{recipeId}")]
        // public ActionResult Show(int cuisineId, int recipeId)
        // {
        //     Recipe recipe = Recipe.Find(recipeId);
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine cuisine = Cuisine.Find(cuisineId);
        //     model.Add("recipe", recipe);
        //     model.Add("cuisine", cuisine);
        //     return View(model);
        // }
        //
        // [HttpPost("/cuisines/{cuisineId}/recipes/{recipeId}/delete")]
        // public ActionResult Delete(int cuisineId, int recipeId)
        // {
        //     Recipe recipe = Recipe.Find(recipeId);
        //     recipe.Delete();
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine foundCuisine = Cuisine.Find(cuisineId);
        //     List<Recipe> cuisineRecipes = foundCuisine.GetRecipes();
        //     model.Add("recipes", cuisineRecipes);
        //     model.Add("cuisine", foundCuisine);
        //     return View(model);
        // }
        //
        // [HttpGet("/cuisines/{cuisineId}/recipes/{recipeId}/edit")]
        // public ActionResult Edit(int cuisineId, int recipeId)
        // {
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine cuisine = Cuisine.Find(cuisineId);
        //     model.Add("cuisine", cuisine);
        //     Recipe recipe = Recipe.Find(recipeId);
        //     model.Add("recipe", recipe);
        //     return View(model);
        // }
        //
        // [HttpPost("/cuisines/{cuisineId}/recipes/{recipeId}")]
        // public ActionResult Update(int cuisineId, int recipeId, string newIngredients, string newInstructions)
        // {
        //     Recipe recipe = Recipe.Find(recipeId);
        //     recipe.Edit(newPhoneNumber, newEmailAddress);
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine cuisine = Cuisine.Find(cuisineId);
        //     model.Add("cuisine", cuisine);
        //     model.Add("recipe", recipe);
        //     return View("Show", model);
        // }
    }
}
