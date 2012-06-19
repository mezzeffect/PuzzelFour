using System.Collections.Generic;

namespace PuzzleFourGlossary.Interfaces.Repository
{
    public interface IEntityRepository<TEntity>
    {
        /// <summary>
        /// Finds entity with id 
        /// </summary>
        /// <param name="id">integer represent the id of the entity desired</param>
        TEntity GetById(int? id);

        /// <summary>
        /// Adds a new entity to the database
        /// </summary>
        /// <param name="entity">entity database object</param>
        void Add(TEntity entity);

        /// <summary>
        /// Removes a entity from the database
        /// </summary>
        /// <param name="entity">Entity database object</param>
        void Delete(TEntity entity);

        /// <summary>
        /// Updates a entity from the database
        /// </summary>
        /// <param name="entity">Entity database object</param>
        void Update(TEntity entity);

        /// <summary>
        /// Returns a list of Entities
        /// </summary>
        /// <returns>List of entities</returns>
        List<TEntity> All();

        /// <summary>
        /// Saves the changes made to the context to the database 
        /// </summary>
        void SubmitChanges();
    }
}
