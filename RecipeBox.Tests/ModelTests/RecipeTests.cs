using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using RecipeBox.Models;

namespace RecipeBox.Tests
{
  [TestClass]
  public class RecipeTests : IDisposable
  {

    public void Dispose()
    {
      Cuisine.ClearAll();
      Recipe.ClearAll();
    }

    public RecipeTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test;";
    }

    [TestMethod]
    public void Save_SavesRecipeToDatabase_RecipeList()
    {
      //Arrange
      Recipe testRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
      testRecipe.Save();

      //Act
      List<Recipe> result = Recipe.GetAll();
      List<Recipe> testList = new List<Recipe>{testRecipe};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToRecipe_Id()
    {
      //Arrange
      Recipe testRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
      testRecipe.Save();

      //Act
      Recipe savedRecipe = Recipe.GetAll()[0];

      int result = savedRecipe.Id;
      int testId = testRecipe.Id;

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
    public void Equals_ReturnsTrueIfNamesAreTheSame_Recipe()
    {
      //Arrange, Act
      Recipe firstRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
      Recipe secondRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");

      //Assert
      Assert.AreEqual(firstRecipe, secondRecipe);
    }

    [TestMethod]
    public void GetAll_ReturnsRecipe_RecipeList()
    {
      //Arrange
      Recipe newRecipe1 = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
      newRecipe1.Save();
      Recipe newRecipe2 = new Recipe("Long-pig", "friends", "I get by with a little help from my friends");
      newRecipe2.Save();
      List<Recipe> newList = new List<Recipe> { newRecipe1, newRecipe2 };

      //Act
      List<Recipe> result = Recipe.GetAll();

      //Assert
      CollectionAssert.AreEqual(newList, result);
    }

    [TestMethod]
    public void Find_ReturnsCorrectRecipeFromDatabase_Recipe()
    {
      //Arrange

      Recipe testRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
      testRecipe.Save();

      //Act
      Recipe foundRecipe = Recipe.Find(testRecipe.Id);

      //Assert
      Assert.AreEqual(testRecipe, foundRecipe);
    }

    [TestMethod]
    public void Edit_UpdatesRecipeInDatabase_String()
    {
      //Arrange
      Recipe testRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
      testRecipe.Save();
      string newName = "Trish kabobs";
      string newIngredients = "Meat of unknown or origin, vegetables of your choice";
      string newInstructions = "Chop up meat and veggies. Skwer chunks and roast over and open flame";

      //Act
      testRecipe.Edit(newName, newIngredients, newInstructions);
      // string result = "Trish kabobs";
      string result = Recipe.Find(testRecipe.Id).Name;

      //Assert
      Assert.AreEqual(newName, result);
    }

    [TestMethod]
    public void AddCuisine_AddsCuisineToRecipe_CuisineList()
    {
      //Arrange

      Recipe testRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
      testRecipe.Save();
      Cuisine testCuisine = new Cuisine("Home stuff");
      testCuisine.Save();

      //Act
      testRecipe.AddCuisine(testCuisine);

      List<Cuisine> result = testRecipe.GetCuisine();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void GetCuisine_ReturnsAllRecipeCuisine_CuisineList()
    {
        //Arrange
        Recipe testRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
        testRecipe.Save();
        Cuisine testCuisine1 = new Cuisine("Home stuff");
        testCuisine1.Save();
        Cuisine testCuisine2 = new Cuisine("Work stuff");
        testCuisine2.Save();

        //Act
        testRecipe.AddCuisine(testCuisine1);
        List<Cuisine> result = testRecipe.GetCuisine();
        List<Cuisine> testList = new List<Cuisine> {testCuisine1};

        //Assert
        CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Delete_DeletesRecipeAssociationsFromDatabase_RecipeList()
    {
        //Arrange
        Cuisine testCuisine = new Cuisine("Home stuff");
        testCuisine.Save();
        string testName = "Mow the lawn";
        Recipe testRecipe = new Recipe(testName, "stuff", "make stuff");
        testRecipe.Save();

        //Act
        testRecipe.AddCuisine(testCuisine);
        testRecipe.Delete();
        List<Recipe> resultCuisineRecipes = testCuisine.GetRecipes();
        List<Recipe> testCuisineRecipes = new List<Recipe> {};

        //Assert
        CollectionAssert.AreEqual(testCuisineRecipes, resultCuisineRecipes);
    }
  }
}
