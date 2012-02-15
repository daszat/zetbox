using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using Kistl.API;
using Kistl.App.Base;

namespace Kistl.Client.Presentables.ValueViewModels
{
    public static class ObjectReferenceHelper
    {
        public static void AddActionViewModels(ObservableCollection<ICommandViewModel> cmds, IDataObject obj, ObjectReferenceProperty navigator, ViewModel parent, IViewModelFactory vmdlFactory)
        {
            var ctx = obj.Context;
            navigator
                .Methods
                .SelectMany(m => (String.IsNullOrEmpty(m.CategoryTags) ? string.Empty : m.CategoryTags)
                                        .Split(", ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)
                                        .Select(s => new { Category = s, Method = m }))
                .GroupBy(x => x.Category, x => x.Method)
                .OrderBy(group => group.Key)
                .ForEach(group =>
                {
                    var name = group.Key;
                    if (string.IsNullOrEmpty(name))
                    {
                        foreach (var m in group)
                        {
                            cmds.Add(vmdlFactory.CreateViewModel<ActionViewModel.Factory>(m).Invoke(ctx, parent, obj, m).ExecuteCommand);
                        }
                    }
                    else
                    {
                        var container = vmdlFactory
                            .CreateViewModel<ContainerCommand.Factory>()
                            .Invoke(
                                ctx,
                                parent,
                                name,
                                "",
                                group.Select(m => vmdlFactory.CreateViewModel<ActionViewModel.Factory>(m).Invoke(ctx, parent, obj, m).ExecuteCommand).ToArray());
                        cmds.Add(container);
                    }
                });
        }

    }
}
