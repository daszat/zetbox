using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;
using Kistl.App.Base;

namespace Kistl.App.GUI.Hacks
{
    /// <summary>
    /// The abstract entity representing a visualisation of a object
    /// </summary>
    public static class TemplateHelper
    {

        public static Visual CreateDefaultVisualTree(IKistlContext ctx, ObjectClass @class)
        {
            var result = ctx.CreateVisual(
                VisualType.Object,
                "top level visual to display a object"
                );

            // Copy visuals to tree (base properties first)
            // TODO: later, group by implementing class and use property group
            foreach (Property bp in GetAllProperties(@class).Reverse())
            {
                result.Children.Add(ctx.CreateDefaultVisual(bp));
            }

            Visual methodResults = ctx.CreateVisual(
                VisualType.PropertyGroup,
                "list of calculated results"
                );


            // a list of all displayable methods
            var allMethods = @class.GetInheritedMethods().Concat(@class.Methods).Where(method => method.IsDisplayable);

            // all parameter-less Methods which return a value are displayed as "calculated properties"
            foreach (Method m in allMethods.Where(
                method => method.Parameter.Count == 1
                    && method.GetReturnParameter() != null))
            {
                methodResults.Children.Add(ctx.CreateDefaultVisual(m));
            }

            // only add method results if there are any
            if (methodResults.Children.Count > 0)
            {
                result.Children.Add(methodResults);
            }

            return result;
        }

        /// <returns>the list of all Properties of objectType</returns>
        private static IList<Property> GetAllProperties(ObjectClass @class)
        {
            List<Property> result = new List<Property>();
            while (@class != null)
            {
                foreach (Property p in @class.Properties)
                {
                    result.Add(p);
                }
                @class = @class.BaseObjectClass;
            }
            result.Reverse();
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
        EditControl = 0,
    }
}
