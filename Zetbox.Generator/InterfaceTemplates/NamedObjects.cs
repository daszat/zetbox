

namespace Kistl.Generator.InterfaceTemplates
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;
    using Kistl.App.Base;
    using Kistl.App.Extensions;

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
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger("Kistl.Generator.InterfaceTemplates.NamedObjects");

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
