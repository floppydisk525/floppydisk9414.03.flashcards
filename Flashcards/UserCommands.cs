using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    public class UserCommands
    {
        internal static void MainMenu()
        {
            bool closeApp = false;
            while(closeApp==false)
            {
                Console.WriteLine("\n\nMAIN MENU\n\n");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("Type 0 to close Aplication");
                Console.WriteLine("Type 1 to manange Flashcards");
                Console.WriteLine("Type 2 to Study");

                string commandInput = Console.ReadLine();
                while(string.IsNullOrEmpty(commandInput) || !int.TryParse(commandInput, out _))
                {
                    Console.WriteLine("\nInvalid command.  Please type a number from 0 to 2.\n");
                    commandInput= Console.ReadLine();
                }

                int command = Convert.ToInt32(commandInput);

                switch(command)
                {
                    case 0: 
                        closeApp= true;
                        break;
                    case 1:
                        StacksMenu();
                        break;
                    case 2:
                        //StudyMenu();
                        break;
                    default:
                        Console.WriteLine("\nInvalid Entry, Please type a number between 0 and 2. \n");
                        break;
                }
            }
        }
        internal static void StacksMenu()
        {
            Console.WriteLine("\n\nFLASHCARD STACKS AREA\n");
            StacksController.GetStacks();
        }

    }
}
