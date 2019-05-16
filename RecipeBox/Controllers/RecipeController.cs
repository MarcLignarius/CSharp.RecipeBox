using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System;

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

        [HttpPost("/recipes")]
        public ActionResult Create(string name, string ingredients, string instructions)
        {
            Recipe newRecipe = new Recipe(name, ingredients, instructions);
            newRecipe.Save();
            List<Recipe> allRecipes = Recipe.GetAll();
            return View("Index", allRecipes);
        }

        [HttpGet("/recipes/new")]
        public ActionResult New()
        {
            return View();
        }

        [HttpGet("/recipes/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Recipe selectedRecipe = Recipe.Find(id);
            List<Cuisine> recipeCuisines = selectedRecipe.GetCuisines();
            List<Cuisine> allCuisines = Cuisine.GetAll();
            model.Add("selectedRecipe", selectedRecipe);
            model.Add("recipeCuisines", recipeCuisines);
            model.Add("allCuisines", allCuisines);
            return View(model);
        }

        [HttpPost("/recipes/{recipe_id}/cuisines/new")]
        public ActionResult AddCuisine(int recipe_id, int cuisine_id)
        {
            Recipe recipe = Recipe.Find(recipe_id);
            Cuisine cuisine = Cuisine.Find(cuisine_id);
            recipe.AddCuisine(cuisine);
            return RedirectToAction("Show",  new { id = recipe_id });
        }

        // [HttpPost("/cuisines/{cuisineId}/recipes/{recipeId}")]
        // public ActionResult Update(int cuisineId, int recipeId, string newDescription, DateTime newDueDate, bool newCompleted)
        // {
        //     Recipe recipe = Recipe.Find(recipeId);
        //     recipe.Edit(newDescription, newDueDate, newCompleted);
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine cuisine = Cuisine.Find(cuisineId);
        //     model.Add("cuisine", cuisine);
        //     model.Add("recipe", recipe);
        //     return View("Show", model);
        // }

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
        // public ActionResult Update(int cuisineId, int recipeId, string newDescription)
        // {
        //     Recipe recipe = Recipe.Find(recipeId);
        //     recipe.Edit(newDescription);
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine cuisine = Cuisine.Find(cuisineId);
        //     model.Add("cuisine", cuisine);
        //     model.Add("recipe", recipe);
        //     return View("Show", model);
        // }

    }
}
