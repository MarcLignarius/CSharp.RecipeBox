using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;

namespace RecipeBox.Controllers
{
    public class CuisineController : Controller
    {
        [HttpGet("/cuisines")]
        public ActionResult Index()
        {
            List<Cuisine> allCuisines = Cuisine.GetAll();
            return View(allCuisines);
        }

        [HttpGet("/cuisines/new")]
        public ActionResult New()
        {
          return View();
        }

        [HttpPost("/cuisines")]
        public ActionResult Create(string region, string description)
        {
            Cuisine newCuisine = new Cuisine(region, description);
            newCuisine.Save();
            List<Cuisine> allCuisines = Cuisine.GetAll();
            return View("Index", allCuisines);
        }

        [HttpGet("/cuisines/{id}")]
        public ActionResult Show(int id)
        {
            Dictionary<string, object> model = new Dictionary<string, object>();
            Cuisine selectedCuisine = Cuisine.Find(id);
            List<Recipe> cuisineRecipes = selectedCuisine.GetRecipes();
            List<Recipe> allRecipes = Recipe.GetAll();
            model.Add("cuisine", selectedCuisine);
             model.Add("cuisineRecipes", cuisineRecipes);
            model.Add("allRecipes", allRecipes);
            return View(model);
        }

        [HttpPost("/cuisines/{cuisineId}")]
        public ActionResult AddRecipe(int cuisineId, int recipeId)
        {
            Cuisine cuisine = Cuisine.Find(cuisineId);
            Recipe recipe = Recipe.Find(recipeId);
            cuisine.AddRecipe(recipe);
            List<Recipe> allRecipes = cuisine.GetRecipes();
            return RedirectToAction("Show",  new { id = cuisineId });
        }

        // [HttpPost("/cuisines/{cuisineId}/recipes")]
        // public ActionResult Create(int cuisineId, string firstName, string lastName, string phoneNumber, string emailAddress)
        // {
        //     Dictionary<string, object> model = new Dictionary<string, object>();
        //     Cuisine foundCuisine = Cuisine.Find(cuisineId);
        //     Recipe newRecipe = new Recipe(name, ingredients, instructions);
        //     newRecipe.Save();
        //     List<Recipe> cuisineRecipes = foundCuisine.GetRecipes();
        //     model.Add("recipes", cuisineRecipes);
        //     model.Add("cuisine", foundCuisine);
        //     return View("Show", model);
        // }
    }
}
