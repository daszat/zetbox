using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Kistl.App.Base;
using Kistl.App.GUI;
using Kistl.Client;

namespace Kistl.GUI.DB
{
    /// <summary>
    /// The abstract entity representing an actual visual element in the tree. 
    /// Usually displaying a single Property.
    /// </summary>
    public class Visual
    {

        /// <summary>
        /// Which visual is represented here
        /// </summary>
        public VisualType ControlType { get; set; }

        /// <summary>
        /// A short description of the utility of this visual
        /// </summary>
        public string Description { get; set; }

        #region TODO: refactor into descendents of "Visual"? Or not, since those can be combined? More research neccessary

        #region How data should be displayed

        /// <summary>
        /// if this is a container, here are the visually contained/controlled children of this Visual
        /// </summary>
        /// This needs ControlInfo.Container set
        public IList<Visual> Children { get; set; }

        /////////////////////// -- OR -- ///////////////////////

        /// <summary>
        /// If there is a runtime-variable number of descendents, a template is needed to be able to 
        /// create new Controls.
        /// </summary>
        /// This is currently unused(??) but should be used for Lists of SimpleObjects ???
        public Template ItemTemplate { get; set; }

        #endregion

        #region Which data should be displayed

        /// <summary>
        /// The Property to display
        /// </summary>
        public BaseProperty Property { get; set; }

        /////////////////////// -- OR -- ///////////////////////

        /// <summary>
        /// The Method whose result shall be displayed
        /// </summary>
        public Method Method { get; set; }

        #endregion

        #endregion

        /// <summary>
        /// Create the default Visual for a given BaseProperty
        /// </summary>
        /// Part of a Visitor&lt;BaseProperty&gt; pattern to create a Visual for a given BaseProperty
        /// <param name="p">the property to visualize</param>
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

        /// <summary>
        /// Create the default Visual for a given Method
        /// </summary>
        /// <param name="p">the method to visualize</param>
        public static Visual CreateDefaultVisual(Method method)
        {
            if (method == null)
                throw new ArgumentNullException("m", "cannot create Visual for null Method");

            BaseParameter bp = method.GetReturnParameter();

            // ignore methods without return value for now
            if (bp == null)
                return null;

            if (bp is StringParameter)
            {
                return CreateVisual(method, bp.IsList? VisualType.StringList : VisualType.String);
            }
            else if (bp is IntParameter)
            {
                return CreateVisual(method, bp.IsList ? VisualType.IntegerList : VisualType.Integer);
            }
            else if (bp is DoubleParameter)
            {
                return CreateVisual(method, bp.IsList ? VisualType.DoubleList : VisualType.Double);
            }
            else if (bp is BoolParameter)
            {
                return CreateVisual(method, bp.IsList ? VisualType.BooleanList : VisualType.Boolean);
            }
            else if (bp is DateTimeParameter)
            {
                return CreateVisual(method, bp.IsList ? VisualType.DateTimeList : VisualType.DateTime);
            }
            else if (bp is ObjectParameter)
            {
                return CreateVisual(method, bp.IsList ? VisualType.ObjectList : VisualType.ObjectReference);
            }
            else if (bp is CLRObjectParameter)
            {
                return null; // TODO: CreateVisual(method, bp.IsList ? VisualType. : VisualType.);
            }
            else
            {
                return null;
            }
        }

        private static Visual CreateVisual(Method method, VisualType returnType)
        {
            return new Visual()
            {
                ControlType = returnType,
                Description = "this control displays the results of calling a method",
                Method = method
            };
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
            return new Visual()
            {
                ControlType = VisualType.Enumeration,
                Description = "this control displays an enumeration value",
                Property = enumerationProperty
            };
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
            ObjectClass refClass = ClientHelper.ObjectClasses[backReferenceProperty.GetPropertyType()];

            if (refClass != null && refClass.IsSimpleObject)
            {
                return new Visual()
                {
                    ControlType = VisualType.SimpleObjectList,
                    Description = "Display and edit the referenced Simple Objects in place",
                    Property = backReferenceProperty,
                    ItemTemplate = Template.DefaultTemplate(backReferenceProperty.GetPropertyType())
                };
            }
            else
            {
                return new Visual()
                {
                    ControlType = VisualType.ObjectList,
                    Description = "this control displays a list of objects referencing this via a given relation",
                    Property = backReferenceProperty
                };
            }
        }

        public override string ToString()
        {
            return String.Format("Visual: {0}: {1}", ControlType, Property.PropertyName);
        }

    }

}
