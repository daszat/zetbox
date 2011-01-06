
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

        public ColumnDisplayModel(string header, string name, ColumnType type)
            : this(header, name, null, type)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind, ColumnType type)
            : this(header, name, kind, null, type)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind, ControlKind gridPreviewKind)
            : this(header, name, kind, gridPreviewKind, ColumnType.PropertyModel)
        {
        }

        public ColumnDisplayModel(string header, string name, ControlKind kind, ControlKind gridPreviewKind, ColumnType type)
        {
            this.Header = header;
            this.Name = name;
            this.ControlKind = kind;
            this.GridPreEditKind = gridPreviewKind ?? kind;
            this.Type = type;
        }

        public ColumnType Type { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }
        public ControlKind ControlKind { get; set; }
        public ControlKind GridPreEditKind { get; set; }

        public override string ToString()
        {
            return Header;
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

        public IList<ColumnDisplayModel> Columns { get; set; }

        public GridDisplayConfiguration()
        {
        }

        public void BuildColumns(Kistl.App.Base.ObjectClass cls, Mode mode, bool showMethods)
        {
            if (cls == null) throw new ArgumentNullException("cls");

            ShowIcon = cls.ShowIconInLists;
            ShowId = cls.ShowIdInLists;
            ShowName = cls.ShowNameInLists;

            var props = cls.GetAllProperties()
                .Where(p => (p.CategoryTags ?? String.Empty).Split(',', ' ').Contains("Summary"));
            if (props.Count() == 0)
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
                });
            }

            var methods = cls.GetAllMethods()
                .Where(m => m.IsDisplayable && (m.CategoryTags ?? String.Empty).Split(',', ' ').Contains("Summary"));

            this.Columns = props.SelectMany(p => CreateColumnDisplayModels(mode, p, string.Empty, string.Empty)).ToList();

            if (showMethods)
            {
                this.Columns = this.Columns.Concat(
                    methods
                    .Select(m => new ColumnDisplayModel()
                    {
                        Header = m.GetLabel(),
                        Name = m.Name,
                        Type = ColumnDisplayModel.ColumnType.MethodModel
                    })
                ).ToList();
            }
        }

        private List<ColumnDisplayModel> CreateColumnDisplayModels(Mode mode, Property p, string parentLabel, string parentProp)
        {
            var result = new List<ColumnDisplayModel>();
            var lb = p.GetLabel();

            if (p is CompoundObjectProperty)
            {
                foreach(var i in ((CompoundObjectProperty)p).CompoundObjectDefinition.Properties)
                {
                    result.AddRange(CreateColumnDisplayModels(mode, i, parentLabel + lb + ".", parentProp + p.Name + "."));
                }
            }
            else
            {
                var colMdl = new ColumnDisplayModel()
                {
                    Header = parentLabel + lb,
                    Name = parentProp + p.Name,
                };
                switch(mode)
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
            }
            return result;
        }
    }
}
