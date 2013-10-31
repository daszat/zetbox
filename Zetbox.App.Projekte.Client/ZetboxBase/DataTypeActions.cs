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
    using Zetbox.Client.GUI;
    using Zetbox.API.Common;

    /// <summary>
    /// Client implementation
    /// </summary>
    [Implementor]
    public class DataTypeActions
    {
        private static IViewModelFactory _vmf;
        private static IFrozenContext _frozenCtx;
        // private static IAssetsManager _assets;

        public DataTypeActions(IViewModelFactory vmf, IFrozenContext frozenCtx, IAssetsManager assets)
        {
            if (vmf == null) throw new ArgumentNullException("vmf");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");
            // if (assets == null) throw new ArgumentNullException("assets");

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
                        if (chosenClass != null && chosenClass.Count() == 1)
                        {
                            var propCls = (ObjectClass)chosenClass.Single().Object;
                            bool show;
                            var newProp = ShowCreatePropertyDialog(ctx, propCls, obj.Module, out show);
                            if (newProp != null)
                            {
                                obj.Properties.Add(newProp);
                                if (show)
                                {
                                    e.Result = newProp;
                                }
                            }
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

        private static Property ShowCreatePropertyDialog(IZetboxContext ctx, ObjectClass propCls, Module targetModule, out bool show)
        {
            var ifType = propCls.GetDescribedInterfaceType();

            var dlg = _vmf.CreateDialog(ctx, string.Format(Zetbox.App.Projekte.Client.ZetboxBase.Strings.CreatePropertyDlg_Title, ifType.Type.Name))
                .AddString("name", NamedObjects.Base.Classes.Zetbox.App.Base.Property_Properties.Name.Find(_frozenCtx).GetLabel())
                .AddString("label", NamedObjects.Base.Classes.Zetbox.App.Base.Property_Properties.Label.Find(_frozenCtx).GetLabel(), allowNullInput: true)
                .AddString("description", NamedObjects.Base.Classes.Zetbox.App.Base.Property_Properties.Description.Find(_frozenCtx).GetLabel(), allowNullInput: true)
                .AddString("categorytags", NamedObjects.Base.Classes.Zetbox.App.Base.Property_Properties.CategoryTags.Find(_frozenCtx).GetLabel(), allowNullInput: true, vmdesc: NamedObjects.Gui.ViewModelDescriptors.Zetbox_Client_Presentables_ZetboxBase_TagPropertyEditorViewModel.Find(_frozenCtx))
                .AddObjectReference("module", NamedObjects.Base.Classes.Zetbox.App.Base.Property_Properties.Module.Find(_frozenCtx).GetLabel(), typeof(Module).GetObjectClass(_frozenCtx), value: targetModule)
                .AddBool("isNullable", Zetbox.App.Projekte.Client.ZetboxBase.Strings.IsNullable, value: true)
                .AddBool("show", Zetbox.App.Projekte.Client.ZetboxBase.Strings.ShowPropertyWhenFinished, value: false);

            Property newProp = null;
            bool localShow = false;
            dlg.Show(((values) =>
            {
                newProp = (Property)ctx.Create(ifType);
                newProp.Name = (string)values["name"];
                newProp.Label = (string)values["label"];
                newProp.Description = (string)values["description"];
                newProp.CategoryTags = (string)values["categorytags"];
                newProp.Module = (Module)values["module"];
                var isNullable = (bool)values["isNullable"];
                if (!isNullable)
                {
                    newProp.Constraints.Add(ctx.Create<NotNullableConstraint>());
                }

                localShow = (bool)values["show"];
            }));

            show = localShow;
            return newProp;
        }
    }
}
