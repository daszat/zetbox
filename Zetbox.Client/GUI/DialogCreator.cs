

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
            ValueModels = new List<Tuple<object, BaseValueViewModel>>();
            DataContext = ctx;
            ViewModelFactory = mdlFactory;
            FrozenCtx = frozenCtx;
        }

        public IZetboxContext DataContext { get; private set; }
        public IViewModelFactory ViewModelFactory { get; private set; }
        public IFrozenContext FrozenCtx { get; private set; }

        public string Title { get; set; }
        public List<Tuple<object, BaseValueViewModel>> ValueModels { get; private set; }

        private static readonly Action<Dictionary<object, object>> _doNothing = p => { };

        public void Show()
        {
            Show(_doNothing);
        }

        public void Show(Action<Dictionary<object, object>> ok, ViewModel ownerMdl = null)
        {
            var dlg = ViewModelFactory.CreateViewModel<ValueInputTaskViewModel.Factory>().Invoke(DataContext, null, Title, ValueModels, ok);
            ViewModelFactory.ShowDialog(dlg, ownerMdl ?? ViewModelFactory.GetWorkspace(DataContext));
        }
    }

    public static class DialogCreatorExtensions
    {
        public static DialogCreator AddString(this DialogCreator c, object key, string label, string value = null, bool allowNullInput = false, bool isReadOnly = false, ControlKind requestedKind = null, ViewModelDescriptor vmdesc = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            if (key == null) throw new ArgumentNullException("key");

            var mdl = new ClassValueModel<string>(label, description, allowNullInput, isReadOnly);
            mdl.Value = value;

            BaseValueViewModel vmdl;
            if (vmdesc != null)
                vmdl = c.ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>(vmdesc).Invoke(c.DataContext, null, mdl);
            else
                vmdl = c.ViewModelFactory.CreateViewModel<StringValueViewModel.Factory>().Invoke(c.DataContext, null, mdl);

            if (requestedKind != null)
                vmdl.RequestedKind = requestedKind;

            c.ValueModels.Add(new Tuple<object, BaseValueViewModel>(key, vmdl));
            return c;
        }
        public static DialogCreator AddMultiLineString(this DialogCreator c, object key, string label, string value = null, bool allowNullInput = false, bool isReadOnly = false, ViewModelDescriptor vmdesc = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            return AddString(c, key, label, value, allowNullInput, isReadOnly, Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_MultiLineTextboxKind.Find(c.FrozenCtx), vmdesc, description);
        }

        public static DialogCreator AddPassword(this DialogCreator c, object key, string label, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            return AddString(c, key, label, requestedKind: Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_PasswordKind.Find(c.FrozenCtx), description: description);
        }

        public static DialogCreator AddTextBlock(this DialogCreator c, object key, string label, string value, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            return AddString(c, label, value, allowNullInput: true, requestedKind: Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_TextKind.Find(c.FrozenCtx), description: description);
        }

        public static DialogCreator AddDateTime(this DialogCreator c, object key, string label, DateTime? value = null, App.Base.DateTimeStyles style = App.Base.DateTimeStyles.Date, bool allowNullInput = false, bool isReadOnly = false, ControlKind requestedKind = null, ViewModelDescriptor vmdesc = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            if (key == null) throw new ArgumentNullException("key");

            var mdl = new DateTimeValueModel(label, description, allowNullInput, isReadOnly, style);
            mdl.Value = value;

            BaseValueViewModel vmdl;
            if (vmdesc != null)
                vmdl = c.ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>(vmdesc).Invoke(c.DataContext, null, mdl);
            else
                vmdl = c.ViewModelFactory.CreateViewModel<NullableDateTimePropertyViewModel.Factory>().Invoke(c.DataContext, null, mdl);

            if (requestedKind != null)
                vmdl.RequestedKind = requestedKind;

            c.ValueModels.Add(new Tuple<object, BaseValueViewModel>(key, vmdl));
            return c;
        }

        public static DialogCreator AddBool(this DialogCreator c, object key, string label, bool? value = null, bool allowNullInput = false, bool isReadOnly = false, ControlKind requestedKind = null, ViewModelDescriptor vmdesc = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            if (key == null) throw new ArgumentNullException("key");

            var mdl = new BoolValueModel(label, description, allowNullInput, isReadOnly);
            mdl.Value = value;

            BaseValueViewModel vmdl;
            if (vmdesc != null)
                vmdl = c.ViewModelFactory.CreateViewModel<NullableBoolPropertyViewModel.Factory>(vmdesc).Invoke(c.DataContext, null, mdl);
            else
                vmdl = c.ViewModelFactory.CreateViewModel<NullableBoolPropertyViewModel.Factory>().Invoke(c.DataContext, null, mdl);

            if (requestedKind != null)
                vmdl.RequestedKind = requestedKind;

            c.ValueModels.Add(new Tuple<object, BaseValueViewModel>(key, vmdl));
            return c;
        }

        public static DialogCreator AddInt(this DialogCreator c, object key, string label, int? value = null, bool allowNullInput = false, bool isReadOnly = false, ControlKind requestedKind = null, ViewModelDescriptor vmdesc = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            if (key == null) throw new ArgumentNullException("key");

            var mdl = new NullableStructValueModel<int>(label, description, allowNullInput, isReadOnly);
            mdl.Value = value;

            BaseValueViewModel vmdl;
            if (vmdesc != null)
                vmdl = c.ViewModelFactory.CreateViewModel<NullableIntPropertyViewModel.Factory>(vmdesc).Invoke(c.DataContext, null, mdl);
            else
                vmdl = c.ViewModelFactory.CreateViewModel<NullableIntPropertyViewModel.Factory>().Invoke(c.DataContext, null, mdl);

            if (requestedKind != null)
                vmdl.RequestedKind = requestedKind;

            c.ValueModels.Add(new Tuple<object, BaseValueViewModel>(key, vmdl));
            return c;
        }

        public static DialogCreator AddObjectReference(this DialogCreator c, object key, string label, Zetbox.App.Base.ObjectClass cls, IDataObject value = null, bool allowNullInput = false, bool isReadOnly = false, ControlKind requestedKind = null, ViewModelDescriptor vmdesc = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            if (key == null) throw new ArgumentNullException("key");

            var mdl = new ObjectReferenceValueModel(label, description, allowNullInput, isReadOnly, cls);
            mdl.Value = value;

            BaseValueViewModel vmdl;
            if (vmdesc != null)
                vmdl = c.ViewModelFactory.CreateViewModel<ObjectReferenceViewModel.Factory>(vmdesc).Invoke(c.DataContext, null, mdl);
            else
                vmdl = c.ViewModelFactory.CreateViewModel<ObjectReferenceViewModel.Factory>().Invoke(c.DataContext, null, mdl);

            if (requestedKind != null)
                vmdl.RequestedKind = requestedKind;

            c.ValueModels.Add(new Tuple<object, BaseValueViewModel>(key, vmdl));
            return c;
        }

        public static DialogCreator AddEnumeration(this DialogCreator c, object key, string label, Zetbox.App.Base.Enumeration enumeration, int? value = null, bool allowNullInput = false, bool isReadOnly = false, ControlKind requestedKind = null, ViewModelDescriptor vmdesc = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            if (key == null) throw new ArgumentNullException("key");

            var mdl = new EnumerationValueModel(label, description, allowNullInput, isReadOnly, enumeration);
            mdl.Value = value;

            BaseValueViewModel vmdl;
            if (vmdesc != null)
                vmdl = c.ViewModelFactory.CreateViewModel<EnumerationValueViewModel.Factory>(vmdesc).Invoke(c.DataContext, null, mdl);
            else
                vmdl = c.ViewModelFactory.CreateViewModel<EnumerationValueViewModel.Factory>().Invoke(c.DataContext, null, mdl);

            if (requestedKind != null)
                vmdl.RequestedKind = requestedKind;

            c.ValueModels.Add(new Tuple<object, BaseValueViewModel>(key, vmdl));
            return c;
        }
    }
}
