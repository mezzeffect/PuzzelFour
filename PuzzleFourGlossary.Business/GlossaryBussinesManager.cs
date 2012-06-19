using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PuzzleFourGlossary.Dto;
using PuzzleFourGlossary.Interfaces;
using PuzzleFourGlossary.Interfaces.Business;
using PuzzleFourGlossary.Interfaces.Repository;

namespace PuzzleFourGlossary.Business
{
    public class GlossaryBusinessManager : IGlossaryBusinessManager<Glossary>
    {
        #region P R I V A T E
        
        private readonly IEntityRepository<Glossary> _glossaryRepository; 
        
        #endregion

        #region C O N S T R U C T O R S
        
        public GlossaryBusinessManager(IEntityRepository<Glossary> glossaryRepository)
        {
            _glossaryRepository = glossaryRepository;
        } 
        
        #endregion

        #region I  G L O S S A R Y  B U S I N E S S  M A N A G E R  M E M B E R S

        public Glossary Find(int id)
        {
            try
            {
                return _glossaryRepository.GetById(id);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddGlossary(Glossary glossary)
        {
            try
            {
                _glossaryRepository.Add(glossary);
                _glossaryRepository.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Glossary> All()
        {
            try
            {
                return _glossaryRepository.All();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool RemoveGlossary(Glossary glossary)
        {
            try
            {
                _glossaryRepository.Delete(glossary);
                _glossaryRepository.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateGlossary(Glossary glossary)
        {
            try
            {
                _glossaryRepository.Update(glossary);
                _glossaryRepository.SubmitChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        } 

        #endregion
    }
}
