using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ClassLibrary
{
    public class DataMngr // Ensure this matches the class name you're using
    {
        private string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\States.mdf;Integrated Security=True;";

        // Get all states from the database
        public List<State> GetAllStates()
        {
            List<State> states = new List<State>();// Create a list to hold the states

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection and execute the query
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM StateData";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Read the results and create a State object for each row
                        while (reader.Read())
                        {
                            // Create a new State object and populate it with the data from the database
                            State state = new State
                            {
                                // Ensure the column names match the database schema
                                Name = reader["Name"].ToString(),
                                Population = Convert.ToInt32(reader["Population"]),
                                FlagDescription = reader["FlagDescription"].ToString(),
                                Flower = reader["Flower"].ToString(),
                                Bird = reader["Bird"].ToString(),
                                Colors = reader["Colors"].ToString(),
                                LargestCities = reader["LargestCities"].ToString(),
                                Capital = reader["Capital"].ToString(),
                                MedianIncome = Convert.ToDecimal(reader["MedianIncome"]),
                                CompJobsPercent = Convert.ToDecimal(reader["CompJobsPercent"])
                            };
                            states.Add(state);// Add the State object to the list
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }
            }

            return states;// Return the list of states
        }

        // Get a single state by name for user selection
        public State GetStateByName(string stateName)
        {
            State state = null;

            // Open a connection to the database and execute the query
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM StateData WHERE Name = @StateName";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StateName", stateName);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                state = new State
                                {
                                    Name = reader["Name"].ToString(),
                                    Population = Convert.ToInt32(reader["Population"]),
                                    FlagDescription = reader["FlagDescription"].ToString(),
                                    Flower = reader["Flower"].ToString(),
                                    Bird = reader["Bird"].ToString(),
                                    Colors = reader["Colors"].ToString(),
                                    LargestCities = reader["LargestCities"].ToString(),
                                    Capital = reader["Capital"].ToString(),
                                    MedianIncome = Convert.ToDecimal(reader["MedianIncome"]),
                                    CompJobsPercent = Convert.ToDecimal(reader["CompJobsPercent"])
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Database error: {ex.Message}");
                }
            }

            return state;
        }
    }
}
