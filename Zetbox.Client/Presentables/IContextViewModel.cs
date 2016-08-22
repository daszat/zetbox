// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Client.Presentables
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// A ViewModel able to edit a set of IDataObjects.
    /// </summary>
    public interface IContextViewModel
    {
        bool CanSave();
        /// <summary>
        /// Tries to save the current changes.
        /// </summary>
        /// <returns>false if saving failed due to communication or validation errors</returns>
        bool Save();
        ICommandViewModel SaveCommand { get; }
        event EventHandler Saving;
        event EventHandler Saved;

        void Abort();
        ICommandViewModel AbortCommand { get; }

        void Verify();
        ICommandViewModel VerifyCommand { get; }

        /// <summary>
        /// Deletes the current selected data object
        /// </summary>
        void Delete();
        ICommandViewModel DeleteCommand { get; }

        bool IsContextModified { get; }
    }
}
