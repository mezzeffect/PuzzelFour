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
        [MaxLength(20, ErrorMessage = "Invalid term, too long")]
        public string Term { get; set; }
        
        [Required]
        [MaxLength(400, ErrorMessage = "Invalid definition, too long")]
        public string Definition { get; set; }
    }
}