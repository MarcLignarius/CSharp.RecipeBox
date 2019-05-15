using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace RecipeBox.Models
{
    public class Cuisine
    {
        public string Region {get; set;}
        public int Id {get; set;}

        public Cuisine(string region, int id = 0)
        {
            Region = region;
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
                return (idEquality && regionEquality);
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
            Cuisine newCuisine = new Cuisine(cuisineRegion, cuisineId);
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
          cmd.CommandText = @"INSERT INTO cuisines (region) VALUES (@region);";
          MySqlParameter region = new MySqlParameter();
          region.ParameterName = "@region";
          region.Value = this.Region;
          cmd.Parameters.Add(region);
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
            while(rdr.Read())
            {
              cuisineId = rdr.GetInt32(0);
              cuisineName = rdr.GetString(1);
            }
            Cuisine newCuisine = new Cuisine(cuisineRegion, cuisineId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newCuisine;
        }
        // public List<Client> GetClients()
        // {
        //   List<Client> allCuisineClients = new List<Client> {};
        //   MySqlConnection conn = DB.Connection();
        //   conn.Open();
        //   var cmd = conn.CreateCommand() as MySqlCommand;
        //   cmd.CommandText = @"SELECT * FROM clients WHERE cuisine_id = @cuisine_id;";
        //   MySqlParameter cuisineId = new MySqlParameter();
        //   cuisineId.ParameterName = "@cuisine_id";
        //   cuisineId.Value = this._id;
        //   cmd.Parameters.Add(cuisineId);
        //   var rdr = cmd.ExecuteReader() as MySqlDataReader;
        //   while(rdr.Read())
        //   {
        //     int clientId = rdr.GetInt32(0);
        //     string clientFirstName = rdr.GetString(1);
        //     string clientLastName = rdr.GetString(2);
        //     string clientPhoneNumber = rdr.GetString(3);
        //     string clientEmailAddress = rdr.GetString(4);
        //     int clientCuisineId = rdr.GetInt32(5);
        //     Client newClient = new Client(clientFirstName, clientLastName, clientPhoneNumber, clientEmailAddress, clientCuisineId, clientId);
        //     allCuisineClients.Add(newClient);
        //   }
        //   conn.Close();
        //   if (conn != null)
        //   {
        //       conn.Dispose();
        //   }
        //   return allCuisineClients;
        // }
        //

        //
    }
}
