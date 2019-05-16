using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using RecipeBox.Models;

namespace RecipeBox.Tests
{
  [TestClass]
  public class CuisineTests : IDisposable
  {

    public void Dispose()
    {
      //Repice.ClearAll();
      Cuisine.ClearAll();
    }

    public CuisineTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=recipe_box_test;";
    }

    [TestMethod]
    public void Save_SavesCuisineToDatabase_CuisineList()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Japanese", "It's good");
      testCuisine.Save();

      //Act
      List<Cuisine> result = Cuisine.GetAll();
      List<Cuisine> testList = new List<Cuisine>{testCuisine};

      //Assert
      CollectionAssert.AreEqual(testList, result);
    }

    [TestMethod]
    public void Save_DatabaseAssignsIdToCuisine_Id()
    {
      //Arrange
      Cuisine testCuisine = new Cuisine("Japanese", "It's good");
      testCuisine.Save();

      //Act
      Cuisine savedCuisine = Cuisine.GetAll()[0];

      int result = savedCuisine.Id;
      int testId = testCuisine.Id;
      Console.WriteLine(result);
      Console.WriteLine(testId);

      //Assert
      Assert.AreEqual(testId, result);
    }

    [TestMethod]
      public void Equals_ReturnsTrueIfNamesAreTheSame_Cuisine()
      {
          //Arrange, Act
          Cuisine firstCuisine = new Cuisine("Japanese", "It's good");
          Cuisine secondCuisine = new Cuisine("Japanese", "It's good");

          //Assert
          Assert.AreEqual(firstCuisine, secondCuisine);
      }
  }
}
