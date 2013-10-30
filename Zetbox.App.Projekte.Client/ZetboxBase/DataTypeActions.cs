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
    using Zetbox.API.Utils;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;
    using Zetbox.App.GUI;
    using Zetbox.Client;
    using Zetbox.Client.Presentables;
    using ViewModelDescriptors = Zetbox.NamedObjects.Gui.ViewModelDescriptors;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class DataTypeActions
    {
        private static IViewModelFactory _vmf;
        private static IFrozenContext _frozenCtx;

        public DataTypeActions(IViewModelFactory vmf, IFrozenContext frozenCtx)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            _vmf = vmf;
            _frozenCtx = frozenCtx;
        }

        [Invocation]
        public static void NotifyDeleting(DataType obj)
        {
            var ctx = obj.Context;
            foreach (var prop in obj.Properties.ToList())
            {
                ctx.Delete(prop);
            }

            foreach (var m in obj.Methods.ToList())
            {
                ctx.Delete(m);
            }

            foreach (var c in obj.Constraints.ToList())
            {
                ctx.Delete(c);
            }
        }

        [Invocation]
        public static void AddProperty(DataType obj, MethodReturnEventArgs<Zetbox.App.Base.Property> e)
        {
            var candidates = new List<ObjectClass>()
            {
                // Common first
                typeof(StringProperty).GetObjectClass(_frozenCtx),
                typeof(BoolProperty).GetObjectClass(_frozenCtx),
                typeof(ObjectReferenceProperty).GetObjectClass(_frozenCtx),
                typeof(DateTimeProperty).GetObjectClass(_frozenCtx),
                typeof(DecimalProperty).GetObjectClass(_frozenCtx),
                typeof(EnumerationProperty).GetObjectClass(_frozenCtx),
                typeof(CompoundObjectProperty).GetObjectClass(_frozenCtx),

                // all other
                typeof(IntProperty).GetObjectClass(_frozenCtx),
                typeof(DoubleProperty).GetObjectClass(_frozenCtx),
                typeof(GuidProperty).GetObjectClass(_frozenCtx),
                typeof(CalculatedObjectReferenceProperty).GetObjectClass(_frozenCtx),
            };

            var ctx = obj.Context;
            var selectClass = _vmf
                .CreateViewModel<DataObjectSelectionTaskViewModel.Factory>()
                .Invoke(
                    ctx,
                    null,
                    typeof(ObjectClass).GetObjectClass(_frozenCtx),
                    () => candidates.AsQueryable(),
                    (chosenClass) =>
                    {
                        if (chosenClass != null)
                        {

                        }
                    },
                    null);
            selectClass.ListViewModel.ViewMethod = InstanceListViewMethod.List;
            selectClass.ListViewModel.AllowDelete = false;
            selectClass.ListViewModel.AllowOpen = false;
            selectClass.ListViewModel.AllowAddNew = false;
            selectClass.ListViewModel.UseNaturalSortOrder = true;
            _vmf.ShowDialog(selectClass);
        }
    }
}
