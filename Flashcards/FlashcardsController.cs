using flashcards.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class FlashcardsController
    {
        public static readonly string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=flashcardDb; Integrated Security=true;";

        internal static void CreateFlashcard(int stackId, string name)
        {
            Console.WriteLine($"\n\nCreating a flashcard for stack {stackId} - {name}\n\n");

            bool createFlashcard = true;

            while (createFlashcard)
            {
                Flashcard flashcard = new();

                Console.WriteLine("\n\nPlesae enter a Question:\n");
                flashcard.Question = Console.ReadLine();

                Console.WriteLine("\n\nPlease enter the Answer:\n");
                flashcard.Answer= Console.ReadLine();

                SqlConnection conn = new(connectionString);

                using(conn)
                {
                    conn.Open();
                    var tableCmd = conn.CreateCommand();
                    tableCmd.CommandText =
                        $@"INSERT INTO flashcard (question, answer, stackId) VALUES 
                        ('{flashcard.Question}', '{flashcard.Answer}', '{stackId}')";
                    tableCmd.ExecuteNonQuery();
                    conn.Close();

                    Console.WriteLine(
                        $"\n\nWould you like to create another flashcard for stack {stackId} - {name}?  Y/N \n");

                    string anotherFlashcard = Console.ReadLine();
                    anotherFlashcard = anotherFlashcard.ToUpper();

                    while (anotherFlashcard != "Y" && anotherFlashcard != "N")
                    {
                        Console.WriteLine(
                            $"\n\nPlease choose Y/N\n?");
                        anotherFlashcard = Console.ReadLine();
                        anotherFlashcard = anotherFlashcard.ToUpper();

                        if (anotherFlashcard == "Y" || anotherFlashcard == "N")
                            return;
                    }

                    if (anotherFlashcard == "N")
                        createFlashcard = false;
                }
            }
        }
        internal static void DeleteFlashcard(List<FlashcardsWithStack> list)
        {
            int flashcardIdOnView = UserCommands.GetIntegerInput("\nWhich flashcard would you like to update?");
            int flashcardId = list.Select(x => x.Id).ElementAt(flashcardIdOnView - 1);

            SqlConnection conn = new(connectionString);

            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $"DELETE FROM flashcard WHERE Id = {flashcardId}";
                tableCmd.ExecuteNonQuery();
                conn.Close();
            }

            Console.WriteLine("\n\nYour flashcard was successfully deleted.\n\n");
        }
        internal static void UpdateFlashcard(List<FlashcardsWithStack> list)
        {
            int flashcardIdOnView = UserCommands.GetIntegerInput("\nWhich flashcard would you like to update?");
            int flashcardId = list.Select(x => x.Id).ElementAt(flashcardIdOnView - 1);
            string updateCommand = "";
            string newQuestion = "";
            string newAnswer = "";

            string questionOption = UserCommands.GetBinaryInput("Would you like do update the question? (Y/N)");

            if (questionOption == "Y")
                newQuestion = UserCommands.GetStringInput("Please type new question:");

            string answerOption = UserCommands.GetBinaryInput("Would you like do update the answer? (Y/N)");
            if (answerOption == "Y")
                newAnswer = UserCommands.GetStringInput("Please type new answer:");

            if (newQuestion == "")
                updateCommand = @$"UPDATE flashcard SET Answer = '{newAnswer}' WHERE Id = {flashcardId}";
            else if (newAnswer == "")
                updateCommand = @$"UPDATE flashcard SET Question = '{newQuestion}' WHERE Id = {flashcardId}";
            else
                updateCommand = @$"UPDATE flashcard SET Question = '{newQuestion}', Answer = '{newAnswer}' WHERE Id = {flashcardId}";

            SqlConnection conn = new(connectionString);

            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();

                tableCmd.CommandText = updateCommand;
                Console.WriteLine(updateCommand);

                tableCmd.ExecuteNonQuery();
                conn.Close();
            }

            Console.WriteLine("\n\nYour flashcard was successfully updated.\n\n");
        }
    }
}
