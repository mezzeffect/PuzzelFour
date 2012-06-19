using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using PuzzleFourGlossary.Dto;

namespace PuzzleFourGlossary.Repository.Tests.Helper
{
    public class FakeGlossarySet :FakeDbSet<Glossary>
    {
        public override Glossary Find(params object[] keyValues)
        {
            return this.SingleOrDefault(g => g.GlossaryId == (int) keyValues.Single());
        }
        
    }
}
