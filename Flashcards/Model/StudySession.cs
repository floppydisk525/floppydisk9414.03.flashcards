#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards.Model
{
    internal class StudySession
    {
        public int Id { get; set; }
        public DateTime DateOfStudy { get; set; }
        public int NumCorrect { get; set; }
        public int NumTotal { get; set; }
        public int StackId { get; set; }
        public string? StackName { get; set; }
    }
}
