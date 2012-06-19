using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Web.Models;

namespace PuzzleFourGlossary.Web.Helpers
{
    public static class Extensions
    {
        public static Glossary ToGlossaryDto(this GlossaryView glossary)
        {
            try
            {
                return new Glossary() { GlossaryId = glossary.Id, Term = glossary.Term, Definition = glossary.Definition };
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}