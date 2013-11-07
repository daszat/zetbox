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
    using Zetbox.Client.Presentables.GUI;

    public class DialogCreator
    {
        public delegate DialogCreator Factory(IZetboxContext ctx);

        public DialogCreator(IZetboxContext ctx, IViewModelFactory mdlFactory, IFrozenContext frozenCtx)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");
            if (mdlFactory == null) throw new ArgumentNullException("mdlFactory");
            if (frozenCtx == null) throw new ArgumentNullException("frozenCtx");

            Items = new List<ViewModel>();
            ValueModels = new List<Tuple<object, BaseValueViewModel>>();
            AdditionalButtons = new List<Tuple<string, string, Action<Dictionary<object, object>>>>();
            DataContext = ctx;
            ViewModelFactory = mdlFactory;
            FrozenCtx = frozenCtx;
        }

        public DialogCreator(DialogCreator parent)
        {
            if (parent == null) throw new ArgumentNullException("parent");

            Parent = parent;

            Items = new List<ViewModel>();
            AdditionalButtons = new List<Tuple<string, string, Action<Dictionary<object, object>>>>();
            DataContext = parent.DataContext;
            ViewModelFactory = parent.ViewModelFactory;
            FrozenCtx = parent.FrozenCtx;
        }

        public IZetboxContext DataContext { get; private set; }
        public IViewModelFactory ViewModelFactory { get; private set; }
        public IFrozenContext FrozenCtx { get; private set; }
        public DialogCreator Parent { get; private set; }

        public string Title { get; set; }
        public string AcceptLabel { get; set; }
        public string CancelLabel { get; set; }

        public List<ViewModel> Items { get; private set; }
        public List<Tuple<object, BaseValueViewModel>> ValueModels { get; private set; }

        private static readonly Action<Dictionary<object, object>> _doNothing = p => { };

        public Action<Dictionary<object, object>> OnAcceptAction { get; set; }
        public Action OnCancelAction { get; set; }
        public List<Tuple<string, string, Action<Dictionary<object, object>>>> AdditionalButtons { get; private set; }

        public void Add(object key, ViewModel vmdl)
        {
            Items.Add(vmdl);
            AddValueModel(key, vmdl);
        }

        private void AddValueModel(object key, ViewModel vmdl)
        {
            if (vmdl is BaseValueViewModel)
            {
                if (Parent != null)
                {
                    Parent.AddValueModel(key, vmdl);
                }
                else
                {
                    ValueModels.Add(new Tuple<object, BaseValueViewModel>(key, (BaseValueViewModel)vmdl));
                }
            }
        }

        public void Show()
        {
            Show(OnAcceptAction ?? _doNothing);
        }

        public void Show(Action<Dictionary<object, object>> ok, ViewModel ownerMdl = null)
        {
            var duplicateKeys = ValueModels.GroupBy(t => t.Item1).Where(grp => grp.Count() > 1).ToList();
            if (duplicateKeys.Count > 0)
            {
                throw new InvalidOperationException(string.Format("One ore more key occures more than once: {0}", string.Join(", ", duplicateKeys.Select(grp => grp.Key))));
            }

            var dlg = ViewModelFactory.CreateViewModel<ValueInputTaskViewModel.Factory>().Invoke(DataContext, null, Title, Items, ValueModels, ok);
            dlg.SetInvokeCommandLabel(AcceptLabel.IfNullOrWhiteSpace(DialogCreatorResources.Accept));
            dlg.SetCancelCommandLabel(CancelLabel.IfNullOrWhiteSpace(DialogCreatorResources.Cancel));
            dlg.CancelCallback = OnCancelAction;
            foreach (var btn in AdditionalButtons)
            {
                dlg.AddButton(btn.Item1, btn.Item2, btn.Item3);
            }
            ViewModelFactory.ShowDialog(dlg, ownerMdl ?? ViewModelFactory.GetWorkspace(DataContext));
        }
    }

    public static class DialogCreatorExtensions
    {
        #region Value input
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

            c.Add(key, vmdl);
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

        public static DialogCreator AddTextBlock(this DialogCreator c, object key, string labelOrText, string value = null, string description = null)
        {
            if (c == null) throw new ArgumentNullException("c");
            if (string.IsNullOrWhiteSpace(value))
            {
                var vmdl = c.ViewModelFactory.CreateViewModel<TextViewModel.Factory>().Invoke(c.DataContext, null, labelOrText);
                c.Add(key, vmdl);
                return c;
            }
            else
            {
                return AddString(c, key, labelOrText, value, allowNullInput: true, requestedKind: Zetbox.NamedObjects.Gui.ControlKinds.Zetbox_App_GUI_TextKind.Find(c.FrozenCtx), description: description);
            }
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

            c.Add(key, vmdl);
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

            c.Add(key, vmdl);
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

            c.Add(key, vmdl);
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

            c.Add(key, vmdl);
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

            c.Add(key, vmdl);
            return c;
        }
        #endregion

        #region Panels
        public static DialogCreator AddGroupBox(this DialogCreator c, object key, string header, Action<DialogCreator> children)
        {
            var sub = new DialogCreator(c);
            children(sub);
            var vmdl = c.ViewModelFactory.CreateViewModel<GroupBoxViewModel.Factory>().Invoke(c.DataContext, null, header, sub.Items);
            c.Add(key, vmdl);
            return c;
        }

        public static DialogCreator AddTabControl(this DialogCreator c, object key, string header, Action<DialogCreator> children)
        {
            var sub = new DialogCreator(c);
            children(sub);
            var vmdl = c.ViewModelFactory.CreateViewModel<TabControlViewModel.Factory>().Invoke(c.DataContext, null, header, sub.Items);
            c.Add(key, vmdl);
            return c;
        }

        public static DialogCreator AddTabItem(this DialogCreator c, object key, string header, Action<DialogCreator> children)
        {
            var sub = new DialogCreator(c);
            children(sub);
            var vmdl = c.ViewModelFactory.CreateViewModel<TabItemViewModel.Factory>().Invoke(c.DataContext, null, header, sub.Items);
            c.Add(key, vmdl);
            return c;
        }
        #endregion

        #region Buttons
        public static DialogCreator YesNo(this DialogCreator c)
        {
            c.AcceptLabel = DialogCreatorResources.Yes;
            c.CancelLabel = DialogCreatorResources.No;

            return c;
        }

        public static DialogCreator DefaultButtons(this DialogCreator c, string acceptLabel, string cancelLabel)
        {
            c.AcceptLabel = acceptLabel;
            c.CancelLabel = cancelLabel;

            return c;
        }

        public static DialogCreator AddButton(this DialogCreator c, string label, Action<Dictionary<object, object>> action, string tooltip = null)
        {
            c.AdditionalButtons.Add(new Tuple<string, string, Action<Dictionary<object, object>>>(label, tooltip, action));
            return c;
        }

        public static DialogCreator OnAccept(this DialogCreator c, Action<Dictionary<object, object>> action)
        {
            c.OnAcceptAction = action;
            return c;
        }

        public static DialogCreator OnCancel(this DialogCreator c, Action action)
        {
            c.OnCancelAction = action;
            return c;
        }
        #endregion
    }
}
