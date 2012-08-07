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

namespace Zetbox.Client.Presentables.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;
    using Zetbox.Client.Presentables.ZetboxBase;
    using System.Collections.ObjectModel;
    using Zetbox.API.Utils;
    using Zetbox.App.GUI;
    using Zetbox.API.Common.GUI;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using Zetbox.Client.GUI;
    using Zetbox.App.Base;

    // No ViewModelDescriptor -> internal
    public partial class SavedListConfiguratorViewModel
    {
        private void Load()
        {
            Parent.FilterList.ResetUserFilter();
            if (_selectedItem != null)
            {
                // Filter
                foreach (var f in _selectedItem.Object.Filter)
                {
                    FilterModel mdl = null;
                    if (f.IsUserFilter && f.Properties != null)
                    {
                        var props = f.Properties.Select(i => FrozenContext.FindPersistenceObject<Property>(i)).ToList();
                        mdl = FilterModel.FromProperty(DataContext, FrozenContext, props);
                        Parent.FilterList.AddFilter(mdl, true, props);
                    }
                    else if(!f.IsUserFilter && !string.IsNullOrEmpty(f.Expression))
                    {
                        mdl = Parent.FilterList.Filter.FirstOrDefault(i => i.ValueSource.Expression == f.Expression) as FilterModel;
                    }

                    if (mdl != null)
                    {
                        int idx = 0;
                        foreach (var val in f.Values ?? new object[] { })
                        {
                            if (idx >= mdl.FilterArguments.Count) break;
                            var valueMdl = mdl.FilterArguments[idx].Value;
                            valueMdl.SetUntypedValue(ResolveUntypedValue(val, valueMdl));
                            idx++;
                        }
                    }
                }

                // Cols
                if (_selectedItem.Object.Columns != null && _selectedItem.Object.Columns.Count > 0)
                {
                    Parent.DisplayedColumns.Columns.Clear();
                    foreach (var col in _selectedItem.Object.Columns)
                    {
                        ColumnDisplayModel colMdl = null;
                        switch ((ColumnDisplayModel.ColumnType)col.Type)
                        {
                            case ColumnDisplayModel.ColumnType.PropertyModel:
                                var props = col.Properties.Select(i => FrozenContext.FindPersistenceObject<Property>(i)).ToArray();
                                colMdl = ColumnDisplayModel.Create(GridDisplayConfiguration.Mode.ReadOnly, props);
                                break;
                            case ColumnDisplayModel.ColumnType.MethodModel:
                                var m = FrozenContext.FindPersistenceObject<Method>(col.Method.Value);
                                colMdl = ColumnDisplayModel.Create(m);
                                break;
                            case ColumnDisplayModel.ColumnType.ViewModelProperty:
                                colMdl = ColumnDisplayModel.Create(col.Path, null, col.Path);
                                break;
                            default: 
                                continue;
                        }
                        colMdl.Header = col.Header;
                        if(col.ControlKind!= null)
                            colMdl.ControlKind = FrozenContext.FindPersistenceObject<ControlKind>(col.ControlKind.Value);
                        if (col.GridPreEditKind != null)
                            colMdl.GridPreEditKind = FrozenContext.FindPersistenceObject<ControlKind>(col.GridPreEditKind.Value);
                        // colMdl.RequestedWidthAbsolute = col.Width; // TODO: Not supported yet
                        Parent.DisplayedColumns.Columns.Add(colMdl);
                    }
                }
            }
        }

        private void Save(string name)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException("name");
            using (var ctx = ctxFactory())
            {
                var config = GetSavedConfig(ctx);
                var obj = ExtractConfigurationObject(config);
                var item = ExtractItem(name, obj);

                // Do the update
                item.Filter = new List<SavedListConfig.FilterConfig>();
                foreach (var fvm in Parent.FilterList.FilterListEntryViewModels)
                {
                    item.Filter.Add(new SavedListConfig.FilterConfig()
                    {
                        IsUserFilter = fvm.IsUserFilter,
                        Properties = fvm.SourceProperties != null ? fvm.SourceProperties.Select(i => i.ExportGuid).ToArray() : null,
                        Values = fvm.FilterViewModel.Arguments.Select(i => ExtractUntypedValue(i.UntypedValue)).ToArray(),
                        Expression = fvm.FilterViewModel.Filter.ValueSource.Expression,
                    });
                }

                item.Columns = new List<SavedListConfig.ColumnConfig>();
                foreach (var col in Parent.DisplayedColumns.Columns)
                {
                    item.Columns.Add(new SavedListConfig.ColumnConfig()
                    {
                        Type = (int)col.Type,
                        Header = col.Header,
                        Properties = col.Properties != null && col.Properties.Length > 0 ? col.Properties.Select(i => i.ExportGuid).ToArray() : null,
                        Method = col.Method != null ? col.Method.ExportGuid : (Guid?)null,
                        Path = col.Path,
                        ControlKind = col.ControlKind != null ? col.ControlKind.ExportGuid : (Guid?)null,
                        GridPreEditKind = col.ControlKind != null ? col.GridPreEditKind.ExportGuid : (Guid?)null,
                        Width = 0
                    });
                }

                config.Configuration = obj.ToXmlString();
                ctx.SubmitChanges();

                UpdateViewModel(name, item);
            }
        }
    }
}
