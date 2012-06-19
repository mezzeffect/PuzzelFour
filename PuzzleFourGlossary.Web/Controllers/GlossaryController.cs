using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces;
using PuzzleFourGlossary.Interfaces.Business;
using PuzzleFourGlossary.Web.Helper;
using PuzzleFourGlossary.Web.Models;
using PuzzleFourGlossary.Web.Helpers;

namespace PuzzleFourGlossary.Web.Controllers
{
    public class GlossaryController : Controller
    {
        #region P R I V A T E 
        
        private readonly IGlossaryBusinessManager<Glossary> _glossaryBusinessManager; 
        
        #endregion

        #region C O N S T R U C T O R
        
        public GlossaryController(IGlossaryBusinessManager<Glossary> glossaryBusinessManager)
        {
            _glossaryBusinessManager = glossaryBusinessManager;
        } 
        
        #endregion
       
        
        //
        // GET: /Glossary/
        public ActionResult Index()
        {
            var model = GetGlossaryModel();
            return View(model);
        }

        // POST: /Glossary/SaveGlossary/
        [HttpPost]
        public ActionResult SaveGlossary(FormCollection collection)
        {
            var model = new GlossaryView();
            try
            {
                if (TryUpdateModel(model))
                {
                    var glossaryDto = model.ToGlossaryDto();
                    if (glossaryDto == null)
                    {
                        return Json(new { success = false, errorMessage = "Error saving glossary" });
                    }

                    // new glossary
                    if (model.Id == -1)
                    {
                        _glossaryBusinessManager.AddGlossary(glossaryDto); 
                    }
                    else//edit existing glossary
                    {
                        _glossaryBusinessManager.UpdateGlossary(glossaryDto); 
                    }
                    
                    var glossaryModel = GetGlossaryModel();
                    return Json(new { success = true, model = MvcHelper.EncodeJson(glossaryModel) }, JsonRequestBehavior.AllowGet);

                }
                return Json(new { success = false, errorMessage = string.Join(Environment.NewLine, ModelState.GetErros().ToArray()) });
            }
            catch
            {
                return Json(new { success = false, errorMessage = "Error saving glossary" });
            }
        }


        // POST: /Glossary/DeleteGlossary/5
        [HttpPost]
        public ActionResult DeleteGlossary(int id)
        {
            try
            {
                var glossary = _glossaryBusinessManager.Find(id);
                _glossaryBusinessManager.RemoveGlossary(glossary);
                return Json(new {success = true,id = id});
            }
            catch
            {
                return Json(new { success = false, errorMessage="Error occured while removing glossary" });
            }
        }

        #region P R I V  H E L P E R
        private GlossaryModel GetGlossaryModel()
        {
            var model = new GlossaryModel();
            model.Glossaries = _glossaryBusinessManager.All();
            model.GlossaryEditModel = new GlossaryView();
            return model;
        } 
        #endregion
    }
}
