using ConsoleTableExt;
using flashcards.Model;
using flashcards.Model.DTO;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class TableVisualizationEngine
    {
        public static void ShowTable<T>(List<T> tableData, [AllowNull] string tableName) where T : class
        {
            if (tableName == null)
                tableName = "";

            Console.WriteLine("\n\n");

            ConsoleTableBuilder
                .From(tableData)
                .WithTitle(tableName)
                .ExportAndWriteLine();
            Console.WriteLine("\n\n");
        }

        public static void PrepareFlashcardsList(int id, List<FlashcardsWithStack> list)
        {
            string stackName = list.FirstOrDefault()?.StackName;
            string tableName = $"{id} - {stackName}";

            List<FlashcardsWithStackToView> stackToView = new List<FlashcardsWithStackToView>();

            int cardIndex = 1;
            list.ForEach(x =>
            {
                stackToView.Add(new FlashcardsWithStackToView
                {
                    Id = cardIndex,
                    Question = x.Question,
                    Answer = x.Answer,
                });

                cardIndex++;
            });

            ShowTable(stackToView, tableName);
        }
    }
}
