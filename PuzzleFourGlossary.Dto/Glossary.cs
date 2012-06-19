using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PuzzleFourGlossary.Dto
{
    /// <summary>
    /// The database object for the glossaries database 
    /// </summary>
    public class Glossary
    {
        public int GlossaryId { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
    }
}
