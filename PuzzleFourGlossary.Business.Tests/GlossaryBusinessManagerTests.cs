using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces;
using PuzzleFourGlossary.Interfaces.Business;
using PuzzleFourGlossary.Interfaces.Data;
using PuzzleFourGlossary.Interfaces.Repository;

namespace PuzzleFourGlossary.Business.Tests
{
    [TestFixture]
    public class GlossaryBusinessManagerTests
    {
        private Mock<IEntityRepository<Glossary>> _mockGlossaryRepository;

        private IGlossaryBusinessManager<Glossary> GetGlossaryBussinessManager()
        {
            _mockGlossaryRepository = new Mock<IEntityRepository<Glossary>>();
            return new GlossaryBusinessManager(_mockGlossaryRepository.Object);
        }

        [Test]
        public void GlossaryBusinessManagerFindShouldCallRepoCallById()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();

            //act
            manager.Find(0);

            //assert
            _mockGlossaryRepository.Verify(g=>g.GetById(It.IsAny<int>()),Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerFindShouldCallRepoCallByIdWithCorrectId()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            int? id = 0;
            _mockGlossaryRepository.Setup(g => g.GetById(It.IsAny<int>())).Callback((int? i)=>
                                                                                        {
                                                                                            id = i;
                                                                                        });
            //act
            manager.Find(12);

            //assert
            Assert.AreEqual(12,id);
        }

        [Test]
        public void GlossaryBusinessManagerFindShouldReturnNullIfGetByIdThrowsException()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            int? id = 0;
            _mockGlossaryRepository.Setup(g => g.GetById(It.IsAny<int>())).Throws(new Exception());
            //act
            var result = manager.Find(12);

            //assert
            Assert.Null(result);
        }

        [Test]
        public void GlossaryBusinessManagerAddShouldReturnTrueIfNothingIsWrong()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            //act
            var result = manager.AddGlossary(new Glossary());

            //assert
            Assert.AreEqual(true,result);
        }

        [Test]
        public void GlossaryBusinessManagerAddShouldCallRepoAddIfNothingIsWrong()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            //act
            var result = manager.AddGlossary(new Glossary());

            //assert
            _mockGlossaryRepository.Verify(m=>m.Add(It.IsAny<Glossary>()),Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerAddShouldCallRepoSubmitChanges()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            //act
            var result = manager.AddGlossary(new Glossary());

            //assert
            _mockGlossaryRepository.Verify(m => m.SubmitChanges(), Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerAddShouldReturnFalseIfAddThowsException()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            _mockGlossaryRepository.Setup(m => m.Add(It.IsAny<Glossary>())).Throws(new Exception());
            
            //act
            var result = manager.AddGlossary(new Glossary());

            //assert
            Assert.False(result);
            
        }

        [Test]
        public void GlossaryBusinessManagerAddShouldReturnFalseIfSubmitChangesThrowsException()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            _mockGlossaryRepository.Setup(m => m.SubmitChanges()).Throws(new Exception());

            //act
            var result = manager.AddGlossary(new Glossary());

            //assert
            Assert.False(result);

        }

        [Test]
        public void GlossaryBusinessManagerAllShouldCallRepoAll()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();

            //act
            var result = manager.All();

            //assert
            _mockGlossaryRepository.Verify(g=>g.All(),Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerAllShouldReturnNullIfExceptionIsThrown()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            _mockGlossaryRepository.Setup(g => g.All()).Throws(new Exception());
            
            //act
            var result = manager.All();

            //assert
            Assert.Null(result);
        }

        [Test]
        public void GlossaryBusinessManagerRemoveShouldCallRepDelete()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();

            //act
            var result = manager.RemoveGlossary(It.IsAny<Glossary>());

            //assert
            _mockGlossaryRepository.Verify(g=>g.Delete(It.IsAny<Glossary>()),Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerRemoveShouldCallRepSubmitChanges()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();

            //act
            var result = manager.RemoveGlossary(It.IsAny<Glossary>());

            //assert
            _mockGlossaryRepository.Verify(g => g.SubmitChanges(),Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerRemoveShouldReturnFalseIfExceptionIsThrown()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            _mockGlossaryRepository.Setup(g => g.Delete(It.IsAny<Glossary>())).Throws(new Exception());
            //act
            var result = manager.RemoveGlossary(It.IsAny<Glossary>());

            //assert
            Assert.False(result);
        }

        [Test]
        public void GlossaryBusinessManagerRemoveShouldReturnTrueIfSuccessfull()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            //act
            var result = manager.RemoveGlossary(It.IsAny<Glossary>());

            //assert
            Assert.True(result);
        }

        [Test]
        public void GlossaryBusinessManagerUpdateShouldCallRepUpdate()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();

            //act
            var result = manager.UpdateGlossary(It.IsAny<Glossary>());

            //assert
            _mockGlossaryRepository.Verify(g => g.Update(It.IsAny<Glossary>()), Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerUpdateShouldCallRepSubmitChanges()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();

            //act
            var result = manager.UpdateGlossary(It.IsAny<Glossary>());

            //assert
            _mockGlossaryRepository.Verify(g => g.SubmitChanges(), Times.Once());
        }

        [Test]
        public void GlossaryBusinessManagerUpdateShouldReturnFalseIfExceptionIsThrown()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            _mockGlossaryRepository.Setup(g => g.Update(It.IsAny<Glossary>())).Throws(new Exception());
            //act
            var result = manager.UpdateGlossary(It.IsAny<Glossary>());

            //assert
            Assert.False(result);
        }

        [Test]
        public void GlossaryBusinessManagerUpdateShouldReturnTrueIfSuccessfull()
        {
            //arrange
            var manager = GetGlossaryBussinessManager();
            
            //act
            var result = manager.UpdateGlossary(It.IsAny<Glossary>());

            //assert
            Assert.True(result);
        }


    }
}
