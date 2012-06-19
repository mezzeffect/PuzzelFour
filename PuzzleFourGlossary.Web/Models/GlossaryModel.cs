using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PuzzleFourGlossary.Dto;

namespace PuzzleFourGlossary.Web.Models
{
    public class GlossaryModel
    {
        public List<Glossary> Glossaries { get; set; }

        public GlossaryView GlossaryEditModel { get; set; }
    }
}