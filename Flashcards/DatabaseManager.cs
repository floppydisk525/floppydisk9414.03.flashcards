using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class DatabaseManager
    {
        internal static void CheckDatabase()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB;Integrated Security=true;";

            try
            {

                SqlConnection conn = new SqlConnection(connectionString);
                using (conn)
                {
                    conn.Open();
                    var tableCmd = conn.CreateCommand();
                    tableCmd.CommandText =
                        $@"IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'flashcardDb')
                           BEGIN
                             CREATE DATABASE flashcardDb;
                           END;
                         ";
                    tableCmd.ExecuteNonQuery();
                    conn.Close();
                }

                CreateTables();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        internal static void CreateTables()
        {
            string connectionString = "Server=(localdb)\\MSSQLLocalDB; Initial Catalog=flashcardDb; Integrated Security=true;";

            SqlConnection conn = new SqlConnection(connectionString);
            using (conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();

                tableCmd.CommandText =
                    $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'stack')
                        CREATE TABLE stack (
	                      Id int IDENTITY(1,1) NOT NULL,
	                      Name varchar(100) NOT NULL UNIQUE,
	                      PRIMARY KEY (Id)
                         );
                      ";
                tableCmd.ExecuteNonQuery();

                tableCmd.CommandText =
                    $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'flashcard')
                        CREATE TABLE flashcard (
                          Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                          Question varchar(30) NOT NULL,
                          Answer varchar(30) NOT NULL,
                          StackId int NOT NULL 
                            FOREIGN KEY 
                            REFERENCES stack(Id) 
                            ON DELETE CASCADE 
                            ON UPDATE CASCADE
                         );
                      ";
                tableCmd.ExecuteNonQuery();

                tableCmd.CommandText =
                    $@" IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'studySession')
                        CREATE TABLE studySession (
                          Id int IDENTITY(1,1) NOT NULL PRIMARY KEY,
                          DateOfStudy DateTime NOT NULL,
                          NumCorrect int,
                          NumTotal int,
                          StackId int NOT NULL 
                            FOREIGN KEY 
                            REFERENCES stack(Id) 
                            ON DELETE CASCADE 
                            ON UPDATE CASCADE,
                          StackName nchar(10) NULL
                         );
                      ";
                tableCmd.ExecuteNonQuery();

                conn.Close();
            }
        }
    }
}