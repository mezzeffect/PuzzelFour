using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces;
using PuzzleFourGlossary.Interfaces.Business;
using PuzzleFourGlossary.Web.Controllers;
using PuzzleFourGlossary.Web.Models;
using Workbench3.Web.Tests;

namespace PuzzleFourGlossary.Web.Tests.Controller
{
    [TestFixture]
    public class GlossaryControllerTests
    {
        #region P R I V A T E

        private FormCollection _glossaryEditForm;
        private Mock<IGlossaryBusinessManager<Glossary>> _mockGlossaryBusinessManager; 

        #endregion

        #region P R I V  H E L P E R S
        private GlossaryController GetGlossaryController()
        {
            _mockGlossaryBusinessManager = new Mock<IGlossaryBusinessManager<Glossary>>();
            var controller = new GlossaryController(_mockGlossaryBusinessManager.Object);
            controller.SetFakeControllerContext();
            return controller;
        } 
        #endregion

        #region D A T A  S E T U P
        
        [SetUp]
        public void SetData()
        {
            _glossaryEditForm = new FormCollection() { { "Id", "1" }, { "Term", "first" }, { "Definition", "Define the first" }, };
        } 

        #endregion

        #region I N D E X
        [Test]
        public void ControllerIndexShouldCallManagerAll()
        {
            //arrange 
            var controller = GetGlossaryController();

            //act 
            controller.Index();

            //assert
            _mockGlossaryBusinessManager.Verify(g => g.All(), Times.Once());
        }

        [Test]
        public void ControllerIndexShouldReturnGlossaryModel()
        {
            //arrange 
            var controller = GetGlossaryController();

            //act 
            var result = controller.Index() as ViewResult;

            //assert
            Assert.IsInstanceOf(typeof(GlossaryModel), result.Model);
        }

