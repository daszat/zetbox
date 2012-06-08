using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Kistl.API;
using Kistl.App.Base;
using System.Collections;

namespace Kistl.Client.Presentables.ValueViewModels
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
