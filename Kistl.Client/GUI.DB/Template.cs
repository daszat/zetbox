using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.App.Base;
using Kistl.Client;
using Kistl.API;

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
        public ObjectType Type { get; set; }

        public static Template DefaultTemplate(ObjectType objectType)
        {
            Template result = new Template()
            {
                DisplayName = objectType.Classname,
                Usage = TemplateUsage.EditControl,
                Type = objectType
            };
            result.VisualTree = new Visual()
            {
                ControlType = VisualType.Object,
                Description = "top level visual to display a object",
                Children = new List<Visual>()
            };
            ObjectClass klass = ClientHelper.ObjectClasses[result.Type];
            foreach (var p in klass.Properties)
            {
                result.VisualTree.Children.Add(Visual.CreateDefaultVisual(p));
            }
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
