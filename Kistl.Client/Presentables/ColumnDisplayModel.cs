
namespace Kistl.Client.Presentables
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
            /// References a DataObjectViewModel.ActionModelsByName[] ViewModel
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
        {
            this.Header = header;
            this.Name = name;
            this.ControlKind = kind;
            this.Type = type;
        }

        public ColumnType Type { get; set; }
        public string Header { get; set; }
        public string Name { get; set; }
        public ControlKind ControlKind { get; set; }

        public override string ToString()
        {
            return Header;
        }
    }

    public class GridDisplayConfiguration
    {
        public bool ShowId { get; set; }
        public bool ShowIcon { get; set; }
        public bool ShowName { get; set; }
        public IList<ColumnDisplayModel> Columns { get; set; }

        public void BuildColumns(Kistl.App.Base.ObjectClass cls)
        {
            BuildColumns(cls, false);
        }

        public void BuildColumns(Kistl.App.Base.ObjectClass cls, bool displayOnly)
        {
            if (cls == null) throw new ArgumentNullException("cls");

            ShowIcon = cls.ShowIconInLists;
            ShowId = cls.ShowIdInLists;
            ShowName = cls.ShowNameInLists;

            var group = cls.GetAllProperties()
                .Where(p => (p.CategoryTags ?? String.Empty).Split(',', ' ').Contains("Summary"));
            if (group.Count() == 0)
            {
                group = cls.GetAllProperties().Where(p =>
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
                            return false; // something went wrong
                    }
                });
            }

            this.Columns = group
                .Select(p => new ColumnDisplayModel()
                {
                    Header = p.Name,
                    Name = p.Name,
                    ControlKind = displayOnly ? p.ValueModelDescriptor.GetDefaultGridCellDisplayKind() : p.ValueModelDescriptor.GetDefaultGridCellKind()
                })
                .ToList();
        }
    }
}
