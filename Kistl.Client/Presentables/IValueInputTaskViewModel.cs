namespace Kistl.Client.Presentables
{
    using System;
    using Kistl.Client.Presentables.ValueViewModels;
    using System.Linq;
    using System.Collections.Generic;

    public interface IValueInputTaskViewModel
    {
        void Cancel();
        ICommandViewModel CancelCommand { get; }
        void Invoke();
        ICommandViewModel InvokeCommand { get; }
        string Name { get; }
        IEnumerable<BaseValueViewModel> ValueViewModels { get; }
        ILookup<string, BaseValueViewModel> ValueViewModelsByName { get; }
    }
}
