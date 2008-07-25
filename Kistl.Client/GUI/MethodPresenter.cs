using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;
using System.Reflection;
using System.ComponentModel;
using Kistl.API;
using System.Collections.ObjectModel;
using System.Collections;

namespace Kistl.GUI
{

    /// <summary>
    /// The default presenter has infrastructure for setting IBasicControl and IValueControl members.
    /// Additionally basic change handling is implemented.
    /// </summary>
    /// <typeparam name="TYPE">The type of the handled Method's value.</typeparam>
    /// <typeparam name="CONTROL">The type of the actual control which is used for display.</typeparam>
    public class DefaultMethodPresenter<TYPE> : Presenter<IValueControl<TYPE>>
    {
        protected Method Method { get { return Preferences.Method; } }

        protected virtual TYPE ExecuteMethod()
        {
            return (TYPE)ExecuteMethodImpl();
        }

        protected object ExecuteMethodImpl()
        {
            MethodInfo mi = Object.GetType().GetMethods().Single(info => info.Name == Method.MethodName);
            return mi.Invoke(Object, new object[] { });
        }

        #region Initialisation

        /// <summary>
        /// Setup the Control with default values from the Property. 
        /// Install EventHandlers for UserInput and PropertyChanged.
        /// </summary>
        protected override void InitializeComponent()
        {
            // TODO: Control.IsReadOnly = true;

            Control.ShortLabel = Method.MethodName;
            Control.Description = String.Format("{0} {1}.{2}() from {3}",
                Method.Parameter.Single(p => p.IsReturnParameter),
                Method.ObjectClass.ClassName,
                Method.MethodName,
                Method.Module
                );

            // To prevent resource leaks, all event handlers have to be removed
            // See DisposeManagedResources()
            {
                _Object_PropertyChanged = new System.ComponentModel.PropertyChangedEventHandler(Object_PropertyChanged);
                Object.PropertyChanged += _Object_PropertyChanged;
            }

            Control.Value = ExecuteMethod();
            Control.IsValidValue = true;

            // Control.Size = Preferences.PreferredSize;
            Control.Size = FieldSize.Full;
        }

        #endregion

        #region Change Management

        private PropertyChangedEventHandler _Object_PropertyChanged = null;

        private void Object_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            using (TraceClient.TraceHelper.TraceMethodCall())
            {
                if (sender != Object)
                    throw new InvalidOperationException(String.Format("Resource Leak: _Object_PropertyChanged in '{0}' was called by '{1}', but should be attached to '{2}'",
                        this, sender, Object));

                // if the object has changed, unconditionally overwrite the value in the GUI
                Control.Value = ExecuteMethod();
            }
        }

        #endregion

        #region Disposal

        protected override void DisposeManagedResources()
        {
            base.DisposeManagedResources();

            // To prevent resource leaks, all event handlers have to be removed
            if (Object != null)
                Object.PropertyChanged -= _Object_PropertyChanged;
        }

        #endregion
    }

    public class ObjectMethodPresenter : DefaultMethodPresenter<IDataObject>
    {
        protected override void InitializeComponent()
        {
            ((IReferenceControl)Control).ObjectType = Method.GetReturnParameter().GetType();
            ((IReferenceControl)Control).ItemsSource = Object.Context.GetQuery(Method.GetReturnParameter().GetType()).ToList();
            base.InitializeComponent();
        }
    }

    public class ObjectListMethodPresenter : DefaultMethodPresenter<ObservableCollection<IDataObject>>
    {
        protected override void InitializeComponent()
        {
            ((IReferenceListControl)Control).ObjectType = Method.GetReturnParameter().GetType();
            ((IReferenceListControl)Control).ItemsSource = Object.Context.GetQuery(Method.GetReturnParameter().GetType()).ToList();
            base.InitializeComponent();
        }

        protected override ObservableCollection<IDataObject> ExecuteMethod()
        {
            ObservableCollection<IDataObject> result = new ObservableCollection<IDataObject>();

            foreach (object o in (IList)base.ExecuteMethodImpl())
            {
                result.Add((IDataObject)o);
            }

            return result;
        }
    }

}
