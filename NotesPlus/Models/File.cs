using System;
using System.Collections.Generic;
using System.Text;

namespace NotesPlus.Models
{
    public class File
    {
        public Guid Id { get; set;}
        public string Category { get; set; }
        public string Content { get; set; }
    }
}
