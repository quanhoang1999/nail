using System;
using System.Collections.Generic;
using System.Text;

namespace Cms.Data.Interfaces
{
    public interface IDateTracking
    {
        DateTime DateCreated { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was last modified.
        /// </summary>
        DateTime? DateModified { get; set; }

        /// <summary>
        /// Gets or sets the date and time the object was delete.
        /// </summary>
        DateTime? DateDeleted { get; set; }
    }
}
