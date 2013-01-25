

namespace Zetbox.Client.GUI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.Client.Presentables;
    using Zetbox.Client.Presentables.ValueViewModels;
    using Zetbox.Client.Models;
    using Zetbox.App.GUI;

    public class DialogCreator
    {
        public delegate DialogCreator Factory(IZetboxContext ctx);

        public DialogCreator(IZetboxContext ctx, IViewModelFactory mdlFactory, IFrozenContext frozenCtx)
        {
            ValueModels = new List<BaseValueViewModel>();
            DataContext = ctx;
            ViewModelFactory = mdlFactory;
            FrozenCtx = frozenCtx;
        }

        public IZetboxContext DataContext { get; private set; }
        public IViewModelFactory ViewModelFactory { get; private set; }
        public IFrozenContext FrozenCtx { get; private set; }

        public string Title { get; set; }
        public List<BaseValueViewModel> ValueModels { get; private set; }

        private static readonly Action<object[]> _doNothing = p => { };

        public void Show()
        {
            Show(_doNothing);
        }

        public void Show(Action<object[]> ok)
        {
            var dlg = ViewModelFactory.CreateViewModel<ValueInputTaskViewModel.Factory>().Invoke(DataContext, null, Title, ValueModels, ok);
            ViewModelFactory.ShowDialog(dlg);
        }
    }

    public static class DialogCreatorExtensions
    {
        public static DialogCreator AddString(this DialogCreator c, string label)
        {
            return c.AddString(label, null, null);
        }

        public static DialogCreator AddString(this DialogCreator c, string label, ControlKind requestedKind)
        {
            return c.AddString(label, null, requestedKind);
        }

        public static DialogCreator AddString(this DialogCreator c, string label, string value, ControlKind requestedKind)
        {
            if (c == null) throw new ArgumentNullException("c");

            var mdl = new ClassValueModel<string>(label, "", false, false);

            if (value != null)
                mdl.Value = value;

            var vmdl = c.ViewModelFactory.CreateViewModel<ClassValueViewModel<string>.Factory>().Invoke(c.DataContext, null, mdl);

            if (requestedKind != null)
                vmdl.RequestedKind = requestedKind;

            c.ValueModels.Add(vmdl);
            return c;
        }

        public static DialogCreator AddPassword(this DialogCreator c, string label)
        {
            if (c == null) throw new ArgumentNullException("c");

            return AddString(c, label, Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_PasswordKind.Find(c.FrozenCtx));
        }
    }
}
