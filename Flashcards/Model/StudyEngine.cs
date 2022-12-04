using System;
using System.Collections.Generic;
using System.Linq;
using flashcards.Model;
using System.Text;
using System.Threading.Tasks;

namespace flashcards.Model
{
    internal class StudyEngine
    {
        internal static StudySession CreateStudySession()
        {
            List<Stack> stacks = StacksController.GetStacks();
            var id = UserCommands.GetIntegerInput("Enter the ID of the stack would you like to study?");

            while (stacks.All(x => x.Id != id))
            {
                Console.WriteLine("\nThere's no stack wit this id.");
                id = UserCommands.GetIntegerInput("Which stack would you like to study?");
            }

            List<FlashcardsWithStack> stack = StacksController.GetStackWithCard(id);

            var inputAnswer = "";
            var score = 0;
            var questionId = 0;

            List<int> incorrectAnswers = new List<int>();

            stack.ForEach(x =>
            {
                questionId++;
                Console.WriteLine(x.Question);
                inputAnswer = UserCommands.GetStringInput("Type your answer:");

                if (inputAnswer == x.Answer)
                    score++;
                else
                    incorrectAnswers.Add(questionId);
            });

            Console.WriteLine($"Score: {score}");
            incorrectAnswers.ForEach(x => Console.Write($"{x}, "));

            StudySession studySession = new StudySession
            {
                StackName = stack.FirstOrDefault().StackName,
                NumTotal = stack.Count,                
                NumCorrect = score,
            };

            Console.WriteLine($"\nYour result: {studySession.NumCorrect}%");
            Console.WriteLine($"\nIncorrect answers:");
            incorrectAnswers.ForEach(x => Console.Write($"{x}, "));

            return studySession;
        }
    }
}
