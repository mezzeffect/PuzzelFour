using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using System.Text;
using Moq;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces.Data;

namespace PuzzleFourGlossary.Repository.Tests.Helper
{
    public class FakeGlossaryDataContext:IGlossaryDataContext
    {
        public IDbSet<Glossary> Glossaries { get; set; }
        public bool Saved { get; private set; }
        public bool Updated { get; private set; }
        public IDbSet<T> Set<T>() where T : class
        {
            foreach (PropertyInfo property in typeof (FakeGlossaryDataContext).GetProperties())
            {
                if (property.PropertyType == typeof (IDbSet<T>))
                    return property.GetValue(this, null) as IDbSet<T>;
            }
            throw new Exception("Type collection not found");
        }
        public void SaveChanges()
        {
            Saved = true;
        }
        public FakeGlossaryDataContext()
        {
            // Set up your collections
            Glossaries = new FakeGlossarySet()
                             {
                                 new Glossary() {GlossaryId = 1,Term = "Brent"},
                                 new Glossary() {GlossaryId = 2,Term = "Darth"},
                                 new Glossary() {GlossaryId = 3,Term = "Follower"},
                                 new Glossary() {GlossaryId = 3,Term = "Maker"},
                                 new Glossary() {GlossaryId = 3,Term = "Green"},
                                 new Glossary() {GlossaryId = 3,Term = "Red"},
                                 new Glossary() {GlossaryId = 3,Term = "Yellow"},
                                 new Glossary() {GlossaryId = 3,Term = "Fast"},
                                 new Glossary() {GlossaryId = 3,Term = "Zoo"},
                                 new Glossary() {GlossaryId = 3,Term = "Adam"}
                             };
        }

        int IGlossaryDataContext.SaveChanges()
        {
            Saved = true;
            return 1;
        }

        public void SetModified(object entity)
        {
            throw new NotImplementedException("Not To be tested here");
        }
    }
}
