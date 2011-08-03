
namespace Kistl.Client.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Kistl.API;
    using Kistl.API.Utils;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.Presentables;
    using Kistl.Client.Presentables.ValueViewModels;
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
            /// References a ViewModels calculaded property
            /// </summary>
            CalculatedProperty,
            /// <summary>
            /// References a DataObjectViewModel.ActionViewModelsByName[] ViewModel
            /// </summary>
            MethodModel,
        }

        public ColumnDisplayModel()
            : this(string.Empty, string.Empty, null, ColumnType.PropertyModel)
        {
        }

        public ColumnDisplayModel(string header, string name)
            : this(header, name, null, ColumnType.PropertyModel)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind)
            : this(header, name, kind, ColumnType.PropertyModel)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind, int requestedWidth)
            : this(header, name, kind, null, ColumnType.PropertyModel, requestedWidth)
        {
        }

        public ColumnDisplayModel(string header, string name, ColumnType type)
            : this(header, name, null, type)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind, ColumnType type)
            : this(header, name, kind, null, type, -1)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind, ControlKind gridPreviewKind)
            : this(header, name, kind, gridPreviewKind, ColumnType.PropertyModel, -1)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind, ControlKind gridPreviewKind, ColumnType type, int requestedWidth)
        {
            this.Header = header;
            this.Name = name;
            this.ControlKind = kind;
            this.GridPreEditKind = gridPreviewKind ?? kind;
            this.Type = type;
            this.RequestedWidth = requestedWidth;
        }

        public ColumnType Type { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }
        public ControlKind ControlKind { get; set; }
        public ControlKind GridPreEditKind { get; set; }
        public int RequestedWidth { get; set; }

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
                case ColumnDisplayModel.ColumnType.CalculatedProperty:
                    val = ((IFormattedValueViewModel)obj.CalculatedPropertyModelsByName[this.Name]).FormattedValue;
                    break;
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

        public void BuildColumns(Kistl.App.Base.DataType cls, IEnumerable<Property> props, IEnumerable<Method> methods, Mode mode)
        {
            if (cls == null) throw new ArgumentNullException("cls");
            if (props == null) throw new ArgumentNullException("props");
            if (methods == null) throw new ArgumentNullException("methods");

            ShowIcon = cls.ShowIconInLists;
            ShowId = cls.ShowIdInLists;
            ShowName = cls.ShowNameInLists;

            this.Columns = new ObservableCollection<ColumnDisplayModel>(props
                .SelectMany(p => CreateColumnDisplayModels(mode, p, string.Empty, string.Empty))
                .Concat(
                    methods
                        .Select(m => new ColumnDisplayModel()
                        {
                            Header = m.GetLabel(),
                            Name = m.Name,
                            Type = ColumnDisplayModel.ColumnType.MethodModel
                        })
                    )
                .ToList());
            this.Columns.CollectionChanged += (s,e) => OnUpdate();
        }

        public void BuildColumns(Kistl.App.Base.CompoundObject cls, Mode mode)
        {
            BuildColumns(cls, cls.Properties, cls.Methods.Where(m => m.IsDisplayable), mode);
        }

        public void BuildColumns(Kistl.App.Base.ObjectClass cls, Mode mode, bool showMethods)
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

        public static List<ColumnDisplayModel> CreateColumnDisplayModels(Mode mode, Property p, string parentLabel, string parentProp)
        {
            if (p == null) throw new ArgumentNullException("p");

            var result = new List<ColumnDisplayModel>();
            var lb = p.GetLabel();

            var colMdl = new ColumnDisplayModel()
            {
                Header = parentLabel + lb,
                Name = parentProp + p.Name,
            };
            switch (mode)
            {
                case Mode.ReadOnly:
                    colMdl.ControlKind = p.ValueModelDescriptor.GetDefaultGridCellDisplayKind();
                    colMdl.GridPreEditKind = p.ValueModelDescriptor.GetDefaultGridCellDisplayKind();
                    break;
                case Mode.Editable:
                    colMdl.ControlKind = p.ValueModelDescriptor.GetDefaultGridCellEditorKind();
                    colMdl.GridPreEditKind = p.ValueModelDescriptor.GetDefaultGridCellPreEditorKind();
                    break;
            }
            result.Add(colMdl);
            return result;
        }

        protected void OnUpdate()
        {
            var temp = Update;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public event EventHandler Update;
    }
}
