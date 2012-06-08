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


namespace Zetbox.Generator.InterfaceTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.Extensions;

    internal sealed class NamedObjectDescriptor
    {
        public string Type { get; set; }
        public string Name { get; set; }
        public Guid Guid { get; set; }

        public string[] Path { get; set; }
        public string PathString { get; set; }
    }

    public partial class NamedObjects
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Zetbox.Generator.InterfaceTemplates.NamedObjects");

        private List<string> currentPrefix;
        private string currentPrefixString;
        private string indent;

        private void RenderNamedObjects()
        {
            currentPrefix = new List<string>();
            BuildCurrentPrefixString();
            indent = "        ";

            foreach (var obj in GetNamedObjects().OrderBy(no => no.PathString).ThenBy(no => no.Name))
            {
                while (!obj.PathString.StartsWith(currentPrefixString))
                {
                    Pop();
                }

                while (obj.Path.Length > currentPrefix.Count)
                {
                    Push(obj.Path[currentPrefix.Count]);
                }

                RenderObjects(obj.Type, obj.Name, obj.Guid);
            }
            while (currentPrefix.Count > 0)
            {
                Pop();
            }
        }

        private IEnumerable<NamedObjectDescriptor> GetNamedObjects()
        {
            foreach (var objClass in ctx.GetQuery<ObjectClass>().ToList().Where(cls => cls.ImplementsIModuleMember(false)))
            {
                string typeName = null;
                List<NamedObjectDescriptor> instances = null;
                try
                {
                    typeName = objClass.GetDescribedInterfaceTypeName();
                    instances = ctx.Internals().GetAll(objClass.GetDescribedInterfaceType())
                        .OfType<INamedObject>()
                        .Select(ino => new { Ino = ino, Path = ino.GetName(), Guid = ino.ExportGuid, })
                        .Where(ino => !string.IsNullOrEmpty(ino.Path))
                        .Select(ino =>
                        {
                            var splitName = ino.Path.Split('.');
                            var path = splitName.Take(splitName.Length - 1).ToArray();

                            return new NamedObjectDescriptor()
                            {
                                Type = typeName,
                                Name = splitName.Last(),
                                Path = path,
                                PathString = string.Join(".", path),
                                Guid = ino.Guid
                            };
                        })
                        .ToList();
                }
                catch (TypeLoadException ex)
                {
                    Log.WarnFormat("Skipping rendering for {0}: {1}", objClass, ex.Message);
                }

                if (instances != null)
                {
                    foreach (var nod in instances)
                    {
                        yield return nod;
                    }
                }
            }
        }

        private void BuildCurrentPrefixString()
        {
            currentPrefixString = string.Join(".", currentPrefix.ToArray());
        }

        /// <summary>
        /// pushes the specified path component onto currentPrefix, starts a new class and increases indent
        /// </summary>
        /// <param name="pathComponent"></param>
        private void Push(string pathComponent)
        {
            currentPrefix.Add(pathComponent);
            BuildCurrentPrefixString();

            WriteObjects(indent, "public static class ", pathComponent);
            WriteLine();
            WriteObjects(indent, "{");
            WriteLine();

            indent += "    ";
        }

        /// <summary>
        /// Removes the top-most pathComponent from currentPrefix, closes the current class and reduces indent
        /// </summary>
        private void Pop()
        {
            currentPrefix.RemoveAt(currentPrefix.Count - 1);
            BuildCurrentPrefixString();

            indent = indent.Substring(0, indent.Length - 4);

            WriteObjects(indent, "}");
            WriteLine();
        }

        private void RenderObjects(string type, string name, Guid guid)
        {
            if (name == currentPrefix.LastOrDefault())
            {
                WriteObjects(indent, "// Cannot render: member names cannot be the same as their enclosing type");
                WriteLine();
                WriteObjects(indent, "// public static TypedGuid<global::", type, "> ", name);
                WriteLine();
            }
            else
            {
                WriteObjects(indent, "public static TypedGuid<global::", type, "> ", name);
                WriteLine();
                WriteObjects(indent, "{");
                WriteLine();
                WriteObjects(indent, "    ", "get { return new TypedGuid<global::", type, ">(\"", guid, "\"); }");
                WriteLine();
                WriteObjects(indent, "}");
                WriteLine();
            }
        }
    }
}
