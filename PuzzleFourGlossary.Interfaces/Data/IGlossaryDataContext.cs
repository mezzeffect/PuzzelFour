using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Objects;
using System.Linq;
using System.Text;
using PuzzleFourGlossary.Dto;

namespace PuzzleFourGlossary.Interfaces.Data
{
    public interface IGlossaryDataContext
    {
        IDbSet<Glossary> Glossaries { get; set; }
        IDbSet<TEntity> Set<TEntity>() where TEntity : class;
        
        /// <summary>
        /// set the current entity state to modified
        /// </summary>
        /// <param name="entity">entity database object</param>
        void SetModified(object entity);
        int SaveChanges();
    }
}
