using System;
using System.Threading;
using flashcards;
using Microsoft.Data.SqlClient;

namespace Flashcards
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("hello world from floppy");
            
            DatabaseManager.CheckDatabase();
/*            UserCommands.MainMenu();  
*/
        }
    }
}
