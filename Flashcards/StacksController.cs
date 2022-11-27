using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using flashcards.Model;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    public class StacksController
    {
        public static readonly string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=flashcardDb; Integrated Security=true;";
        internal static List<Stack> GetStacks()
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText = "SELECT * FROM stack " +
                "ORDER BY Id";

            List<Stack> stacks = new();
            SqlDataReader reader = tableCmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    stacks.Add(
                        new Stack
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        });
                }
            }
            else
            {
                Console.WriteLine("\n\nNo Rows Found.\n\n");
            }
            reader.Close();
            TableVisualizationEngine.ShowTable(stacks, "Stacks");   //should consider doing this where called, if that's what you want to do

            return stacks;
        }
        
        internal static void CreateStack()
        {

        }
    }
}
