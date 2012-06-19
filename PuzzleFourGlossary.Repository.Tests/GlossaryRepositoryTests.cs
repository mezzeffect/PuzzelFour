using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces.Data;
using PuzzleFourGlossary.Interfaces.Repository;
using PuzzleFourGlossary.Repository.Tests.Helper;

namespace PuzzleFourGlossary.Repository.Tests
{
    [TestFixture]
    public class GlossaryRepositoryTests
    {
        private Mock<IGlossaryDataContext> _mockGlossaryDataContext;

        private IEntityRepository<Glossary> GetMockDbContextRepository()
        {
            _mockGlossaryDataContext = new Mock<IGlossaryDataContext>();
            return new GlossaryRepository(_mockGlossaryDataContext.Object);
        }

        private IEntityRepository<Glossary> GetFakeDbContextRepository()
        {
            return new GlossaryRepository(new FakeGlossaryDataContext());
        }

        [Test]
        public void GlossaryRepositoryAddShouldReturnAGlossary()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();
            var glossary = new Glossary();

            //act
            repo.Add(glossary);
            var result = repo.GetById(glossary.GlossaryId);

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public void GlossaryRepositoryAllShouldReturnListOrderedByTermAlphabitically()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();

            //act
            var result = repo.All();

            //assert
            Assert.True(result.Last().Term == "Zoo");
        }

        [Test]
        public void GlossaryRepositoryAddShouldAddTheCorrectGlossary()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();
            var glossary = new Glossary();

            //act
            repo.Add(glossary);
            var result = repo.GetById(glossary.GlossaryId);

            //assert
            Assert.AreEqual(result, glossary);
        }

        [Test]
        public void GlossaryRepositoryGetByIdShouldReturnGlossary()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();

            //act
            var result = repo.GetById(1);

            //assert
            Assert.NotNull(result);
        }

        [Test]
        public void GlossaryRepositoryGetByIdShouldReturnTheCorrectGlossary()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();            

            //act
            var result = repo.GetById(1);

            //assert
            Assert.NotNull(result);
            Assert.AreEqual("Brent",result.Term);
        }

        [Test]
        public void GlossaryRepositoryDeleteShouldWorkCorrectly()
        {
            //arrange 
            var repo = GetFakeDbContextRepository();

            //act
            var result = repo.GetById(1);
            repo.Delete(result);
            result = repo.GetById(1);
            
            //assert
            Assert.Null(result);
        }

        [Test]
        public void GlossaryRepositoryUpdateShouldCallEntry()
        {
            //arrange 
            var repo = GetMockDbContextRepository();

            //act
            repo.Update(It.IsAny<Glossary>());

            //assert
            _mockGlossaryDataContext.Verify(g=>g.SetModified(It.IsAny<object>()),Times.Once());
        }

        [Test]
        public void GlossaryRepositoryUpdateShouldEntryWithTheCorrectEntity()
        {
            //arrange 
            var repo = GetMockDbContextRepository();
            var glossary = new Glossary() {Term = "rath", Definition = "The anger of rage"};
            var result = new Glossary();
            _mockGlossaryDataContext.Setup(g => g.SetModified(It.IsAny<object>())).Callback((object o) =>
                                                                                                {
                                                                                                    result =
                                                                                                        o as Glossary;
                                                                                                });
            //act
            repo.Update(glossary);

            //assert
            Assert.AreEqual(glossary,result);
        }

        [Test]
        public void GlossaryRepositorySubmitChangesShouldCallContextSave()
        {
            //arrange 
            var repo = GetMockDbContextRepository();
            
            //act
            repo.SubmitChanges();

            //assert
            _mockGlossaryDataContext.Verify(c=>c.SaveChanges(),Times.Once());
        }
    }
}
