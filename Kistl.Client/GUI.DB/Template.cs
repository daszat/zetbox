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
                Name = "object",
                Description = "top level visual to display a object",
                Children = new List<Visual>()
            };
            ObjectClass klass = ClientHelper.ObjectClasses[result.Type];
            foreach (var p in klass.Properties)
            {
                result.VisualTree.Children.Add(CreateDefaultVisual(p));
            }
            return result;
        }

        /// <summary>
        /// Part of a Visitor&lt;BaseProperty&gt; pattern to create a Visual for a given BaseProperty
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        private static Visual CreateDefaultVisual(BaseProperty p)
        {
            if (p is BackReferenceProperty)
            {
                return CreateVisual((BackReferenceProperty)p);
            }
            else if (p is BoolProperty)
            {
                return CreateVisual((BoolProperty)p);
            }
            else if (p is DateTimeProperty)
            {
                return CreateVisual((DateTimeProperty)p);
            }
            else if (p is DoubleProperty)
            {
                return CreateVisual((DoubleProperty)p);
            }
            else if (p is EnumerationProperty)
            {
                return CreateVisual((EnumerationProperty)p);
            }
            else if (p is IntProperty)
            {
                return CreateVisual((IntProperty)p);
            }
            else if (p is ObjectReferenceProperty)
            {
                return CreateVisual((ObjectReferenceProperty)p);
            }
            else if (p is StringProperty)
            {
                return CreateVisual((StringProperty)p);
            }
            else if (p is ValueTypeProperty)
            {
                return CreateVisual((ValueTypeProperty)p);
            }
            else
            {
                throw new InvalidCastException(
                    String.Format("Found unknown Property Type, when trying to create Default Visual: {0}",
                        p.Type));
            }
        }

        private static Visual CreateVisual(ValueTypeProperty valueTypeProperty)
        {
            throw new NotImplementedException();
        }

        private static Visual CreateVisual(StringProperty stringProperty)
        {
            return new Visual()
            {
                Name = "string",
                Description = "this control displays a string",
                Property = stringProperty
            };
        }

        private static Visual CreateVisual(ObjectReferenceProperty objectReferenceProperty)
        {
            if (objectReferenceProperty.IsList)
            {
                return new Visual()
                {
                    Name = "objectlist",
                    Description = "display a list of objects",
                    Property = objectReferenceProperty
                };
            }
            else
            {
                return new Visual()
                {
                    Name = "fk",
                    Description = "this control displays a foreign key reference",
                    Property = objectReferenceProperty
                };
            }
        }

        private static Visual CreateVisual(IntProperty intProperty)
        {
            return new Visual()
            {
                Name = "int",
                Description = "this control displays a integer",
                Property = intProperty
            };
        }

        private static Visual CreateVisual(EnumerationProperty enumerationProperty)
        {
            throw new NotImplementedException();
        }

        private static Visual CreateVisual(DoubleProperty doubleProperty)
        {
            return new Visual()
            {
                Name = "double",
                Description = "this control displays a double",
                Property = doubleProperty
            };
        }

        private static Visual CreateVisual(DateTimeProperty dateTimeProperty)
        {
            return new Visual()
            {
                Name = "date",
                Description = "this control displays a date and time",
                Property = dateTimeProperty
            };
        }

        private static Visual CreateVisual(BoolProperty boolProperty)
        {
            return new Visual()
            {
                Name = "bool",
                Description = "this control displays a boolean",
                Property = boolProperty
            };
        }

        private static Visual CreateVisual(BackReferenceProperty backReferenceProperty)
        {
            return new Visual()
            {
                Name = "list",
                Description = "this control displays a list of objects referencing this via a given relation",
                Property = backReferenceProperty
            };
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
            string controlName, string controlDescription)
        {
            var property = (from p in properties where p.PropertyName == propertyName select p).Single();
            return new Visual()
            {
                Property = property,
                Name = controlName,
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
                "fk", "this control displays the referenced Object"));

            visuals.Add(CreateVisual(tTask.Properties, "Name",
                "string", "this control displays a string"));

            visuals.Add(CreateVisual(tTask.Properties, "DatumVon",
                "date", "this control displays a date"));
            visuals.Add(CreateVisual(tTask.Properties, "DatumBis",
                "date", "this control displays a date"));

            // visuals.Add(CreateVisual(tTask.Properties, "Aufwand",
            //    "number", "this control displays a number"));

            return new TaskEditTemplate()
            {
                DisplayName = "TaskEditTemplate",
                Usage = TemplateUsage.EditControl,
                Type = taskObjectType,
                VisualTree = new Visual()
                {
                    Name = "group",
                    Description = "a simple list of the contained visuals",
                    // Property = ID ???
                    Children = visuals
                }
            };
        }
    }
}
