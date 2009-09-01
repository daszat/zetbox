//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//using Kistl.App.Base;
//using Kistl.API;

//namespace Kistl.App.GUI.Hacks
//{
//    /// <summary>
//    /// The abstract entity representing an actual visual element in the tree. 
//    /// Usually displaying a single Property.
//    /// </summary>
//    public static class VisualHelper
//    {

//        public static Visual CreateVisual(this IKistlContext ctx, ControlKindClass type, string description)
//        {
//            Visual result = ctx.Create<Visual>();
//            result.Description = description;
//            return result;
//        }

//        public static Visual CreateVisual(this IKistlContext ctx, ControlKindClass type, string description, Method m)
//        {
//            Visual result = ctx.CreateVisual(type, description);
//            result.Method = m;
//            return result;
//        }

//        public static Visual CreateVisual(this IKistlContext ctx, ControlKindClass type, string description, Property p)
//        {
//            Visual result = ctx.CreateVisual(type, description);
//            result.Property = p;
//            return result;
//        }

//        /// <summary>
//        /// Create the default <see cref="Visual"/> for a given <see cref="Property"/>
//        /// </summary>
//        /// Part of a Visitor&lt;<see cref="Property"/>&gt; pattern to create a <see cref="Visual"/> for a given <see cref="Property"/>
//        /// <param name="ctx">the context used to create the <see cref="Visual"/></param>
//        /// <param name="p">the <see cref="Property"/> to visualize</param>
//        public static Visual CreateDefaultVisual(this IKistlContext ctx, Property p)
//        {
//            if (p is BoolProperty)
//            {
//                return CreateVisual(ctx, (BoolProperty)p);
//            }
//            else if (p is DateTimeProperty)
//            {
//                return CreateVisual(ctx, (DateTimeProperty)p);
//            }
//            else if (p is DoubleProperty)
//            {
//                return CreateVisual(ctx, (DoubleProperty)p);
//            }
//            else if (p is EnumerationProperty)
//            {
//                return CreateVisual(ctx, (EnumerationProperty)p);
//            }
//            else if (p is IntProperty)
//            {
//                return CreateVisual(ctx, (IntProperty)p);
//            }
//            else if (p is ObjectReferenceProperty)
//            {
//                return CreateVisual(ctx, (ObjectReferenceProperty)p);
//            }
//            else if (p is StringProperty)
//            {
//                return CreateVisual(ctx, (StringProperty)p);
//            }
//            else if (p is ValueTypeProperty)
//            {
//                return CreateVisual(ctx, (ValueTypeProperty)p);
//            }
//            else
//            {
//                throw new InvalidCastException(
//                    String.Format("Found unknown Property Type, when trying to create Default Visual: {0}",
//                        p.GetInterfaceType()));
//            }
//        }

//        /// <summary>
//        /// Create the default Visual for a given Method
//        /// </summary>
//        /// <param name="p">the method to visualize</param>
//        public static Visual CreateDefaultVisual(this IKistlContext ctx, Method method)
//        {
//            if (method == null)
//                throw new ArgumentNullException("method", "cannot create Visual for null Method");

//            BaseParameter bp = method.GetReturnParameter();

//            VisualType? vt = null;

//            if (bp == null)
//            {
//                // Method without return parameter becomes MenuItem
//                vt = VisualType.MenuItem;
//            }
//            else if (bp is StringParameter)
//            {
//                vt = bp.IsList ? VisualType.StringList : VisualType.String;
//            }
//            else if (bp is IntParameter)
//            {
//                vt = bp.IsList ? VisualType.IntegerList : VisualType.Integer;
//            }
//            else if (bp is DoubleParameter)
//            {
//                vt = bp.IsList ? VisualType.DoubleList : VisualType.Double;
//            }
//            else if (bp is BoolParameter)
//            {
//                vt = bp.IsList ? VisualType.BooleanList : VisualType.Boolean;
//            }
//            else if (bp is DateTimeParameter)
//            {
//                vt = bp.IsList ? VisualType.DateTimeList : VisualType.DateTime;
//            }
//            else if (bp is ObjectParameter)
//            {
//                vt = bp.IsList ? VisualType.ObjectList : VisualType.ObjectReference;
//            }
//            else if (bp is CLRObjectParameter)
//            {
//                return null; // TODO: CreateVisual(method, bp.IsList ? VisualType.??? : VisualType.??? );
//            }

//            if (vt.HasValue)
//                return CreateVisual(ctx, vt.Value, "this control displays the results of calling a method", method);
//            else
//                return null;
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, ValueTypeProperty valueTypeProperty)
//        {
//            throw new NotImplementedException();
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, StringProperty stringProperty)
//        {
//            if (stringProperty.IsList)
//            {
//                return ctx.CreateVisual(
//                    VisualType.StringList,
//                    "this control displays a list of strings",
//                    stringProperty
//                    );
//            }
//            else
//            {
//                return ctx.CreateVisual(
//                    VisualType.String,
//                    "this control displays a string",
//                    stringProperty
//                    );
//            }
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, ObjectReferenceProperty objectReferenceProperty)
//        {
//            // TODO: remove this hack, Properties should know which Visual they want as default
//            // recognize the Template.VisualTree property and create a modified DefaultVisual
//            if (objectReferenceProperty.ID == 155)
//            {
//                return ctx.CreateVisual(
//                    VisualType.TemplateEditor,
//                    "edit the VisualTree of a template",
//                    objectReferenceProperty
//                    );
//            }
//            else if (objectReferenceProperty.IsList)
//            {
//                return ctx.CreateVisual(
//                    VisualType.ObjectList,
//                    "display a list of objects",
//                    objectReferenceProperty
//                    );
//            }
//            else
//            {
//                return ctx.CreateVisual(
//                    VisualType.ObjectReference,
//                    "this control displays a foreign key reference",
//                    objectReferenceProperty
//                    );
//            }
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, IntProperty intProperty)
//        {
//            return ctx.CreateVisual(
//                VisualType.Integer,
//                "this control displays a integer",
//                intProperty
//                );
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, EnumerationProperty enumerationProperty)
//        {
//            return ctx.CreateVisual(
//                VisualType.Enumeration,
//                "this control displays an enumeration value",
//                enumerationProperty
//                );
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, DoubleProperty doubleProperty)
//        {
//            return ctx.CreateVisual(
//                VisualType.Double,
//                "this control displays a double",
//                doubleProperty
//                );
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, DateTimeProperty dateTimeProperty)
//        {
//            return ctx.CreateVisual(
//                VisualType.DateTime,
//                "this control displays a date and time",
//                dateTimeProperty
//                );
//        }

//        private static Visual CreateVisual(this IKistlContext ctx, BoolProperty boolProperty)
//        {
//            return ctx.CreateVisual(
//                VisualType.Boolean,
//                "this control displays a boolean",
//                boolProperty
//                );
//        }

//        // TODO: Move to Kistl.App.GUI.Visual.ToString
//        //public override string ToString()
//        //{
//        //    return String.Format("Visual: {0}: {1}", ControlType, Property.PropertyName);
//        //}

//    }

//}
