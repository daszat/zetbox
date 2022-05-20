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
        public static System.Threading.Tasks.Task NotifyDeleting(DataType obj)
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

            return System.Threading.Tasks.Task.CompletedTask;
        }

        public class PropertyTypeSelectionViewModel : ViewModel
        {
            public new delegate PropertyTypeSelectionViewModel Factory(IZetboxContext dataCtx, ViewModel parent, string name, ObjectClass targetPropClass);

            public PropertyTypeSelectionViewModel(IViewModelDependencies dependencies, IZetboxContext dataCtx, ViewModel parent, string name, ObjectClass targetPropClass)
                : base(dependencies, dataCtx, parent)
            {
                _name = name;
                TargetPropClass = targetPropClass;
            }

            public override ControlKind RequestedKind
            {
                get
                {
                    return NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_TextKind.Find(FrozenContext);
                }
            }

            private string _name;
            public override string Name
            {
                get { return _name; }
            }

            public ObjectClass TargetPropClass { get; private set; }
        }

        [Invocation]
        public static async System.Threading.Tasks.Task AddProperty(DataType obj, MethodReturnEventArgs<Zetbox.App.Base.Property> e)
        {
            var ctx = obj.Context;
            var candidates = new List<PropertyTypeSelectionViewModel>()
            {
                // Common first
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "string", typeof(StringProperty).GetObjectClass(_frozenCtx)),
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "bool", typeof(BoolProperty).GetObjectClass(_frozenCtx)),
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "DateTime", typeof(DateTimeProperty).GetObjectClass(_frozenCtx)),
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "decimal", typeof(DecimalProperty).GetObjectClass(_frozenCtx)),
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "enum", typeof(EnumerationProperty).GetObjectClass(_frozenCtx)),
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "Compound object", typeof(CompoundObjectProperty).GetObjectClass(_frozenCtx)),

                // all other
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "int", typeof(IntProperty).GetObjectClass(_frozenCtx)),
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "double", typeof(DoubleProperty).GetObjectClass(_frozenCtx)),
                _vmf.CreateViewModel<PropertyTypeSelectionViewModel.Factory>().Invoke(ctx, null, "Guid", typeof(GuidProperty).GetObjectClass(_frozenCtx)),
                

                // No ObjectReferance -> create a relation
                // can be added to this wizard -> future task
                // typeof(ObjectReferenceProperty).GetObjectClass(_frozenCtx),
                // typeof(CalculatedObjectReferenceProperty).GetObjectClass(_frozenCtx),
            };

            var selectClass = _vmf
                .CreateViewModel<SimpleSelectionTaskViewModel.Factory>()
                .Invoke(
                    ctx,
                    null,
                    candidates,
                    (chosenClass) =>
                    {
                        if (chosenClass != null && chosenClass.Count() == 1)
                        {
                            var propCls = ((PropertyTypeSelectionViewModel)chosenClass.Single()).TargetPropClass;
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
            selectClass.RequestedKind = NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_DataObjectSelectionTaskGridKind.Find(_frozenCtx);
            await _vmf.ShowDialog(selectClass);
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
                .AddBool("isNullable", Zetbox.App.Projekte.Client.ZetboxBase.Strings.IsNullable, value: true);

            if (typeof(StringProperty).IsAssignableFrom(ifType.Type))
            {
                var p = NamedObjects.Base.Classes.Zetbox.App.Base.StringRangeConstraint_Properties.MaxLength.Find(_frozenCtx);
                dlg.AddInt("str_maxlengt", p.GetLabel(), allowNullInput: true, description: p.GetDescription());
            }
            if (typeof(DateTimeProperty).IsAssignableFrom(ifType.Type))
            {
                var p = NamedObjects.Base.Classes.Zetbox.App.Base.DateTimeProperty_Properties.DateTimeStyle.Find(_frozenCtx);
                dlg.AddEnumeration("dt_style", p.GetLabel(), _frozenCtx.FindPersistenceObject<Enumeration>(new Guid("1385e46d-3e5b-4d91-bf9a-94a740f08ba1")), description: p.GetDescription(), value: (int)DateTimeStyles.Date);
            }
            if (typeof(DecimalProperty).IsAssignableFrom(ifType.Type))
            {
                var p = NamedObjects.Base.Classes.Zetbox.App.Base.DecimalProperty_Properties.Precision.Find(_frozenCtx);
                var s = NamedObjects.Base.Classes.Zetbox.App.Base.DecimalProperty_Properties.Scale.Find(_frozenCtx);
                dlg.AddInt("decimal_precision", p.GetLabel(), description: p.GetDescription(), value: 10);
                dlg.AddInt("decimal_scale", s.GetLabel(), description: s.GetDescription(), value: 2);
            }
            if (typeof(EnumerationProperty).IsAssignableFrom(ifType.Type))
            {
                var p = NamedObjects.Base.Classes.Zetbox.App.Base.EnumerationProperty_Properties.Enumeration.Find(_frozenCtx);
                dlg.AddObjectReference("enum", p.GetLabel(), typeof(Enumeration).GetObjectClass(_frozenCtx), description: p.GetDescription());
            }
            if (typeof(CompoundObjectProperty).IsAssignableFrom(ifType.Type))
            {
                var p = NamedObjects.Base.Classes.Zetbox.App.Base.CompoundObjectProperty_Properties.CompoundObjectDefinition.Find(_frozenCtx);
                dlg.AddObjectReference("cp_def", p.GetLabel(), typeof(CompoundObject).GetObjectClass(_frozenCtx), description: p.GetDescription());
            }
            if (typeof(IntProperty).IsAssignableFrom(ifType.Type))
            {
                var min = NamedObjects.Base.Classes.Zetbox.App.Base.IntegerRangeConstraint_Properties.Min.Find(_frozenCtx);
                var max = NamedObjects.Base.Classes.Zetbox.App.Base.IntegerRangeConstraint_Properties.Max.Find(_frozenCtx);
                dlg.AddInt("int_min", min.GetLabel(), description: min.GetDescription(), allowNullInput: true);
                dlg.AddInt("int_max", max.GetLabel(), description: max.GetDescription(), allowNullInput: true);
            }

            dlg.AddBool("show", Zetbox.App.Projekte.Client.ZetboxBase.Strings.ShowPropertyWhenFinished, value: false, description: Zetbox.App.Projekte.Client.ZetboxBase.Strings.ShowPropertyWhenFinishedDescription);

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
                if (!(bool)values["isNullable"])
                {
                    newProp.Constraints.Add(ctx.Create<NotNullableConstraint>());
                }

                if (values.ContainsKey("str_maxlengt"))
                {
                    var c = ctx.Create<StringRangeConstraint>();
                    c.MinLength = 0;
                    c.MaxLength = (int?)values["str_maxlengt"];
                    newProp.Constraints.Add(c);
                }
                if (values.ContainsKey("int_min") && values.ContainsKey("int_max") && values["int_min"] != null && values["int_max"] != null)
                {
                    var c = ctx.Create<IntegerRangeConstraint>();
                    c.Min = (int)values["int_min"];
                    c.Max = (int)values["int_max"];
                    newProp.Constraints.Add(c);
                }
                if (values.ContainsKey("dt_style"))
                {
                    ((DateTimeProperty)newProp).DateTimeStyle = (DateTimeStyles)values["dt_style"];
                }
                if (values.ContainsKey("decimal_precision"))
                {
                    ((DecimalProperty)newProp).Precision = (int)values["decimal_precision"];
                }
                if (values.ContainsKey("decimal_scale"))
                {
                    ((DecimalProperty)newProp).Scale = (int)values["decimal_scale"];
                }
                if (values.ContainsKey("enum"))
                {
                    ((EnumerationProperty)newProp).Enumeration = (Enumeration)values["enum"];
                }
                if (values.ContainsKey("cp_def"))
                {
                    ((CompoundObjectProperty)newProp).CompoundObjectDefinition = (CompoundObject)values["cp_def"];
                }

                if (newProp is CompoundObjectProperty)
                {
                    ((CompoundObjectProperty)newProp).IsList = false;
                    ((CompoundObjectProperty)newProp).HasPersistentOrder = false;
                }

                localShow = (bool)values["show"];
            }));

            show = localShow;
            return newProp;
        }
    }
}
