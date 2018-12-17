using Swapi.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swapi.Core.Repository
{
    /// <summary>
    /// Repository that contains details of Starships.
    /// </summary>
    public interface IStarshipRepository
    {
        /// <summary>
        /// Get all Starships in repository.
        /// </summary>
        /// <returns>
        /// A Enumeration of starships.
        /// </returns>
        IEnumerable<Starship> GetAllStarships();
    }
}
