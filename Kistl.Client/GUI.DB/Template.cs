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

    public class TaskEditTemplate : Template
    {
        private static Visual CreateVisual(IList<BaseProperty> properties, string propertyName,
            VisualType visualType, string controlDescription)
        {
            var property = (from p in properties where p.PropertyName == propertyName select p).Single();
            return new Visual()
            {
                Property = property,
                ControlType = visualType,
                Description = controlDescription
            };
        }
        public static Template Create()
        {
            Kistl.API.ObjectType taskObjectType = new Kistl.API.ObjectType("Kistl.App.Projekte", "Task");
            ObjectClass tTask = ClientHelper.ObjectClasses[taskObjectType];
            // var visuals = (from p in tTask.Properties select new Visual()).ToList();
            List<Visual> visuals = new List<Visual>();

            /* the ID property is not declared in the metadata
             * AddToList(visuals, tTask.Properties, "ID", 
             *   "id", "this control displays an id");
             */

            visuals.Add(CreateVisual(tTask.Properties, "Projekt",
                VisualType.ObjectReference, "this control displays the referenced Object"));

            visuals.Add(CreateVisual(tTask.Properties, "Name",
                VisualType.String, "this control displays a string"));

            visuals.Add(CreateVisual(tTask.Properties, "DatumVon",
                VisualType.DateTime, "this control displays a date"));
            visuals.Add(CreateVisual(tTask.Properties, "DatumBis",
                VisualType.DateTime, "this control displays a date"));

            // visuals.Add(CreateVisual(tTask.Properties, "Aufwand",
            //    "number", "this control displays a number"));

            return new TaskEditTemplate()
            {
                DisplayName = "TaskEditTemplate",
                Usage = TemplateUsage.EditControl,
                Type = taskObjectType,
                VisualTree = new Visual()
                {
                    ControlType = VisualType.PropertyGroup,
                    Description = "a simple list of the contained visuals",
                    // Property = ID ???
                    Children = visuals
                }
            };
        }
    }
}
