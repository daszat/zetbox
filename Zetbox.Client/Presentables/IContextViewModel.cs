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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Zetbox.Client.Presentables
{
    public interface IContextViewModel
    {
        ICommandViewModel AbortCommand { get; }
        ICommandViewModel DeleteCommand { get; }
        ICommandViewModel SaveCommand { get; }
        ICommandViewModel VerifyCommand { get; }

        bool CanSave();
        void Save();
        void Abort();
        void Verify();
        void Delete(IEnumerable<DataObjectViewModel> items);

        bool IsContextModified { get; }

        void RegisterError(IDataErrorInfo vmdl);
    }
}
