using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces.Data;

namespace PuzzleFourGlossary.Data
{
    public class GlossaryDataContext:DbContext, IGlossaryDataContext
    {
        public IDbSet<Glossary> Glossaries { get; set; }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }

        public void SetModified(object entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
    }
}
