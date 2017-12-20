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
    using Zetbox.App.Extensions;
    using Zetbox.Generator.Extensions;

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
            var qry = ctx.GetQuery<DataType>();
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }

        protected virtual IQueryable<ObjectClass> GetObjectClasses(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<ObjectClass>();
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }

        protected virtual IQueryable<ObjectClass> GetBaseClasses(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass == null);
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }

        protected virtual IQueryable<ObjectClass> GetDerivedClasses(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<ObjectClass>().Where(cls => cls.BaseObjectClass != null);
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }

        protected virtual IQueryable<ObjectClass> GetDerivedTPTClasses(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<ObjectClass>()
                .Where(cls => cls.BaseObjectClass != null)
                .ToList()
                .Where(cls => cls.GetTableMapping() == TableMapping.TPT)
                .AsQueryable();
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }


        protected virtual IQueryable<CompoundObject> GetCompoundObjects(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<CompoundObject>();
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }

        protected virtual IQueryable<Relation> GetRelations(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<Relation>();
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }

        protected virtual IEnumerable<Relation> GetRelationsWithSeparateStorage(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<Relation>()
                .Where(r => r.Storage == StorageType.Separate);
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }

        protected virtual IEnumerable<Relation> GetRelationsWithoutSeparateStorage(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<Relation>()
                .Where(r => r.Storage != StorageType.Separate);
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry
                .ToList()
                .OrderBy(r => r.GetAssociationName());
        }

        protected virtual IQueryable<ValueTypeProperty> GetValueTypeProperties(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<ValueTypeProperty>();
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }

        protected virtual IQueryable<CompoundObjectProperty> GetCompoundObjectProperties(IZetboxContext ctx)
        {
            var qry = ctx.GetQuery<CompoundObjectProperty>();
            if (IsFallback)
                qry = qry.ToArray().Where(i => Compiler.FallbackModules.Contains(i.Module.Name)).AsQueryable();
            return qry;
        }
    }
}
