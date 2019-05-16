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

        [HttpPost("/cuisines")]
        public ActionResult Create(string region, string description)
        {
            Cuisine newCuisine = new Cuisine(region, description);
            newCuisine.Save();
            List<Cuisine> allCuisines = Cuisine.GetAll();
            return View("Index", allCuisines);
        }

        [HttpGet("/cuisines/new")]
        public ActionResult New()
        {
          return View();
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

        [HttpPost("/cuisines/{cuisine_id}/recipes/new")]
        public ActionResult AddRecipe(int cuisine_id, int recipe_id)
        {
            Cuisine cuisine = Cuisine.Find(cuisine_id);
            Recipe recipe = Recipe.Find(recipe_id);
            cuisine.AddRecipe(recipe);
            return RedirectToAction("Show",  new { id = cuisine_id });
        }

        // [HttpGet("/cuisines")]
        // public ActionResult Index()
        // {
        //     List<Cuisine> allCuisines = Cuisine.GetAll();
        //     return View(allCuisines);
        // }
        //
        //
    }
}
