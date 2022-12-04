using flashcards.Model;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
                    $@"INSERT INTO studysession (StackId, NumTotal, NumCorrect, DateOfStudy, StackName)
                        VALUES ('{studySession.StackId}', '{studySession.NumTotal}', 
                        '{studySession.NumCorrect}', '{studySession.DateOfStudy}', '{studySession.StackName}')";
                tblCmd.ExecuteNonQuery();
                conn.Close();
            }
        }
        internal static void GetStudySessions()
        {
            List<StudySession> sessions = new List<StudySession>();

            SqlConnection conn = new(connectionString);
            using(conn)
            {
                conn.Open();
                var tableCmd = conn.CreateCommand();
                tableCmd.CommandText =
                    $@"SELECT * FROM studysession";
                tableCmd.ExecuteNonQuery();

                SqlDataReader reader = tableCmd.ExecuteReader();

                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        var stackNameCheck = reader.SafeGetString(5);
                        
                        if (reader["StackName"].ToString() == "")
                        {
                            sessions.Add(
                            new StudySession
                            {
                                Id = reader.GetInt32(0),
                                DateOfStudy = reader.GetDateTime(1),
                                NumCorrect = reader.GetInt32(2),
                                NumTotal = reader.GetInt32(3),
                                StackId = reader.GetInt32(4),                               
                            });
                        }
                        else
                        {
                            sessions.Add(
                            new StudySession
                            {
                                Id = reader.GetInt32(0),
                                DateOfStudy = reader.GetDateTime(1),
                                NumCorrect = reader.GetInt32(2),
                                NumTotal = reader.GetInt32(3),
                                StackId = reader.GetInt32(4),
                                //StackName = reader.GetString(5),
                                StackName = reader.SafeGetString(5),
                            });
                        }
                        //sessions.Add(
                        //    new StudySession
                        //    {
                        //        Id = reader.GetInt32(0),
                        //        DateOfStudy = reader.GetDateTime(1),
                        //        NumCorrect= reader.GetInt32(2),
                        //        NumTotal= reader.GetInt32(3),
                        //        StackId=reader.GetInt32(4),
                        //        StackName=reader.GetString(5),
                        //    });
                    }
                }
                else
                {
                    Console.WriteLine("\n\nNo rows found.\n");
                }
                conn.Close();
            }

            TableVisualizationEngine.ShowTable(sessions, "Study Sessions");
        }

        public static string SafeGetString (this SqlDataReader reader, int colIndex)
        {
            if(!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return string.Empty;
        }
    }
}
