using System.Collections.Generic;

namespace PuzzleFourGlossary.Interfaces.Business
{
    public interface IGlossaryBusinessManager<T>
    {
        /// <summary>
        /// Finds entity with id 
        /// </summary>
        /// <param name="id">integer represent the id of the entity desired</param>
        /// <returns>an instnace of enity with the id</returns>
        T Find(int id);

        /// <summary>
        /// Adds a new glossary to the database
        /// </summary>
        /// <param name="glossary">glossary database object</param>
        /// <returns>true if successful and false if failure</returns>
        bool AddGlossary(T glossary);

        /// <summary>
        /// Returns a list of Glossaries
        /// </summary>
        /// <returns>List of glossaries</returns>
        List<T> All();

        /// <summary>
        /// Removes a glossary from the database
        /// </summary>
        /// <param name="glossary">glossary database object</param>
        /// <returns>true if successful and false if failure</returns>
        bool RemoveGlossary(T glossary);

        /// <summary>
        /// Updates glossary from the database
        /// </summary>
        /// <param name="glossary">glossary database object</param>
        /// <returns>true if successful and false if failure</returns>
        bool UpdateGlossary(T glossary);
    }
}
