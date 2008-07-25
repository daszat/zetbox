using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;
using Kistl.App.Base;
using Kistl.Client;

namespace Kistl.GUI.DB
{
    /// <summary>
    /// The abstract entity representing a visualisation of a object
    /// </summary>
    public class Template
    {
        public string DisplayName { get; set; }
        public TemplateUsage Usage { get; set; }
        public Visual VisualTree { get; set; }
        public Type Type { get; set; }

        public static Template DefaultTemplate(Type objectType)
        {
            if (objectType == null)
                throw new ArgumentNullException("objectType", "Template.DefaultTemplate(objectType): need objectType to create Template");

            Template result = new Template()
            {
                DisplayName = objectType.Name,
                Usage = TemplateUsage.EditControl,
                Type = objectType,
                VisualTree = new Visual()
                {
                    ControlType = VisualType.Object,
                    Description = "top level visual to display a object",
                    Children = new List<Visual>()
                }
            };

            Visual methodResults = new Visual()
            {
                ControlType = VisualType.PropertyGroup,
                Description = "list of calculated results",
                Children = new List<Visual>()
            };

            ObjectClass @class = ClientHelper.ObjectClasses[result.Type];
            while (@class != null)
            {
                foreach (BaseProperty p in @class.Properties)
                {
                    result.VisualTree.Children.Add(Visual.CreateDefaultVisual(p));
                }

                foreach (Method m in @class.Methods)
                {
                    Visual v = Visual.CreateDefaultVisual(m);
                    if (v != null)
                        methodResults.Children.Add(v);
                }
                @class = @class.BaseObjectClass;
            }

            if (methodResults.Children.Count > 0)
                result.VisualTree.Children.Add(methodResults);

            return result;
        }

    }

    /// <summary>
    /// the different usage scenarios a template can be used for
    /// </summary>
    public enum TemplateUsage
    {
        /// <summary>
        /// Use this Template as Control to edit a instance
        /// </summary>
        EditControl = 0
    }
}
