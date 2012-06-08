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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Zetbox.API;
using Zetbox.App.Base;
using System.Collections;

namespace Zetbox.Client.Presentables.ValueViewModels
{
    public static class ObjectReferenceHelper
    {
        public static List<ActionViewModel> AddActionViewModels(IList cmds, IDataObject obj, IEnumerable<Method> methods, ViewModel parent, IViewModelFactory vmdlFactory)
        {
            var result = new List<ActionViewModel>();
            var ctx = obj.Context;
            methods
                .SelectMany(m => (String.IsNullOrEmpty(m.CategoryTags) ? "Summary" : m.CategoryTags)
                                        .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => new { Category = s == "Summary" ? string.Empty : s, Method = m })) // make summary empty -> will be first, then groups
                .GroupBy(x => x.Category, x => x.Method)
                .OrderBy(group => group.Key)
                .ForEach(group =>
                {
                    var name = group.Key;
                    if (string.IsNullOrEmpty(name))
                    {
                        foreach (var m in group.OrderBy(m => m.Name))
                        {
                            var mdl = vmdlFactory.CreateViewModel<ActionViewModel.Factory>(m).Invoke(ctx, parent, obj, m);
                            cmds.Add(mdl);
                            result.Add(mdl);
                        }
                    }
                    else
                    {
                        var mdls = group.OrderBy(m => m.Name).Select(m => vmdlFactory.CreateViewModel<ActionViewModel.Factory>(m).Invoke(ctx, parent, obj, m)).ToArray();
                        result.AddRange(mdls);
                        var container = vmdlFactory
                            .CreateViewModel<ContainerCommand.Factory>()
                            .Invoke(
                                ctx,
                                parent,
                                name,
                                "",
                                mdls);
                        cmds.Add(container);
                    }
                });

            return result;
        }
    }
}
