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
namespace Zetbox.App.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.API.Common;

    [Implementor]
    public class EnumerationEntryActions
    {
        private static IAssetsManager _assets;

        public EnumerationEntryActions(IAssetsManager assets)
        {
            _assets = assets;
        }

        [Invocation]
        public static System.Threading.Tasks.Task GetLabel(Zetbox.App.Base.EnumerationEntry obj, MethodReturnEventArgs<System.String> e)
        {
            e.Result = !string.IsNullOrEmpty(obj.Label) ? obj.Label : obj.Name;

            if (obj.Enumeration == null || obj.Enumeration.Module == null)
                return System.Threading.Tasks.Task.CompletedTask;

            e.Result = _assets.GetString(obj.Enumeration.Module, ZetboxAssetKeys.ConstructBaseName(obj.Enumeration), ZetboxAssetKeys.ConstructLabelKey(obj), e.Result);

            return System.Threading.Tasks.Task.CompletedTask;
        }


        [Invocation]
        public static System.Threading.Tasks.Task ToString(EnumerationEntry obj, MethodReturnEventArgs<string> e)
        {
            e.Result = obj.Enumeration + "." + obj.Name;

            ToStringHelper.FixupFloatingObjectsToString(obj, e);

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
