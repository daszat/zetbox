
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
        /// <param name="visualType">which kind of view to use</param>
        /// <param name="readOnly">whether or not the view should be read-only</param>
        /// <returns>a DataTemplate capable of displaying the specified model</returns>
        public static DataTemplate SelectTemplate(PresentableModel mdl, VisualType? visualType, bool readOnly)
        {
            PresentableModelDescriptor pmd = mdl.GetType().ToRef(FrozenContext.Single).GetPresentableModelDescriptor();

            var visualDesc = visualType.HasValue 
                ? pmd.GetViewDescriptor(Toolkit.WPF, visualType.Value, readOnly) 
                : pmd.GetDefaultViewDescriptor(Toolkit.WPF, readOnly);

            DataTemplate result = new DataTemplate();
            if (visualDesc != null)
            {
                result.VisualTree = new FrameworkElementFactory(visualDesc.ControlRef.AsType(true));
            }
            return result;
        }

        /// <summary>
        /// Initializes a new instance of the VisualTypeTemplateSelector class.
        /// </summary>
        public VisualTypeTemplateSelector()
        {
            RequestedType = null;
            ReadOnly = false;
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
        /// Gets or sets a value indicating whether read-only views should be preferred.
        /// </summary>
        public bool ReadOnly
        {
            get;
            set;
        }

        /// <inheritdoc/>
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var model = item as PresentableModel;
            if (model != null)
            {
                return VisualTypeTemplateSelector.SelectTemplate(model, this.RequestedType, ReadOnly);
            }
            else
            {
                return (DataTemplate)((FrameworkElement)container).FindResource("emptyTemplate");
            }
        }
    }
}
