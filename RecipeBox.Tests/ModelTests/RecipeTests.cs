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
      public void Find_ReturnsCorrectPatientFromDatabase_Patient()
      {
          //Arrange

          Recipe testRecipe = new Recipe("Manburger", "man-meat, man-buns", "Turn stove top to medium heat, grill meat until cooked through, assemble burger to your preference");
          testRecipe.Save();

          //Act
          Recipe foundRecipe = Recipe.Find(testRecipe.Id);

          //Assert
          Assert.AreEqual(testRecipe, foundRecipe);
      }

//
//         [TestMethod]
//         public void Edit_UpdatesPatientInDatabase_String()
//         {
//             //Arrange
//             DateTime birthDate =  new DateTime(1999, 12, 24);
//             bool completed = false;
//             int id = 0;
//             Patient testPatient = new Patient("Walk the Dog", birthDate);
//             testPatient.Save();
//             string newName = "Mow the lawn";
//             DateTime newDueDate = new DateTime(2019, 12, 24);
//             bool newCompleted = true;
//             int newId = 1;
//
//             //Act
//             testPatient.Edit(newName, newDueDate, newCompleted);
//             string result = Patient.Find(testPatient.GetId()).GetName();
//
//             //Assert
//             Assert.AreEqual(newName, result);
//         }
//
//         [TestMethod]
//         public void GetCategories_ReturnsAllPatientCategories_CategoryList()
//         {
//             //Arrange
//             DateTime birthDate =  new DateTime(1999, 12, 24);
//             Patient testPatient = new Patient("Mow the lawn", birthDate);
//             testPatient.Save();
//             Category testCategory1 = new Category("Home stuff");
//             testCategory1.Save();
//             Category testCategory2 = new Category("Work stuff");
//             testCategory2.Save();
//
//             //Act
//             testPatient.AddCategory(testCategory1);
//             List<Category> result = testPatient.GetCategories();
//             List<Category> testList = new List<Category> {testCategory1};
//
//             //Assert
//             CollectionAssert.AreEqual(testList, result);
//         }
//
//         [TestMethod]
//         public void AddCategory_AddsCategoryToPatient_CategoryList()
//         {
//             //Arrange
//             DateTime birthDate =  new DateTime(1999, 12, 24);
//             Patient testPatient = new Patient("Mow the lawn", birthDate);
//             testPatient.Save();
//             Category testCategory = new Category("Home stuff");
//             testCategory.Save();
//
//             //Act
//             testPatient.AddCategory(testCategory);
//
//             List<Category> result = testPatient.GetCategories();
//             List<Category> testList = new List<Category>{testCategory};
//
//             //Assert
//             CollectionAssert.AreEqual(testList, result);
//         }
//
//         [TestMethod]
//         public void Delete_DeletesPatientAssociationsFromDatabase_PatientList()
//         {
//             //Arrange
//             DateTime birthDate =  new DateTime(1999, 12, 24);
//             Category testCategory = new Category("Home stuff");
//             testCategory.Save();
//             string testName = "Mow the lawn";
//             Patient testPatient = new Patient(testName, birthDate);
//             testPatient.Save();
//
//             //Act
//             testPatient.AddCategory(testCategory);
//             testPatient.Delete();
//             List<Patient> resultCategoryPatients = testCategory.GetPatients();
//             List<Patient> testCategoryPatients = new List<Patient> {};
//
//             //Assert
//             CollectionAssert.AreEqual(testCategoryPatients, resultCategoryPatients);
//         }
    }
}
