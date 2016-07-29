namespace Zetbox.Client.Presentables.ObjectEditor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.Client.Presentables;
    using Zetbox.API;

    [ViewModelDescriptor]
    public class MergeObjectsTaskViewModel : ViewModel
    {
        public new delegate MergeObjectsTaskViewModel Factory(IZetboxContext dataCtx, ViewModel parent, Zetbox.App.Base.IMergeable target, Zetbox.App.Base.IMergeable source);

        public MergeObjectsTaskViewModel(IViewModelDependencies appCtx, IZetboxContext dataCtx, ViewModel parent, Zetbox.App.Base.IMergeable target, Zetbox.App.Base.IMergeable source)
            : base(appCtx, dataCtx, parent)
        {
            Target = target;
            Source = source;
        }

        public Zetbox.App.Base.IMergeable Target { get; private set; }
        public Zetbox.App.Base.IMergeable Source { get; private set; }

        public override string Name
        {
            get { return MergeObjectsTaskViewModelResources.Name; }
        }
    }
}
