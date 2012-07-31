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

namespace Zetbox.Client.Models
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
    using Zetbox.Client.Presentables.ValueViewModels;
    using System.Collections.ObjectModel;

    public class ColumnDisplayModel
    {
        public enum ColumnType
        {
            /// <summary>
            /// References a DataObjectViewModel.PropertyModelsByName[] ViewModel
            /// </summary>
            PropertyModel,
            /// <summary>
            /// References a ViewModels property directly
            /// </summary>
            Property,
            /// <summary>
            /// References a DataObjectViewModel.ActionViewModelsByName[] ViewModel
            /// </summary>
            MethodModel,
        }

        internal ColumnDisplayModel()
            : this(string.Empty, string.Empty, null, ColumnType.PropertyModel)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property name</param>
        public ColumnDisplayModel(string header, string name)
            : this(header, name, null, ColumnType.PropertyModel)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property name</param>
        /// <param name="kind">The requested control kind or null if default should be used</param>
        public ColumnDisplayModel(string header, string name, ControlKind kind)
            : this(header, name, kind, ColumnType.PropertyModel)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property name</param>
        /// <param name="kind">The requested control kind or null if default should be used</param>
        /// <param name="requestedWidth">Requested list column width</param>
        public ColumnDisplayModel(string header, string name, ControlKind kind, WidthHint requestedWidth)
            : this(header, name, kind, null, ColumnType.PropertyModel, requestedWidth, null)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property name</param>
        /// <param name="kind">The requested control kind or null if default should be used</param>
        /// <param name="requestedWidthAbsoulte">Requested list column width in absolute toolkit units</param>
        public ColumnDisplayModel(string header, string name, ControlKind kind, int requestedWidthAbsoulte)
            : this(header, name, kind, null, ColumnType.PropertyModel, WidthHint.Default, requestedWidthAbsoulte)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property, viewmodels property or method name</param>
        /// <param name="type">Requested column type</param>
        public ColumnDisplayModel(string header, string name, ColumnType type)
            : this(header, name, null, type)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property, viewmodels property or method name</param>
        /// <param name="kind">The requested control kind or null if default should be used</param>
        /// <param name="type">Requested column type</param>
        public ColumnDisplayModel(string header, string name, ControlKind kind, ColumnType type)
            : this(header, name, kind, null, type, WidthHint.Default, null)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property name</param>
        /// <param name="kind">The requested editor kind or null if default should be used</param>
        /// <param name="gridPreviewKind">The requested preview kind or null if default should be used</param>
        public ColumnDisplayModel(string header, string name, ControlKind kind, ControlKind gridPreviewKind)
            : this(header, name, kind, gridPreviewKind, ColumnType.PropertyModel, WidthHint.Default, null)
        {
        }

        /// <summary>
        /// Creates a ColumnDisplayModel
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="name">property, viewmodels property or method name</param>
        /// <param name="kind">The requested editor kind or null if default should be used</param>
        /// <param name="gridPreviewKind">The requested preview kind or null if default should be used</param>
        /// <param name="type">Requested column type</param>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="requestedWidthAbsolute">Requested list column width in absolute toolkit units</param>
        public ColumnDisplayModel(string header, string name, ControlKind kind, ControlKind gridPreviewKind, ColumnType type, WidthHint requestedWidth, int? requestedWidthAbsolute)
        {
            this.Header = header;
            this.Name = name;
            this.ControlKind = kind;
            this.GridPreEditKind = gridPreviewKind ?? kind;
            this.Type = type;
            this.RequestedWidth = requestedWidth;
            this.RequestedWidthAbsolute = requestedWidthAbsolute;
        }

        public ColumnType Type { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }
        public ControlKind ControlKind { get; set; }
        public ControlKind GridPreEditKind { get; set; }
        public WidthHint RequestedWidth { get; set; }
        public int? RequestedWidthAbsolute { get; set; }
        public Property Property { get; set; }
        public App.Base.Property[] Properties { get; set; }

        public override string ToString()
        {
            return Header;
        }

        public string ExtractFormattedValue(DataObjectViewModel obj)
        {
            if (obj == null) return string.Empty;

            string val = null;
            switch (this.Type)
            {
                case ColumnDisplayModel.ColumnType.Property:
                    var propVal = obj.GetPropertyValue<object>(this.Name);
                    val = propVal != null ? propVal.ToString() : string.Empty;
                    break;
                case ColumnDisplayModel.ColumnType.PropertyModel:
                    DataObjectViewModel objVmdl = obj;
                    IFormattedValueViewModel resultVMdl = null;
                    foreach (var current in this.Name.Split('.'))
                    {
                        if (objVmdl == null) break;

                        resultVMdl = objVmdl.PropertyModelsByName[current];
                        if (resultVMdl is ObjectReferenceViewModel)
                        {
                            objVmdl = ((ObjectReferenceViewModel)resultVMdl).Value;
                        }
                    }
                    val = resultVMdl != null ? resultVMdl.FormattedValue : string.Empty;
                    break;
            }
            return val;
        }
    }

    public class GridDisplayConfiguration
    {
        public enum Mode
        {
            ReadOnly,
            Editable,
        }

        public bool ShowId { get; set; }
        public bool ShowIcon { get; set; }
        public bool ShowName { get; set; }

        public ObservableCollection<ColumnDisplayModel> Columns { get; private set; }

        public GridDisplayConfiguration()
        {
        }

        private bool ContainsSummaryTag(string categoryTags)
        {
            return (categoryTags ?? String.Empty).Split(',', ' ').Contains("Summary");
        }

        private void BuildColumns(Zetbox.App.Base.DataType cls, IEnumerable<Property> props, IEnumerable<Method> methods, Mode mode)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            if (props == null) throw new ArgumentNullException("props");
            if (methods == null) throw new ArgumentNullException("methods");

            ShowIcon = cls.ShowIconInLists;
            ShowId = cls.ShowIdInLists;
            ShowName = cls.ShowNameInLists;

            this.Columns = new ObservableCollection<ColumnDisplayModel>(
                props.Select(p => CreateColumnDisplayModel(mode, p))
                     .Concat(methods.Select(m => GridDisplayConfiguration.CreateColumnDisplayModel(m)))
                     .ToList()
            );
        }

        public void BuildColumns(Zetbox.App.Base.DataType cls, Mode mode, bool showMethods)
        {
            if (cls is ObjectClass)
            {
                BuildColumns((ObjectClass)cls, mode, showMethods);
            }
            else if (cls is CompoundObject)
            {
                BuildColumns((CompoundObject)cls, mode, showMethods);
            }
            else
            {
                throw new InvalidOperationException("Unsupported DataType");
            }
        }

        public void BuildColumns(Zetbox.App.Base.CompoundObject cls, Mode mode, bool showMethods)
        {
            BuildColumns(cls, cls.Properties, showMethods ? cls.Methods.Where(m => m.IsDisplayable) : new Method[] { }, mode);
        }

        public void BuildColumns(Zetbox.App.Base.ObjectClass cls, Mode mode, bool showMethods)
        {
            if (cls == null) throw new ArgumentNullException("cls");

            var props = cls.GetAllProperties()
                .Where(p => ContainsSummaryTag(p.CategoryTags))
                .ToList();

            if (props.Count == 0)
            {
                props = cls.GetAllProperties().Where(p =>
                {
                    var orp = p as ObjectReferenceProperty;
                    if (orp == null) { return true; }

                    switch (orp.RelationEnd.Parent.GetRelationType())
                    {
                        case RelationType.n_m:
                            return false; // don't display lists in grids
                        case RelationType.one_n:
                            return orp.RelationEnd.Multiplicity.UpperBound() > 1; // if we're "n", the navigator is a pointer, not a list
                        case RelationType.one_one:
                            return true; // can always display
                        default:
                            break; // return false; // something went wrong
                    }
                    return false; // workaround for https://bugzilla.novell.com/show_bug.cgi?id=660569
                })
                .ToList();
            }

            var methods = cls.GetAllMethods()
                .Where(m => m.IsDisplayable && ContainsSummaryTag(m.CategoryTags));

            BuildColumns(cls, props, showMethods ? methods.ToArray() : new Method[0], mode);
        }

        public static ColumnDisplayModel CreateColumnDisplayModel(Method m)
        {
            return new ColumnDisplayModel()
            {
                Header = m.GetLabel(),
                Name = m.Name,
                Type = ColumnDisplayModel.ColumnType.MethodModel,
                RequestedWidth = WidthHint.Medium,
            };
        }

        public static ColumnDisplayModel CreateColumnDisplayModel(Mode mode, params Property[] p)
        {
            if (p == null) throw new ArgumentNullException("p");
            if (p.Length == 0) throw new ArgumentOutOfRangeException("p", "At least one property is requiered");

            var last = p.Last();

            var colMdl = new ColumnDisplayModel()
            {
                Header = string.Join(", ", p.Select(i => i.GetLabel()).ToArray()),
                Name = string.Join(".", p.Select(i => i.Name).ToArray()),
                Property = last,
                Properties = p,
                RequestedWidth = p.Last().GetDisplayWidth(),
            };
            switch (mode)
            {
                case Mode.ReadOnly:
                    colMdl.ControlKind = last.ValueModelDescriptor.GetDefaultGridCellDisplayKind();
                    colMdl.GridPreEditKind = last.ValueModelDescriptor.GetDefaultGridCellDisplayKind();
                    break;
                case Mode.Editable:
                    colMdl.ControlKind = last.ValueModelDescriptor.GetDefaultGridCellEditorKind();
                    colMdl.GridPreEditKind = last.ValueModelDescriptor.GetDefaultGridCellPreEditorKind();
                    break;
            }
            return colMdl;
        }
    }
}
