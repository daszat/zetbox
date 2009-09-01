
namespace Kistl.Client.WPF.View
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;

    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;
    using Kistl.App.GUI;
    using Kistl.Client.GUI;
    using Kistl.Client.Presentables;
    using System.Diagnostics;

    /// <summary>
    /// A <see cref="DataTemplateSelector"/> to choose the appropriate
    /// <see cref="IView"/> for a specified <see cref="PresentableModel"/>.
    /// </summary>
    public class VisualTypeTemplateSelector
        : DataTemplateSelector
    {
        /// <summary>
        /// The core method of this class. Chooses the appropriate 
        /// <see cref="IView"/> for a specified <see cref="PresentableModel"/> 
        /// according to the specified parameters.
        /// </summary>
        /// <param name="mdl">the model to display</param>
        /// <param name="controlKindClassName">which kind of view to use</param>
        /// <returns>a DataTemplate capable of displaying the specified model</returns>
        public static DataTemplate SelectTemplate(PresentableModel mdl, string controlKindClassName)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetPresentableModelDescriptor();
            if (pmd == null)
            {
                throw new ArgumentOutOfRangeException("mdl", "No matching PresentableModelDescriptor found");
            }

            ViewDescriptor visualDesc = LookupSecondaryViewDescriptor(pmd, controlKindClassName);

            DataTemplate result = new DataTemplate();
            if (visualDesc != null)
            {
                result.VisualTree = new FrameworkElementFactory(visualDesc.ControlRef.AsType(true));
            }
            return result;
        }

        private static ViewDescriptor LookupSecondaryViewDescriptor(PresentableModelDescriptor pmd, string controlKindClassName)
        {
            ViewDescriptor visualDesc;
            if (String.IsNullOrEmpty(controlKindClassName))
            {
                visualDesc = pmd.GetDefaultViewDescriptor(Toolkit.WPF);
            }
            else
            {
                var ckcInterface = new InterfaceType(Type.GetType(controlKindClassName + "," + GuiApplicationContext.Current.InterfaceAssembly, true));
                if (pmd.DefaultKind.GetInterfaceType() == ckcInterface)
                {
                    visualDesc = pmd.GetDefaultViewDescriptor(Toolkit.WPF);
                }
                else
                {
                    ControlKind controlKind = pmd.SecondaryControlKinds.Where(ck => ck.GetInterfaceType() == ckcInterface).SingleOrDefault();
                    if (controlKind == null && pmd.PresentableModelRef.Parent != null)
                    {
                        var parentDescriptor = pmd.PresentableModelRef.Parent.GetPresentableModelDescriptor();
                        if (parentDescriptor != null)
                        {
                            // recursively iterate up the inheritance tree
                            visualDesc = LookupSecondaryViewDescriptor(parentDescriptor, controlKindClassName);
                        }
                        else
                        {
                            throw new ArgumentOutOfRangeException("controlKindClassName", "Couldn't find matching controlKind");
                        }
                    }
                    else
                    {
                        visualDesc = pmd.GetViewDescriptor(Toolkit.WPF, controlKind);
                    }
                }
            }
            return visualDesc;
        }

        /// <summary>
        /// Initializes a new instance of the VisualTypeTemplateSelector class.
        /// </summary>
        public VisualTypeTemplateSelector()
        {
            RequestedType = null;
        }

        /// <summary>
        /// Gets or sets the kind of view to use. To select the default view, leave this property at its default null value.
        /// </summary>
        public VisualType? RequestedType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the kind of view to use. To select the default view, leave this property at its default null value.
        /// </summary>
        public string RequestedKind
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item == null)
            {
                return (DataTemplate)((FrameworkElement)container).FindResource("nullTemplate");
            }

            if (String.IsNullOrEmpty(RequestedKind) != (null == RequestedType))
            {
                throw new InvalidOperationException("Cannot SelectTemplate with status mismatch between RequestedValue and RequestedKind");
            }

            var model = item as PresentableModel;
            if (model != null)
            {
                return VisualTypeTemplateSelector.SelectTemplate(model, RequestedKind);
            }
            else
            {
                Trace.TraceWarning("No DataTemplate found for {0}", item);
                return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
            }
        }
    }
}
