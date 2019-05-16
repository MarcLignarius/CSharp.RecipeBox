using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace RecipeBox.Models
{
    public class Recipe
    {
      public string Name {get; set;}
      public string Ingredients {get; set;}
      public string Instructions {get; set;}
      public int Id {get; set;}

        public Recipe (string name, string ingredients, string instructions, int id = 0)
        {
            Name = name;
            Ingredients = ingredients;
            Instructions = instructions;
            Id = id;
        }

        public static List<Recipe> GetAll()
        {
            List<Recipe> allRecipes = new List<Recipe> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM recipes;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
                int recipeId = rdr.GetInt32(0);
                string recipeName = rdr.GetString(1);
                string recipeIngredients = rdr.GetString(2);
                string recipeInstructions = rdr.GetString(3);
                Recipe newRecipe = new Recipe(recipeName, recipeIngredients, recipeInstructions, recipeId);
                allRecipes.Add(newRecipe);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allRecipes;
        }

        public static void ClearAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM recipes;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static Recipe Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM recipes WHERE id = (@search_id);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@search_id";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int recipeId = 0;
            string recipeName = "";
            string recipeIngredients = "";
            string recipeInstructions = "";
            while(rdr.Read())
            {
                recipeId = rdr.GetInt32(0);
                recipeName = rdr.GetString(1);
                recipeIngredients = rdr.GetString(2);
                recipeInstructions = rdr.GetString(3);
            }
            Recipe newRecipe = new Recipe(recipeName, recipeIngredients, recipeInstructions, recipeId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newRecipe;
        }

        public override bool Equals(System.Object otherRecipe)
        {
            if (!(otherRecipe is Recipe))
            {
                return false;
            }
            else
            {
                Recipe newRecipe = (Recipe) otherRecipe;
                bool idEquality = this.Id == newRecipe.Id;
                bool nameEquality = this.Name == newRecipe.Name;
                bool ingredientsEquality = this.Ingredients == newRecipe.Ingredients;
                bool instructionsEquality = this.Instructions == newRecipe.Instructions;
                return (idEquality && nameEquality &&  ingredientsEquality && instructionsEquality);
            }
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO recipes (name, ingredients, instructions) VALUES (@name, @ingredients, @instructions);";
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this.Name;
            cmd.Parameters.Add(name);
            MySqlParameter ingredients = new MySqlParameter();
            ingredients.ParameterName = "@ingredients";
            ingredients.Value = this.Ingredients;
            cmd.Parameters.Add(ingredients);
            MySqlParameter instructions = new MySqlParameter();
            instructions.ParameterName = "@instructions";
            instructions.Value = this.Instructions;
            cmd.Parameters.Add(instructions);
            cmd.ExecuteNonQuery();
            Id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Edit(string newName, string newIngredients, string newInstructions)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"UPDATE recipes SET name = @new_name, ingredients = @new_ingredients, instructions = @new_instructions WHERE id = @search_id;";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@search_id";
            searchId.Value = Id;
            cmd.Parameters.Add(searchId);
            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@new_name";
            name.Value = newName;
            cmd.Parameters.Add(name);
            MySqlParameter ingredients = new MySqlParameter();
            ingredients.ParameterName = "@new_ingredients";
            ingredients.Value = newIngredients;
            cmd.Parameters.Add(ingredients);
            MySqlParameter instructions = new MySqlParameter();
            instructions.ParameterName = "@new_instructions";
            instructions.Value = newInstructions;
            cmd.Parameters.Add(instructions);
            cmd.ExecuteNonQuery();
            Name = newName;
            Ingredients = newIngredients;
            Instructions = newInstructions;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public void Delete()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM recipes WHERE id = @recipe_id; DELETE FROM cuisines_recipes WHERE recipe_id = @recipe_id;";
            MySqlParameter recipeIdParameter = new MySqlParameter();
            recipeIdParameter.ParameterName = "@recipe_id";
            recipeIdParameter.Value = this.Id;
            cmd.Parameters.Add(recipeIdParameter);
            cmd.ExecuteNonQuery();
            if (conn != null)
            {
              conn.Close();
            }
        }

        public List<Cuisine> GetCuisine()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT cuisines.* FROM recipes
                JOIN cuisines_recipes ON (recipes.id = cuisines_recipes.recipe_id)
                JOIN cuisines ON (cuisines_recipes.cuisine_id = cuisines.id)
                WHERE recipes.id = @recipe_id;";
            MySqlParameter recipeIdParameter = new MySqlParameter();
            recipeIdParameter.ParameterName = "@recipe_id";
            recipeIdParameter.Value = Id;
            cmd.Parameters.Add(recipeIdParameter);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Cuisine> cuisines = new List<Cuisine> {};
            while(rdr.Read())
            {
                int thisCuisineId = rdr.GetInt32(0);
                string cuisineRegion = rdr.GetString(1);
                Cuisine foundCuisine = new Cuisine(cuisineRegion, thisCuisineId);
                cuisines.Add(foundCuisine);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return cuisines;
        }


        public void AddCuisine(Cuisine newCuisine)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO cuisines_recipes (cuisine_id, recipe_id) VALUES (@cuisine_id, @recipe_id);";
            MySqlParameter cuisine_id = new MySqlParameter();
            cuisine_id.ParameterName = "@cuisine_id";
            cuisine_id.Value = newCuisine.Id;
            cmd.Parameters.Add(cuisine_id);
            MySqlParameter recipe_id = new MySqlParameter();
            recipe_id.ParameterName = "@recipe_id";
            recipe_id.Value = Id;
            cmd.Parameters.Add(recipe_id);
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }
    }
}