        [Test]
        public void ControllerIndexShouldReturn()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });

            //act 
            var result = controller.Index() as ViewResult;
            var model = result.Model as GlossaryModel;

            //assert
            Assert.AreEqual(model.Glossaries.Count, 2);
        } 
        #endregion

        #region S A V E  &  U P D A T E
        [Test]
        public void ControllerSaveShouldReturnErrorIfGlossaryIdIsMissing()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["Id"] = "";
            //act 
            controller.ValueProvider = _glossaryEditForm;
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(wrapper["success"], false);
        }

        [Test]
        public void ControllerSaveShouldReturnErrorIfGlossaryTermIsMissing()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["Term"] = "";
            //act 
            controller.ValueProvider = _glossaryEditForm;
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(wrapper["success"], false);
        }

        [Test]
        public void ControllerSaveShouldReturnErrorIfGlossaryDefinitionIsMissing()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["Definition"] = "";
            //act 
            controller.ValueProvider = _glossaryEditForm;
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(wrapper["success"], false);
        }

        [Test]
        public void ControllerSaveShouldReturnTrueIfGlossaryNothingIsMissing()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            //act 
            controller.ValueProvider = _glossaryEditForm;
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(wrapper["success"], true);
        }

        [Test]
        public void ControllerSaveShouldCallManagerAddGlossaryIfIdIsNegativeOne()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["Id"] = "-1";
            //act 
            controller.ValueProvider = _glossaryEditForm;
            controller.SaveGlossary(_glossaryEditForm);

            //assert
            _mockGlossaryBusinessManager.Verify(m => m.AddGlossary(It.IsAny<Glossary>()), Times.Once());
        }

        [Test]
        public void ControllerSaveShouldCallManagerUpdateGlossaryIfIdIsNotNegativeOne()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            //act 
            controller.ValueProvider = _glossaryEditForm;
            controller.SaveGlossary(_glossaryEditForm);

            //assert
            _mockGlossaryBusinessManager.Verify(m => m.UpdateGlossary(It.IsAny<Glossary>()), Times.Once());
        }

        [Test]
        public void ControllerSaveShouldReturnErrorIfGlossaryCannotBeConvertedToDto()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["id"] = "xcvxcvxc";

            //act 
            controller.ValueProvider = _glossaryEditForm;
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        public void ControllerSaveShouldReturnGlossaryModelIfNothingIsWrong()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });

            //act 
            controller.ValueProvider = _glossaryEditForm;
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);
            //assert
            Assert.NotNull(wrapper["model"]);
        }

        [Test]
        public void ControllerSaveShouldReturnErrorIfInvalidFormCollection()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            var form = new FormCollection();
            controller.ValueProvider = form;

            //act 
            var result = controller.SaveGlossary(form) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        public void ControllerSaveShouldReturnErrorManagerAddThrowsException()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _mockGlossaryBusinessManager.Setup(g => g.AddGlossary(It.IsAny<Glossary>())).Throws(new Exception());
            _glossaryEditForm["Id"] = "-1";

            //act 
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        public void ControllerSaveShouldReturnErrorManagerUpdateThrowsException()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _mockGlossaryBusinessManager.Setup(g => g.UpdateGlossary(It.IsAny<Glossary>())).Throws(new Exception());

            //act 
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        public void ControllerSaveShouldReturnErrorManagerAllThrowsException()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Throws(new Exception());

            //act 
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }


        [Test]
        [TestCase("    ")]
        [TestCase("    ")]
        [TestCase("!@123")]
        [TestCase("zxczxQWEQWE")]
        [TestCase("1.2")]
        [TestCase("- 1")]
        [TestCase("'\\z")]
        [TestCase("434k")]
        public void ControllerSaveShouldReturnErrorIfId(string id)
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["Id"] = id;
            controller.ValueProvider = _glossaryEditForm;

            //act 
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        [TestCase("")]
        [TestCase("    ")]
        public void ControllerSaveShouldReturnErrorIfTerm(string term)
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["Term"] = term;
            controller.ValueProvider = _glossaryEditForm;

            //act 
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        [TestCase("")]
        [TestCase("    ")]
        public void ControllerSaveShouldReturnErrorIfDefinition(string definition)
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _glossaryEditForm["Term"] = definition;
            controller.ValueProvider = _glossaryEditForm;

            //act 
            var result = controller.SaveGlossary(_glossaryEditForm) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }
        
        #endregion

        #region D E L E T E
        [Test]
        public void ControllerDeleteShouldCallManagerFind()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });

            //act 
            var result = controller.DeleteGlossary(9) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            _mockGlossaryBusinessManager.Verify(g => g.Find(It.IsAny<int>()), Times.Once());
        }

        [Test]
        public void ControllerDeleteShouldReturnCallManagerFindThrowsExcpetion()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _mockGlossaryBusinessManager.Setup(g => g.Find(It.IsAny<int>())).Throws(new Exception());
            //act 
            var result = controller.DeleteGlossary(9) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        public void ControllerDeleteShouldCallRemoveGlossary()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });

            //act 
            var result = controller.DeleteGlossary(9) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            _mockGlossaryBusinessManager.Verify(g => g.RemoveGlossary(It.IsAny<Glossary>()), Times.Once());
        }

        [Test]
        public void ControllerDeleteShouldReturnCallManagerRemoveThrowsExcpetion()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            _mockGlossaryBusinessManager.Setup(g => g.RemoveGlossary(It.IsAny<Glossary>())).Throws(new Exception());
            //act 
            var result = controller.DeleteGlossary(9) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(false, wrapper["success"]);
        }

        [Test]
        public void ControllerDeleteShouldReturnTrueIfNothingIsWrong()
        {
            //arrange 
            var controller = GetGlossaryController();
            _mockGlossaryBusinessManager.Setup(g => g.All()).Returns(new List<Glossary>() { new Glossary(), new Glossary() });
            //act 
            var result = controller.DeleteGlossary(9) as JsonResult;
            IDictionary<string, object> wrapper = new System.Web.Routing.RouteValueDictionary(result.Data);

            //assert
            Assert.AreEqual(true, wrapper["success"]);
        } 
        #endregion
    }
}
