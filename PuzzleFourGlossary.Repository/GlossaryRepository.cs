using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces.Data;
using PuzzleFourGlossary.Interfaces.Repository;

namespace PuzzleFourGlossary.Repository
{
    public class GlossaryRepository:IEntityRepository<Glossary>
    {
        #region P R I V A T E

        private readonly IGlossaryDataContext _glossaryDataContext; 

        #endregion

        #region I  E N T I T Y  R E P O S I T O R Y
        public GlossaryRepository(IGlossaryDataContext glossaryDataContext)
        {
            _glossaryDataContext = glossaryDataContext;
        }
        public Glossary GetById(int? id)
        {
            return _glossaryDataContext.Glossaries.Find(id);
        }

        public void Add(Glossary entity)
        {
            _glossaryDataContext.Glossaries.Add(entity);
        }

        public void Delete(Glossary entity)
        {
            _glossaryDataContext.Glossaries.Remove(entity);
        }

        public void Update(Glossary entity)
        {
            _glossaryDataContext.SetModified(entity);
        }

        public List<Glossary> All()
        {
            return _glossaryDataContext.Glossaries.OrderBy(g=>g.Term).ToList();
        }

        public void SubmitChanges()
        {
            _glossaryDataContext.SaveChanges();
        } 
        #endregion
    }
}
