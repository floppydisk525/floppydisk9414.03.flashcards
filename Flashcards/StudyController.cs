using flashcards.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards
{
    internal class StudyController
    {
        public static readonly string connectionString =
            "Server=(localdb)\\MSSQLLocalDB;Initial Catalog=flashcardDb; Integrated Security=true;";

        internal static void NewStudySession()
        {
            var studySession = StudyEngine.CreateStudySession();
            studySession.DateOfStudy = DateTime.Now;

            SqlConnection conn = new(connectionString);
            using(conn)
            {
                conn.Open();
                var tblCmd = conn.CreateCommand();
                tblCmd.CommandText =
                    $@"INSERT INTO studysession (StackId, NumTotal, NumCorrect, DateOfStudy)
                        VALUES ('{studySession.StackId}', '{studySession.NumTotal}', 
                        '{studySession.NumCorrect}', '{studySession.DateOfStudy}')";
                tblCmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        internal static void GetStudySessions()
        {
            Console.WriteLine("Not setup yet...  todo...");
        }
    }
}
