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
    using System.Collections.ObjectModel;
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
    using System.Globalization;
    using System.Threading.Tasks;

    public class ColumnDisplayModel
    {
        public enum ColumnType
        {
            /// <summary>
            /// References a DataObjectViewModel.PropertyModelsByName[] ViewModel
            /// </summary>
            PropertyModel = 1,
            /// <summary>
            /// References a DataObjectViewModel.ActionViewModelsByName[] ViewModel
            /// </summary>
            MethodModel = 2,
            /// <summary>
            /// References a ViewModels property directly
            /// </summary>
            ViewModelProperty = 3,
        }

        private ColumnDisplayModel()
        {
        }

        #region Create Properties
        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object property or property path
        /// </summary>
        /// <param name="mode">Requested mode (readonly, editable)</param>
        /// <param name="header">Label for the list header</param>
        /// <param name="p">list of properties to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(GridDisplayConfiguration.Mode mode, string header, params Property[] p)
        {
            var colMdl = await Create(mode, p);
            colMdl.Header = header;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object property or property path
        /// </summary>
        /// <param name="mode">Requested mode (readonly, editable)</param>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="p">list of properties to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(GridDisplayConfiguration.Mode mode, WidthHint requestedWidth, params Property[] p)
        {
            var colMdl = await Create(mode, p);
            colMdl.RequestedWidth = requestedWidth;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object property or property path
        /// </summary>
        /// <param name="mode">Requested mode (readonly, editable)</param>
        /// <param name="requestedWidthAbsoulte">Requested list column width in absolute toolkit units</param>
        /// <param name="p">list of properties to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(GridDisplayConfiguration.Mode mode, int requestedWidthAbsoulte, params Property[] p)
        {
            var colMdl = await Create(mode, p);
            colMdl.RequestedWidthAbsolute = requestedWidthAbsoulte;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object property or property path
        /// </summary>
        /// <param name="mode">Requested mode (readonly, editable)</param>
        /// <param name="header">Label for the list header</param>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="p">list of properties to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(GridDisplayConfiguration.Mode mode, string header, WidthHint requestedWidth, params Property[] p)
        {
            var colMdl = await Create(mode, p);
            colMdl.Header = header;
            colMdl.RequestedWidth = requestedWidth;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object property or property path
        /// </summary>
        /// <param name="mode">Requested mode (readonly, editable)</param>
        /// <param name="header">Label for the list header</param>
        /// <param name="requestedWidthAbsoulte">Requested list column width in absolute toolkit units</param>
        /// <param name="p">list of properties to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(GridDisplayConfiguration.Mode mode, string header, int requestedWidthAbsoulte, params Property[] p)
        {
            var colMdl = await Create(mode, p);
            colMdl.Header = header;
            colMdl.RequestedWidthAbsolute = requestedWidthAbsoulte;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object property or property path
        /// </summary>
        /// <param name="mode">Requested mode (readonly, editable)</param>
        /// <param name="header">Label for the list header</param>
        /// <param name="kind">The requested editor kind or null if default should be used</param>
        /// <param name="gridPreviewKind">The requested preview kind or null if default should be used</param>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="requestedWidthAbsolute">Requested list column width in absolute toolkit units</param>
        /// <param name="p">list of properties to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(GridDisplayConfiguration.Mode mode, string header, ControlKind kind, ControlKind gridPreviewKind, WidthHint requestedWidth, int? requestedWidthAbsolute, params Property[] p)
        {
            var colMdl = await Create(mode, p);
            colMdl.Header = header;
            colMdl.ControlKind = kind;
            colMdl.GridPreEditKind = gridPreviewKind ?? kind;
            colMdl.RequestedWidth = requestedWidth;
            colMdl.RequestedWidthAbsolute = requestedWidthAbsolute;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object property or property path
        /// </summary>
        /// <param name="mode">Requested mode (readonly, editable)</param>
        /// <param name="p">list of properties to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(GridDisplayConfiguration.Mode mode, params Property[] p)
        {
            if (p == null) throw new ArgumentNullException("p");
            if (p.Length == 0) throw new ArgumentOutOfRangeException("p", "At least one property is requiered");

            var last = p.Last();

            var colMdl = new ColumnDisplayModel()
            {
                Header = string.Join(", ", (await p.Select(async i => await i.GetLabel()).WhenAll()).ToArray()),
                Path = string.Join(".", p.Select(i => i.Name).ToArray()),
                DynamicOrderByExpression = await FormatDynamicOrderByExpression(p),
                Property = last,
                Properties = p,
                RequestedWidth = await p.Last().GetDisplayWidth(),
                Type = ColumnType.PropertyModel
            };
            switch (mode)
            {
                case GridDisplayConfiguration.Mode.ReadOnly:
                    colMdl.ControlKind = last.ValueModelDescriptor.GetDefaultGridCellDisplayKind();
                    colMdl.GridPreEditKind = last.ValueModelDescriptor.GetDefaultGridCellDisplayKind();
                    break;
                case GridDisplayConfiguration.Mode.Editable:
                    colMdl.ControlKind = last.ValueModelDescriptor.GetDefaultGridCellEditorKind();
                    colMdl.GridPreEditKind = last.ValueModelDescriptor.GetDefaultGridCellPreEditorKind();
                    break;
            }
            return colMdl;
        }

        public static async Task<string> FormatDynamicOrderByExpression(params Property[] properties)
        {
            if (properties == null) return string.Empty;
            if (properties.Length == 0) return string.Empty;

            var props = properties.ToList();
            while (props.Last() is ObjectReferenceProperty)
            {
                var refProp = (ObjectReferenceProperty)props.Last();
                var sortProp = (await refProp.GetReferencedObjectClass())
                                      .AndParents(c => c.BaseObjectClass)
                                      .SelectMany(c => c.Properties)
                                      .Where(p => p.DefaultSortPriority != null)
                                      .OrderBy(p => p.DefaultSortPriority)
                                      .FirstOrDefault();
                if (sortProp == null) break;
                if (props.Contains(sortProp)) break;
                props.Add(sortProp);
            }

            var result = new StringBuilder();
            var propsPath = new StringBuilder("it");
            var lastProp = props.Last();
            var standInValue = GetStandInValue(lastProp);
            var containsList = false;
            foreach (var p in props.Take(props.Count - 1))
            {
                propsPath.Append('.').Append(p.Name);

                if (p.GetIsList())
                {
                    result.Append(propsPath).Append("==null?0:").Append(propsPath);
                    containsList = true;
                    break; // can't sort lists
                }
                else
                {
                    result.Append(propsPath).Append("==null?").Append(standInValue).Append(":");
                }
            }

            if (!containsList && !lastProp.GetIsList())
            {
                // No lists on prop path
                propsPath.Append('.').Append(lastProp.Name);
                result.Append(propsPath);

                if (lastProp is ObjectReferenceProperty)
                {
                    result.Append("!=null?").Append(propsPath).Append(".ID:-1");
                }
            }
            else if (result.Length == 0)
            {
                // it's a list
                propsPath.Append('.').Append(lastProp.Name);
                result.Append(propsPath).Append(".Count()");
            }
            else
            {
                // it's a list on a path or the ref. property has a sort priority
                result.Append(".Count()");
            }

            return result.ToString();
        }

        private static string GetStandInValue(Property p)
        {
            if (p is DecimalProperty)
            {
                return "Decimal.MinValue";
            }
            else if (p is IntProperty)
            {
                return "Int32.MinValue";
            }
            else if (p is DoubleProperty)
            {
                return "Double.MinValue";
            }
            else if (p is StringProperty)
            {
                return "null";
            }
            else if (p is DateTimeProperty)
            {
                return "DateTime.MinValue";
            }
            else if (p is GuidProperty)
            {
                return "Guid.Empty";
            }
            else if (p is BoolProperty)
            {
                return "false";
            }
            else if (p is EnumerationProperty)
            {
                return "-1";
            }
            else if (p is ObjectReferenceProperty)
            {
                return "-1";
            }
            else if (p is CalculatedObjectReferenceProperty)
            {
                return "-1";
            }
            else if (p is CompoundObjectProperty)
            {
                return "null";
            }
            else
            {
                throw new NotSupportedException(string.Format("DynamicOrderByExpression for Property Type {0} are not supported yet", p.GetType().Name));
            }
        }
        #endregion

        #region Create Methods
        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object method
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="m">method to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(string header, Method m)
        {
            var colMdl = await Create(m);
            colMdl.Header = header;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object method
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="m">method to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(string header, WidthHint requestedWidth, Method m)
        {
            var colMdl = await Create(m);
            colMdl.Header = header;
            colMdl.RequestedWidth = requestedWidth;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object method
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="requestedWidthAbsoulte">Requested list column width in absolute toolkit units</param>
        /// <param name="m">method to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(string header, int requestedWidthAbsoulte, Method m)
        {
            var colMdl = await Create(m);
            colMdl.Header = header;
            colMdl.RequestedWidthAbsolute = requestedWidthAbsoulte;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object method
        /// </summary>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="m">method to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(WidthHint requestedWidth, Method m)
        {
            var colMdl = await Create(m);
            colMdl.RequestedWidth = requestedWidth;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object method
        /// </summary>
        /// <param name="requestedWidthAbsoulte">Requested list column width in absolute toolkit units</param>
        /// <param name="m">method to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(int requestedWidthAbsoulte, Method m)
        {
            var colMdl = await Create(m);
            colMdl.RequestedWidthAbsolute = requestedWidthAbsoulte;
            return colMdl;
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a zetbox object method
        /// </summary>
        /// <param name="m">method to build column for</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static async Task<ColumnDisplayModel> Create(Method m)
        {
            if (m == null) throw new ArgumentNullException("m");
            return new ColumnDisplayModel()
            {
                Header = await m.GetLabel(),
                Path = m.Name,
                Type = ColumnDisplayModel.ColumnType.MethodModel,
                RequestedWidth = WidthHint.Medium,
                Method = m,
            };
        }
        #endregion

        #region Create ViewModelProperties
        /// <summary>
        /// Creates a ColumnDisplayModel for a view model property or path to a property
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="kind">The requested editor kind or null if default should be used</param>
        /// <param name="vmPropPath">Path to the viewmodels property</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static ColumnDisplayModel Create(string header, ControlKind kind, string vmPropPath)
        {
            return Create(header, kind, null, WidthHint.Default, null, vmPropPath);
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a view model property or path to a property
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="kind">The requested editor kind or null if default should be used</param>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="vmPropPath">Path to the viewmodels property</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static ColumnDisplayModel Create(string header, ControlKind kind, WidthHint requestedWidth, string vmPropPath)
        {
            return Create(header, kind, null, requestedWidth, null, vmPropPath);
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a view model property or path to a property
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="kind">The requested editor kind or null if default should be used</param>
        /// <param name="requestedWidthAbsolute">Requested list column width in absolute toolkit units</param>
        /// <param name="vmPropPath">Path to the viewmodels property</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static ColumnDisplayModel Create(string header, ControlKind kind, int? requestedWidthAbsolute, string vmPropPath)
        {
            return Create(header, kind, null, WidthHint.Default, requestedWidthAbsolute, vmPropPath);
        }

        /// <summary>
        /// Creates a ColumnDisplayModel for a view model property or path to a property
        /// </summary>
        /// <param name="header">Label for the list header</param>
        /// <param name="kind">The requested editor kind or null if default should be used</param>
        /// <param name="gridPreviewKind">The requested preview kind or null if default should be used</param>
        /// <param name="requestedWidth">Requested list column width</param>
        /// <param name="requestedWidthAbsolute">Requested list column width in absolute toolkit units</param>
        /// <param name="vmPropPath">Path to the viewmodels property</param>
        /// <returns>a ready configurated ColumnDisplayModel</returns>
        public static ColumnDisplayModel Create(string header, ControlKind kind, ControlKind gridPreviewKind, WidthHint requestedWidth, int? requestedWidthAbsolute, string vmPropPath)
        {
            return new ColumnDisplayModel()
            {
                Header = header,
                Path = vmPropPath,
                Type = ColumnDisplayModel.ColumnType.ViewModelProperty,
                RequestedWidth = requestedWidth,
                RequestedWidthAbsolute = requestedWidthAbsolute,
                ControlKind = kind,
                GridPreEditKind = gridPreviewKind ?? kind,
            };
        }
        #endregion

        public ColumnType Type { get; private set; }
        public string Header { get; set; }
        public ControlKind ControlKind { get; set; }
        public ControlKind GridPreEditKind { get; set; }
        public WidthHint RequestedWidth { get; set; }
        public int? RequestedWidthAbsolute { get; set; }

        #region Column Source
        public Property Property { get; private set; }
        public App.Base.Property[] Properties { get; private set; }
        public Method Method { get; private set; }
        public string Path { get; private set; }
        public string DynamicOrderByExpression { get; private set; }
        #endregion

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
                case ColumnDisplayModel.ColumnType.ViewModelProperty:
                    var propVal = obj.GetPropertyValue<object>(this.Path);
                    val = propVal != null ? propVal.ToString() : string.Empty;
                    break;
                case ColumnDisplayModel.ColumnType.PropertyModel:
                    DataObjectViewModel objVmdl = obj;
                    IFormattedValueViewModel resultVMdl = null;
                    foreach (var current in this.Path.Split('.'))
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

        public ObservableCollection<ColumnDisplayModel> Columns { get; private set; } = new ObservableCollection<ColumnDisplayModel>();

        public GridDisplayConfiguration()
        {
        }

        private bool ContainsSummaryTag(string categoryTags)
        {
            return (categoryTags ?? String.Empty).Split(',', ' ').Contains("Summary");
        }

        private async Task BuildColumns(Zetbox.App.Base.DataType cls, IEnumerable<Property> props, IEnumerable<Method> methods, Mode mode)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            if (props == null) throw new ArgumentNullException("props");
            if (methods == null) throw new ArgumentNullException("methods");

            if (cls is ObjectClass)
            {
                // Makes no sense on CompoundObjects 
                ShowIcon = cls.ShowIconInLists;
                ShowId = cls.ShowIdInLists;
                ShowName = cls.ShowNameInLists;
            }
            this.Columns.Clear();
            foreach (var col in (await props.Select(async p => await ColumnDisplayModel.Create(mode, p)).WhenAll())
                     .Concat(await methods.Select(async m => await ColumnDisplayModel.Create(m)).WhenAll()))
            {
                this.Columns.Add(col);
            }
        }

        public async Task BuildColumns(Zetbox.App.Base.DataType cls, Mode mode, bool showMethods)
        {
            if (cls is ObjectClass)
            {
                await BuildColumns((ObjectClass)cls, mode, showMethods);
            }
            else if (cls is CompoundObject)
            {
                await BuildColumns((CompoundObject)cls, mode, showMethods);
            }
            else
            {
                throw new InvalidOperationException("Unsupported DataType");
            }
        }

        public async Task BuildColumns(Zetbox.App.Base.CompoundObject cls, Mode mode, bool showMethods)
        {
            if (cls == null) throw new ArgumentNullException("cls");

            await BuildColumns(cls, cls.Properties, showMethods ? cls.Methods.Where(m => m.IsDisplayable) : new Method[] { }, mode);
        }

        public async Task BuildColumns(Zetbox.App.Base.ObjectClass cls, Mode mode, bool showMethods)
        {
            if (cls == null) throw new ArgumentNullException("cls");

            var props = (await cls.GetAllProperties())
                .Where(p => ContainsSummaryTag(p.CategoryTags))
                .ToList();

            if (props.Count == 0)
            {
                props = (await (await cls.GetAllProperties()).Where(async p =>
                {
                    var orp = p as ObjectReferenceProperty;
                    if (orp == null) { return true; }

                    switch (await orp.RelationEnd.Parent.GetRelationType())
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
                }))
                .ToList();
            }

            var methods = (await cls.GetAllMethods())
                .Where(m => m.IsDisplayable && ContainsSummaryTag(m.CategoryTags));

            await BuildColumns(cls, props, showMethods ? methods.ToArray() : new Method[0], mode);
        }
    }
}
