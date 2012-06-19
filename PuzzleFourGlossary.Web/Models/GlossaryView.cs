using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PuzzleFourGlossary.Web.Models
{
    public class GlossaryView
    {
        [Required]
        public int Id { get; set; }
        
        [Required]
        public string Term { get; set; }
        
        [Required]
        public string Definition { get; set; }
    }
}