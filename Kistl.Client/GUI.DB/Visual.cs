using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.App.Base;

namespace Kistl.GUI.DB
{
    /// <summary>
    /// The abstract entity representing an actual visual element in the tree. 
    /// Usually displaying a single Property.
    /// </summary>
    public class Visual
    {
        /// <summary>
        /// The Property to display
        /// </summary>
        public BaseProperty Property { get; set; }

        /// <summary>
        /// Which visual is represented here
        /// TODO: should become a enum or similar
        /// </summary>
        public VisualType ControlType { get; set; }

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
        public IList<Visual> Children { get; set; }

        /// <summary>
        /// Create the default Visual for a given BaseProperty
        /// </summary>
        /// Part of a Visitor&lt;BaseProperty&gt; pattern to create a Visual for a given BaseProperty
        /// <param name="p">the property to visualize</param>
        /// <returns></returns>
        public static Visual CreateDefaultVisual(BaseProperty p)
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
                        p.GetType()));
            }
        }

        private static Visual CreateVisual(ValueTypeProperty valueTypeProperty)
        {
            throw new NotImplementedException();
        }

        private static Visual CreateVisual(StringProperty stringProperty)
        {
            if (stringProperty.IsList)
            {
                return new Visual()
                {
                    ControlType = VisualType.StringList,
                    Description = "this control displays a list of strings",
                    Property = stringProperty,
                };
            }
            else
            {
                return new Visual()
                {
                    ControlType = VisualType.String,
                    Description = "this control displays a string",
                    Property = stringProperty,
                };
            }
        }

        private static Visual CreateVisual(ObjectReferenceProperty objectReferenceProperty)
        {
            if (objectReferenceProperty.IsList)
            {
                return new Visual()
                {
                    ControlType = VisualType.ObjectList,
                    Description = "display a list of objects",
                    Property = objectReferenceProperty
                };
            }
            else
            {
                return new Visual()
                {
                    ControlType = VisualType.ObjectReference,
                    Description = "this control displays a foreign key reference",
                    Property = objectReferenceProperty
                };
            }
        }

        private static Visual CreateVisual(IntProperty intProperty)
        {
            return new Visual()
            {
                ControlType = VisualType.Integer,
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
                ControlType = VisualType.Double,
                Description = "this control displays a double",
                Property = doubleProperty
            };
        }

        private static Visual CreateVisual(DateTimeProperty dateTimeProperty)
        {
            return new Visual()
            {
                ControlType = VisualType.DateTime,
                Description = "this control displays a date and time",
                Property = dateTimeProperty
            };
        }

        private static Visual CreateVisual(BoolProperty boolProperty)
        {
            return new Visual()
            {
                ControlType = VisualType.Boolean,
                Description = "this control displays a boolean",
                Property = boolProperty
            };
        }

        private static Visual CreateVisual(BackReferenceProperty backReferenceProperty)
        {
            return new Visual()
            {
                ControlType = VisualType.ObjectList,
                Description = "this control displays a list of objects referencing this via a given relation",
                Property = backReferenceProperty
            };
        }

    }

    public enum VisualType
    {
        // Non-Properties
        Renderer,
        Object,
        PropertyGroup,

        // Object References
        ObjectList,
        ObjectReference,

        // Normal Properties
        Boolean,
        DateTime,
        Double,
        Integer,
        String,
        StringList,
    }
}
