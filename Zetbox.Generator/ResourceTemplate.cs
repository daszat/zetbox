// This file is part of zetbox.
//
// Zetbox is free software: you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as
// published by the Free Software Foundation, either version 3 of
// the License, or (at your option) any later version.
//
// Zetbox is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU Lesser General Public
// License along with zetbox.  If not, see <http://www.gnu.org/licenses/>.

namespace Zetbox.Generator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Zetbox.API;
    using Zetbox.App.Base;

    public class ResourceTemplate
        : CodeTemplate
    {
        public ResourceTemplate(IGenerationHost host)
            : base(host)
        {
        }

        public override void Generate()
        {
        }

        /// <summary>
        /// An implementation suffix for properties. This is used in the QueryTranslator.
        /// </summary>
        public string ImplementationPropertySuffix
        {
            get { return Zetbox.API.Helper.ImplementationSuffix; }
        }

        /// <summary>
        /// An implementation suffix for classes. This is only used in the local type transformations
        /// </summary>
        public string ImplementationSuffix
        {
            get { return this.Settings["extrasuffix"] + Zetbox.API.Helper.ImplementationSuffix; }
        }

        public string ImplementationNamespace
        {
            get { return this.Settings["implementationnamespace"]; }
        }

        public string[] RequiredNamespaces
        {
            get
            {
                var namespaces = this.Settings["namespaces"];
                if (String.IsNullOrEmpty(namespaces))
                {
                    return new string[0];
                }
                else
                {
                    return namespaces.Split(',');
                }
            }
        }

        protected string ResolveResourceUrl(string template)
        {
            return String.Format("res://{0}/{1}.{2}", Settings["providertemplateassembly"], Settings["providertemplatenamespace"], template);
        }

        /// <summary>
        /// If someone finds a better implementation, let me now
        /// </summary>
        /// <param name="text">a text to XML Encode</param>
        /// <returns>xml encoded string</returns>
        protected string UglyXmlEncode(string text)
        {
            if (text == null) return null;
            return text.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
        }

        public bool IsFallback
        {
            get
            {
                return "true".Equals(Settings["isfallback"], StringComparison.CurrentCultureIgnoreCase);
            }
        }

        protected virtual IQueryable<DataType> GetDataTypes(IZetboxContext ctx)
        {
            if (!IsFallback)
                return ctx.GetQuery<DataType>();
            else
                return ctx.GetQuery<DataType>().Where(i => Compiler.FallbackModules.Contains(i.Module.Name));
        }

        protected virtual IQueryable<ObjectClass> GetObjectClasses(IZetboxContext ctx)
        {
            if (!IsFallback)
                return ctx.GetQuery<ObjectClass>();
            else
                return ctx.GetQuery<ObjectClass>().Where(i => Compiler.FallbackModules.Contains(i.Module.Name));
        }

        protected virtual IQueryable<CompoundObject> GetCompoundObjects(IZetboxContext ctx)
        {
            if (!IsFallback)
                return ctx.GetQuery<CompoundObject>();
            else
                return ctx.GetQuery<CompoundObject>().Where(i => Compiler.FallbackModules.Contains(i.Module.Name));
        }

        protected virtual IQueryable<Relation> GetRelations(IZetboxContext ctx)
        {
            if (!IsFallback)
                return ctx.GetQuery<Relation>();
            else
                return ctx.GetQuery<Relation>().Where(i => Compiler.FallbackModules.Contains(i.Module.Name));
        }

        protected virtual IQueryable<ValueTypeProperty> GetValueTypeProperties(IZetboxContext ctx)
        {
            if (!IsFallback)
                return ctx.GetQuery<ValueTypeProperty>();
            else
                return ctx.GetQuery<ValueTypeProperty>().Where(i => Compiler.FallbackModules.Contains(i.Module.Name));
        }

        protected virtual IQueryable<CompoundObjectProperty> GetCompoundObjectProperties(IZetboxContext ctx)
        {
            if (!IsFallback)
                return ctx.GetQuery<CompoundObjectProperty>();
            else
                return ctx.GetQuery<CompoundObjectProperty>().Where(i => Compiler.FallbackModules.Contains(i.Module.Name));
        }
    }
}
