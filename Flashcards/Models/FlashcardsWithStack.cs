﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace flashcards.Models
{
    public class FlashcardsWithStack
    {
        public int Id { get; set; }
        public string StackName { get; set;  }
        public string Question { get; set;  }
        public string Answer { get; set;  }
    }
}
