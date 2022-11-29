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
            Stack stack = new();
            Console.WriteLine("\n\nPlease Enter a Stack Name\n\n");
            stack.Name = Console.ReadLine();

            SqlConnection conn = new(connectionString);
            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $@"INSERT INTO stack (Name) VALUES ('{stack.Name}')";
                tableCmd.ExecuteNonQuery();
                conn.Close();
            }
            var stackId = GetStackId();

            Console.WriteLine("\n\nYour flashcards stack was successfully created.\n\n");
            FlashcardsController.CreateFlashcard(stackId, stack.Name);
        }

        private static int GetStackId()
        {
            SqlConnection conn = new(connectionString);
            conn.Open();
            SqlCommand comm = new("SELECT IDENT_CURRENT('stack')", conn);
            int id = Convert.ToInt32(comm.ExecuteScalar());
            conn.Close();
            return id;
        }

        public static void ManageStack()
        {
            GetStacks();

            int stackId = UserCommands.GetIntegerInput("\nType the id of the stack you would like to manage.\n");

            List<FlashcardsWithStack> stack = GetStackWithCard(stackId);

            UserCommands.ManageStackMenu(stackId, stack);
        }

        internal static List<FlashcardsWithStack> GetStackWithCard(int id)       //getting all the flashcards for stacks of id
        {
            using var connection = new SqlConnection(connectionString);
            Stack stack = new();
            
            connection.Open();
            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
                $@"SELECT f.Id, s.Name as stackname, f.Question, f.Answer
                FROM flashcard f
                LEFT JOIN stack s
                ON s.Id = f.StackId
                WHERE s.Id={id}";
            //connection.Close();
            List<FlashcardsWithStack> cards = new();

            SqlDataReader reader = tableCmd.ExecuteReader();
            //now connection.close(); ???????
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cards.Add(
                        new FlashcardsWithStack
                        {
                            Id = reader.GetInt32(0),
                            StackName = reader.GetString(1),
                            Question = reader.GetString(2),
                            Answer = reader.GetString(3)
                        });
                }
            }
            else
            {
                Console.WriteLine("\n\nNo rows found.\n\n");
            }

            reader.Close();

            Console.WriteLine("\n\n");

            TableVisualizationEngine.PrepareFlashcardsList(id, cards);

            return cards;
        }

    }
}
