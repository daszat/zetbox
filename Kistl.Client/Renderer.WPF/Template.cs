using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Projekte;
using Kistl.App.Base;
using Kistl.Client;

namespace Kistl.GUI
{
    /// <summary>
    /// The abstract entity representing a visualisation of a object
    /// </summary>
    public class Template
    {
        public string DisplayName { get; set; }
        public TemplateUsage Usage { get; set; }
        public Container VisualTree { get; set; }
        public ObjectClass Class { get; set; }
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
        private static void AddToList(IList<Visual> visuals, IList<BaseProperty> properties, string propertyName,
            string controlName, string controlDescription)
        {
            var property = (from p in properties where p.PropertyName == propertyName select p).Single();
            visuals.Add(
                new Visual()
                {
                    Property = property,
                    Control = new Control()
                    {
                        Name = controlName,
                        Description = controlDescription
                    }
                });
        }
        public static Template Create()
        {
            Kistl.API.ObjectType taskObjectType = new Kistl.API.ObjectType("Kistl.App.Projekte", "Task");
            ObjectClass tTask = Helper.ObjectClasses[taskObjectType];
            // var visuals = (from p in tTask.Properties select new Visual()).ToList();
            List<Visual> visuals = new List<Visual>();

            /*
             * AddToList(visuals, tTask.Properties, "ID", 
             *   "id", "this control displays an id");
             */

            AddToList(visuals, tTask.Properties, "Projekt",
                "fk", "this control displays the referenced Object");
            
            AddToList(visuals, tTask.Properties, "Name",
                "string", "this control displays a string");

            AddToList(visuals, tTask.Properties, "DatumVon",
                "date", "this control displays a date");
            AddToList(visuals, tTask.Properties, "DatumBis",
                "date", "this control displays a date");

            AddToList(visuals, tTask.Properties, "Aufwand",
                "number", "this control displays a number");

            return new TaskEditTemplate()
            {
                DisplayName = "TaskEditTemplate",
                Usage = TemplateUsage.EditControl,
                Class = tTask,
                VisualTree = new Container()
                {
                    Control = new Control()
                    {
                        Name = "list",
                        Description = "a simple list of the contained visuals"
                    },
                    // Property = ???
                    Children = visuals
                }
            };
        }
    }
}
