using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace RecipeBox.Models
{
    public class Cuisine
    {
        public string Region {get; set;}
        public string Description {get; set;}
        public int Id {get; set;}

        public Cuisine(string region, string description, int id = 0)
        {
            Region = region;
            Description = description;
            Id = id;
        }

        public override bool Equals(System.Object otherCuisine)
        {
            if (!(otherCuisine is Cuisine))
            {
                return false;
            }
            else
            {
                Cuisine newCuisine = (Cuisine) otherCuisine;
                bool idEquality = this.Id.Equals(newCuisine.Id);
                bool regionEquality = this.Region.Equals(newCuisine.Region);
                bool descriptionEquality = this.Description.Equals(newCuisine.Description);
                return (idEquality && regionEquality && descriptionEquality);
            }
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public static List<Cuisine> GetAll()
        {
          List<Cuisine> allCuisines = new List<Cuisine> {};
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"SELECT * FROM cuisines ORDER BY region;";
          var rdr = cmd.ExecuteReader() as MySqlDataReader;
          while(rdr.Read())
          {
            int cuisineId = rdr.GetInt32(0);
            string cuisineRegion = rdr.GetString(1);
            string cuisineDescription = rdr.GetString(2);
            Cuisine newCuisine = new Cuisine(cuisineRegion, cuisineDescription, cuisineId);
            allCuisines.Add(newCuisine);
          }
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
          return allCuisines;
        }

        public static void ClearAll()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"DELETE FROM cuisines;";
          cmd.ExecuteNonQuery();
          conn.Close();
          if (conn != null)
          {
            conn.Dispose();
          }
        }

        public void Save()
        {
          MySqlConnection conn = DB.Connection();
          conn.Open();
          var cmd = conn.CreateCommand() as MySqlCommand;
          cmd.CommandText = @"INSERT INTO cuisines (region, description) VALUES (@region, @description);";
          MySqlParameter region = new MySqlParameter();
          region.ParameterName = "@region";
          region.Value = this.Region;
          cmd.Parameters.Add(region);
          MySqlParameter description = new MySqlParameter();
          description.ParameterName = "@description";
          description.Value = this.Description;
          cmd.Parameters.Add(description);
          cmd.ExecuteNonQuery();
          Id = (int) cmd.LastInsertedId;
          conn.Close();
          if (conn != null)
          {
              conn.Dispose();
          }
        }

        public static Cuisine Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM cuisines WHERE id = (@search_id);";
            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@search_id";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int cuisineId = 0;
            string cuisineRegion = "";
            string cuisineDescription = "";
            while(rdr.Read())
            {
              cuisineId = rdr.GetInt32(0);
              cuisineRegion = rdr.GetString(1);
              cuisineDescription = rdr.GetString(2);
            }
            Cuisine newCuisine = new Cuisine(cuisineRegion, cuisineDescription, cuisineId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCuisine;
        }

        public List<Recipe> GetRecipes()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT recipes.* FROM cuisines
                JOIN cuisines_recipes ON (cuisines.id = cuisines_recipes.cuisine_id)
                JOIN recipes ON (cuisines_recipes.recipe_id = recipes.id)
                WHERE cuisines.id = @cuisine_id;";
            MySqlParameter cuisineIdParameter = new MySqlParameter();
            cuisineIdParameter.ParameterName = "@cuisine_id";
            cuisineIdParameter.Value = Id;
            cmd.Parameters.Add(cuisineIdParameter);
            MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
            List<Recipe> recipes = new List<Recipe>{};
            while(rdr.Read())
            {
                int recipeId = rdr.GetInt32(0);
                string recipeName = rdr.GetString(1);
                string recipeIngredients = rdr.GetString(2);
                string recipeInstructions = rdr.GetString(3);
                Recipe newRecipe = new Recipe(recipeName, recipeIngredients, recipeInstructions, recipeId);
                recipes.Add(newRecipe);
            }
            conn.Close();
            if (conn != null)
            {
              conn.Dispose();
            }
            return recipes;
        }
    }
}
