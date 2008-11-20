using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Text;
using System.CodeDom;
using System.CodeDom.Compiler;

using Kistl.App.Base;
using System.IO;
using System.Reflection;
using System.Collections;
using Kistl.API;
using System.Globalization;

namespace Kistl.Server.Generators
{
    public class BaseDataObjectGenerator
    {
        private string codeBasePath = "";

        #region Current Metadata
        public abstract class CurrentBase : ICloneable
        {
            public Kistl.API.IKistlContext ctx { get; set; }

            public TaskEnum task { get; set; }
            public CodeCompileUnit code { get; set; }
            public CodeNamespace code_namespace { get; set; }
            public CodeTypeDeclaration code_class { get; set; }
            public CodeMemberField code_field { get; set; }
            public CodeMemberProperty code_property { get; set; }

            public BaseProperty property { get; set; }

            public abstract object Clone();

            protected virtual void CloneInternal(CurrentBase result)
            {
                result.ctx = this.ctx;

                result.task = this.task;
                result.code = this.code;
                result.code_namespace = this.code_namespace;
                result.code_class = this.code_class;
                result.code_field = this.code_field;
                result.code_property = this.code_property;

                result.property = this.property;
            }
        }

        public class CurrentObjectClass : CurrentBase
        {
            public ObjectClass objClass { get; set; }
            public CodeConstructor code_constructor { get; set; }

            public override object Clone()
            {
                CurrentObjectClass result = new CurrentObjectClass();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentObjectClass)result).code_constructor = this.code_constructor;
                ((CurrentObjectClass)result).objClass = this.objClass;
            }
        }

        public class CurrentInterface : CurrentBase
        {
            public Interface @interface { get; set; }

            public override object Clone()
            {
                CurrentInterface result = new CurrentInterface();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentInterface)result).@interface = this.@interface;
            }
        }

        public class CurrentStruct : CurrentBase
        {
            public Struct @struct { get; set; }

            public override object Clone()
            {
                CurrentStruct result = new CurrentStruct();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentStruct)result).@struct = this.@struct;
            }
        }

        public class CurrentEnumeration : CurrentBase
        {
            public Enumeration enumeration { get; set; }

            public override object Clone()
            {
                CurrentEnumeration result = new CurrentEnumeration();
                CloneInternal(result);
                return result;
            }

            protected override void CloneInternal(CurrentBase result)
            {
                base.CloneInternal(result);
                ((CurrentEnumeration)result).enumeration = this.enumeration;
            }
        }
        #endregion

        #region Generate
        public virtual void Generate(Kistl.API.IKistlContext ctx, string codeBasePath)
        {
            this.codeBasePath = codeBasePath + (codeBasePath.EndsWith("\\") ? "" : "\\");
            Directory.CreateDirectory(codeBasePath);

            Directory.GetFiles(this.codeBasePath, "*.cs", SearchOption.AllDirectories).
                ToList().ForEach(f => File.Delete(f));

            var objClassList = Generator.GetObjectClassList(ctx);

            Console.Write("  Object Classes");
            foreach (ObjectClass objClass in objClassList)
            {
                Console.Write(".");
                GenerateObjectInterfacesInternal(new CurrentObjectClass() { ctx = ctx, task = TaskEnum.Interface, objClass = objClass });
                GenerateObjectsInternal(new CurrentObjectClass() { ctx = ctx, task = TaskEnum.Client, objClass = objClass });
                GenerateObjectsInternal(new CurrentObjectClass() { ctx = ctx, task = TaskEnum.Server, objClass = objClass });
            }
            Console.WriteLine();

            Console.WriteLine("  Serializer");
            GenerateObjectSerializer(TaskEnum.Server, objClassList);
            GenerateObjectSerializer(TaskEnum.Client, objClassList);


            Console.Write("  Interfaces");
            var interfaceList = Generator.GetInterfaceList(ctx);
            foreach (Interface i in interfaceList)
            {
                Console.Write(".");
                GenerateInterfacesInternal(new CurrentInterface() { ctx = ctx, task = TaskEnum.Interface, @interface = i });
            }
            Console.WriteLine();

            Console.Write("  Enums");
            var enumList = Generator.GetEnumList(ctx);
            foreach (Enumeration e in enumList)
            {
                Console.Write(".");
                GenerateEnumerationsInternal(new CurrentEnumeration() { ctx = ctx, task = TaskEnum.Interface, enumeration = e });
            }
            Console.WriteLine();

            Console.Write("  Structs");
            var structList = Generator.GetStructList(ctx);
            foreach (Struct s in structList)
            {
                Console.Write(".");
                GenerateStructInterfacesInternal(new CurrentStruct() { ctx = ctx, task = TaskEnum.Interface, @struct = s });
                GenerateStructsInternal(new CurrentStruct() { ctx = ctx, task = TaskEnum.Client, @struct = s });
                GenerateStructsInternal(new CurrentStruct() { ctx = ctx, task = TaskEnum.Server, @struct = s });
            }
            Console.WriteLine();

            Console.Write("  FrozenContext");
            GenerateFrozenContext(objClassList);

            Console.WriteLine();

            Console.WriteLine("  Assemblyinfo");
            GenerateAssemblyInfo(TaskEnum.Interface);
            GenerateAssemblyInfo(TaskEnum.Server);
            GenerateAssemblyInfo(TaskEnum.Client);

        }
        #endregion

        #region Save / Helper
        protected virtual void SaveFile(CodeCompileUnit code, string fileName)
        {
            string path = Path.GetDirectoryName(codeBasePath + fileName);
            Directory.CreateDirectory(path);

            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";
            using (StreamWriter sourceWriter = new StreamWriter(codeBasePath + fileName))
            {
                provider.GenerateCodeFromCompileUnit(
                    code, sourceWriter, options);
            }
        }

        protected virtual void AddDefaultNamespaces(CodeNamespace ns, TaskEnum task)
        {
            ns.Imports.Add(new CodeNamespaceImport("System"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.Generic"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections.ObjectModel"));
            ns.Imports.Add(new CodeNamespaceImport("System.Linq"));
            ns.Imports.Add(new CodeNamespaceImport("System.Text"));
            ns.Imports.Add(new CodeNamespaceImport("System.Collections"));
            ns.Imports.Add(new CodeNamespaceImport("System.Xml"));
            ns.Imports.Add(new CodeNamespaceImport("System.Xml.Serialization"));
            ns.Imports.Add(new CodeNamespaceImport("Kistl.API"));
        }

        #region CreateNamespace
        protected CodeNamespace CreateNamespace(CodeCompileUnit code, string name, TaskEnum task)
        {
            return CreateNamespace(code, name, true, task);
        }

        protected CodeNamespace CreateNamespace(CodeCompileUnit code, string name, bool addDefaultNamespaces, TaskEnum task)
        {
            CodeNamespace ns = new CodeNamespace(name);
            code.Namespaces.Add(ns);
            if (addDefaultNamespaces) AddDefaultNamespaces(ns, task);

            return ns;
        }
        #endregion

        #endregion

        #region GenerateAssemblyInfo
        protected virtual void GenerateAssemblyInfo(CodeCompileUnit code, TaskEnum task)
        {
            code.AddAttribute(typeof(System.Reflection.AssemblyTitleAttribute), task.GetKistObjectsName());
            code.AddAttribute(typeof(System.Reflection.AssemblyCompanyAttribute), "dasz.at");
            code.AddAttribute(typeof(System.Reflection.AssemblyProductAttribute), "Kistl");
            code.AddAttribute(typeof(System.Reflection.AssemblyCopyrightAttribute), "Copyright © dasz.at 2008");
            code.AddAttribute(typeof(System.Runtime.InteropServices.ComVisibleAttribute), false);
            code.AddAttribute(typeof(System.Reflection.AssemblyVersionAttribute), "1.0.0.0");
            code.AddAttribute(typeof(System.Reflection.AssemblyFileVersionAttribute), "1.0.0.0");
            code.AddAttribute(typeof(CLSCompliantAttribute), true);
        }

        private void GenerateAssemblyInfo(TaskEnum task)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            GenerateAssemblyInfo(code, task);

            // Generate the code & save
            SaveFile(code, string.Format(@"{0}\AssemblyInfo.cs", task.GetKistObjectsName()));
        }
        #endregion

        #region GenerateFrozenContext


        public virtual void GenerateFrozenContext_LinkObjectReferences<T>(CodeConstructor constructor)
            where T : IDataObject
        {
            char[] progress = new char[] { '-', '\\', '|', '/' };
            int progressIdx = 0;
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.GetContext())
            {
                foreach (IDataObject obj in ctx.GetQuery<T>())
                {
                    Console.Write(progress[progressIdx++ % progress.Length]);
                    if (Console.CursorLeft > 0) Console.CursorLeft -= 1;

                    ObjectClass origObjClass = obj.GetObjectClass(FrozenContext.Single);
                    ObjectClass objClass = origObjClass;
                    ObjectClass rootClass = origObjClass.GetRootClass();

                    string objClassTypeString = objClass.GetDataTypeString();
                    string rootClassTypeString = rootClass.GetDataTypeString();

                    constructor.Statements.AddExpression("{{var obj = this.Find<{0}>({1})", objClassTypeString, obj.ID);
                    while (objClass != null)
                    {
                        foreach (ObjectReferenceProperty prop in objClass.Properties.OfType<ObjectReferenceProperty>())
                        {
                            object val;
                            try
                            {
                                val = obj.GetPropertyValue<object>(prop.PropertyName);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                // TODO: Hack!
                                continue;
                            }
                            if (val == null) continue;

                            string refTypeString = prop.GetPropertyTypeString();
                            if (prop.IsList)
                            {
                                foreach (IDataObject v in (IEnumerable)val)
                                {
                                    constructor.Statements.AddExpression("obj.{0}.Add(this.Find<{1}>({2}))",
                                        prop.PropertyName, refTypeString, v.ID);
                                }
                            }
                            else
                            {
                                constructor.Statements.AddExpression("obj.{0} = this.Find<{1}>({2})",
                                    prop.PropertyName, refTypeString, ((IDataObject)val).ID);
                            }
                        }

                        //foreach (BackReferenceProperty prop in objClass.Properties.OfType<BackReferenceProperty>())
                        //{
                        //    IEnumerable val;
                        //    try
                        //    {
                        //        val = obj.GetPropertyValue<IEnumerable>(prop.PropertyName);
                        //    }
                        //    catch (ArgumentOutOfRangeException)
                        //    {
                        //        // TODO: Hack!
                        //        continue;
                        //    }

                        //    if (val == null) continue;

                        //    string refTypeString = prop.GetPropertyTypeString();

                        //    foreach (IDataObject v in val)
                        //    {
                        //        constructor.Statements.AddExpression("obj.{0}.Add(this.Find<{1}>({2}))",
                        //            prop.PropertyName, refTypeString, v.ID);
                        //    }
                        //}
                        objClass = objClass.BaseObjectClass;
                    }
                    constructor.Statements.AddExpression("}");
                }
            }
        }

        public virtual void GenerateFrozenContext_ObjectInitializer<T>(CodeConstructor constructor)
            where T : IDataObject
        {
            char[] progress = new char[] { '-', '\\', '|', '/' };
            int progressIdx = 0;
            using (IKistlContext ctx = Kistl.API.Server.KistlDataContext.GetContext())
            {
                foreach (IDataObject obj in ctx.GetQuery<T>())
                {
                    Console.Write(progress[progressIdx++ % progress.Length]);
                    if (Console.CursorLeft > 0) Console.CursorLeft -= 1;

                    ObjectClass origObjClass = obj.GetObjectClass(FrozenContext.Single);
                    ObjectClass objClass = origObjClass;
                    ObjectClass rootClass = origObjClass.GetRootClass();

                    string objClassTypeString = objClass.GetDataTypeString();
                    string rootClassTypeString = rootClass.GetDataTypeString();

                    StringBuilder sb = new StringBuilder();
                    sb.AppendLine(string.Format("{{ var _{0}_obj = new {1}{2}() {{",
                        rootClassTypeString.Replace('.', '_'),
                        objClassTypeString,
                        API.Helper.ImplementationSuffix));

                    while (objClass != null)
                    {
                        foreach (Property prop in objClass.Properties.OfType<Property>().Where(p => p.IsList == false))
                        {
                            if (prop is ObjectReferenceProperty) continue;

                            object val;
                            try
                            {
                                val = obj.GetPropertyValue<object>(prop.PropertyName);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                // TODO: Hack!
                                continue;
                            }
                            if (val == null) continue;

                            sb.AppendFormat("                {0} = ", prop.PropertyName);

                            #region Generate Propertyinitializer
                            if (prop is StringProperty)
                            {
                                sb.AppendFormat("@\"{0}\"", ((string)val).Replace("\"", "\"\""));
                            }
                            else if (prop is BoolProperty)
                            {
                                sb.AppendFormat("{0}", ((bool)val) ? "true" : "false");
                            }
                            else if (prop is EnumerationProperty)
                            {
                                EnumerationProperty eprop = (EnumerationProperty)prop;
                                sb.Append(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", eprop.GetPropertyTypeString(), val));
                            }
                            else
                            {
                                sb.Append(string.Format(CultureInfo.InvariantCulture, "{0}", val));
                            }
                            #endregion

                            sb.AppendLine(",");
                        }

                        objClass = objClass.BaseObjectClass;
                    }

                    sb.AppendFormat("                ID = {0}",
                        obj.GetPropertyValue<int>("ID"));

                    sb.Append("}");
                    constructor.Statements.AddExpression(sb.ToString());

                    objClass = origObjClass;

                    //// Lists
                    while (objClass != null)
                    {
                        foreach (Property prop in objClass.Properties.OfType<Property>().Where(p => p.IsList == true))
                        {
                            if (prop is ObjectReferenceProperty) continue;

                            IEnumerable valList;
                            try
                            {
                                valList = obj.GetPropertyValue<IEnumerable>(prop.PropertyName);
                            }
                            catch (ArgumentOutOfRangeException)
                            {
                                // TODO: Hack!
                                continue;
                            }
                            if (valList == null) continue;

                            foreach (object val in valList)
                            {
                                if (val == null) continue;
                                sb = new StringBuilder();
                                sb.AppendFormat("_{0}_obj.{0}.Add(", rootClassTypeString.Replace('.', '_'), prop.PropertyName);

                                #region Generate Propertyinitializer
                                if (prop is StringProperty)
                                {
                                    sb.AppendFormat("@\"{0}\"", ((string)val).Replace("\"", "\"\""));
                                }
                                else if (prop is BoolProperty)
                                {
                                    sb.AppendFormat("{0}", ((bool)val) ? "true" : "false");
                                }
                                else if (prop is EnumerationProperty)
                                {
                                    EnumerationProperty eprop = (EnumerationProperty)prop;
                                    sb.Append(string.Format(CultureInfo.InvariantCulture, "{0}.{1}", eprop.GetPropertyTypeString(), val));
                                }
                                else
                                {
                                    sb.Append(string.Format(CultureInfo.InvariantCulture, "{0}", val));
                                }
                                #endregion

                                sb.Append(")");
                                constructor.Statements.AddExpression(sb.ToString());
                            }
                        }
                        objClass = objClass.BaseObjectClass;
                    }

                    constructor.Statements.AddExpression("_{0}.Add(_{0}_obj); }}",
                        rootClassTypeString.Replace('.', '_'));

                }
            }
        }

        private void GenerateFrozenContext(IQueryable<ObjectClass> objClassList)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            // Create Namespace
            CodeNamespace ns = CreateNamespace(code, "temp", TaskEnum.Interface);

            CodeTypeDeclaration c = ns.CreateClass("FrozenContextImplementation", "Kistl.API.FrozenContext");

            // Create structure
            CodeConstructor constructor = c.CreateConstructor();
            CodeMemberMethod getQuery = c.CreateOverrideMethod("GetQuery", typeof(IQueryable<IDataObject>));
            getQuery.Parameters.Add(new CodeParameterDeclarationExpression(typeof(Type), "type"));

            var baseClassList = objClassList.Where(i => i.BaseObjectClass == null && i.IsFrozenObject);

            // Create Fields and GetQueryMethod
            foreach (ObjectClass objClass in baseClassList)
            {
                Console.Write(".");
                CodeMemberField f = c.CreateField(
                    string.Format("List<{0}>", objClass.GetDataTypeString()),
                    "_" + objClass.GetDataTypeString().Replace('.', '_'),
                    string.Format("new List<{0}>()", objClass.GetDataTypeString()));

                getQuery.Statements.AddExpression(string.Format("if (typeof({0}).IsAssignableFrom(type)) return _{1}.Cast<IDataObject>().AsQueryable<IDataObject>()",
                    objClass.GetDataTypeString(), objClass.GetDataTypeString().Replace('.', '_')));
            }

            getQuery.Statements.AddExpression("return base.GetQuery(type)");

            // Create Objects
            foreach (ObjectClass objClass in baseClassList)
            {
                Console.Write(".");
                Type type = Type.GetType(objClass.Module.Namespace + "." + objClass.ClassName + API.Helper.ImplementationSuffix + ", " + ApplicationContext.Current.ImplementationAssembly);
                if (type == null) continue; // Not created yet

                MethodInfo mi = this.GetType().FindGenericMethod("GenerateFrozenContext_ObjectInitializer",
                    new Type[] { type }, // Generic Parameter
                    new Type[] { typeof(CodeConstructor) }); // Parameter
                mi.Invoke(this, new object[] { constructor });

                constructor.Statements.AddExpression("_{0}.ForEach<IDataObject>(obj => this.Attach(obj))", objClass.GetDataTypeString().Replace('.', '_'));
            }

            // Link Objects
            foreach (ObjectClass objClass in baseClassList)
            {
                Console.Write(".");

                Type type = Type.GetType(objClass.Module.Namespace + "." + objClass.ClassName + API.Helper.ImplementationSuffix + ", " + ApplicationContext.Current.ImplementationAssembly);
                if (type == null) continue; // Not created yet

                MethodInfo mi = this.GetType().FindGenericMethod("GenerateFrozenContext_LinkObjectReferences",
                    new Type[] { type }, // Generic Parameter
                    new Type[] { typeof(CodeConstructor) }); // Parameter
                mi.Invoke(this, new object[] { constructor });
            }

            constructor.Statements.AddExpression("_initialized = true");

            foreach (TaskEnum task in new TaskEnum[] { TaskEnum.Client, TaskEnum.Server })
            {
                code = new CodeCompileUnit();
                ns = CreateNamespace(code, task.GetKistObjectsName(), task);

                ns.Types.Add(c);

                // Generate the code & save
                SaveFile(code, string.Format(@"{0}\FrozenContext.cs", task.GetKistObjectsName()));
            }
        }
        #endregion

        #region GenerateObjectSerializer
        protected virtual void GenerateObjectSerializer(TaskEnum task, IQueryable<ObjectClass> objClassList)
        {
            CodeCompileUnit code = new CodeCompileUnit();

            // Create Namespace
            CodeNamespace ns = CreateNamespace(code, "Kistl.API", task);


            // XMLObjectCollection
            CodeTypeDeclaration c = ns.CreateSealedClass("XMLObjectCollection", "IXmlObjectCollection");
            c.AddAttribute("Serializable");
            c.AddAttribute("XmlRoot", new CodeAttributeArgument("ElementName", new CodePrimitiveExpression("ObjectCollection")));

            CodeMemberField f = c.CreateField(typeof(List<Object>), "_Objects", "new List<Object>()");

            CodeMemberProperty p = c.CreateProperty(f.Type, "Objects", false);
            p.GetStatements.AddExpression("return _Objects");
            foreach (ObjectClass objClass in objClassList)
            {
                p.CustomAttributes.Add(
                    new CodeAttributeDeclaration("XmlArrayItem",
                        new CodeAttributeArgument("Type", new CodeTypeOfExpression(objClass.Module.Namespace + "." + objClass.ClassName)),
                        new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(objClass.ClassName))
                    ));
            }

            CodeMemberMethod m = c.CreateMethod("ToList", "List<T>");
            CodeTypeParameter ct = new CodeTypeParameter("T");
            ct.Constraints.Add("IDataObject");
            m.TypeParameters.Add(ct);

            m.Statements.AddExpression(@"return new List<T>(Objects.OfType<T>())");

            // XMLObject
            c = ns.CreateSealedClass("XMLObject", "IXmlObject");
            c.CustomAttributes.Add(new CodeAttributeDeclaration("Serializable"));
            c.CustomAttributes.Add(new CodeAttributeDeclaration("XmlRoot", new CodeAttributeArgument("ElementName", new CodePrimitiveExpression("Object"))));

            f = c.CreateField(typeof(Object), "_Object");

            p = c.CreateProperty(typeof(Object), "Object");
            p.GetStatements.AddExpression("return _Object");
            p.SetStatements.AddExpression("_Object = value");
            foreach (ObjectClass objClass in objClassList)
            {
                p.CustomAttributes.Add(
                    new CodeAttributeDeclaration("XmlElement",
                        new CodeAttributeArgument("Type", new CodeTypeOfExpression(objClass.Module.Namespace + "." + objClass.ClassName)),
                        new CodeAttributeArgument("ElementName", new CodePrimitiveExpression(objClass.ClassName))
                    ));
            }

            // Generate the code & save
            SaveFile(code, string.Format(@"{0}\ObjectSerializer.cs", task.GetKistObjectsName()));
        }
        #endregion

        #region GenerateObjectInterfacesInternal
        protected virtual void GenerateObjectInterfaces(CurrentObjectClass current)
        {
        }

        private void GenerateObjectInterfacesInternal(CurrentObjectClass current)
        {
            current.code = new CodeCompileUnit();
            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.objClass.Module.Namespace, current.task);
            current.code_namespace.Imports.Add(new CodeNamespaceImport("Kistl.API"));

            // Create Class
            current.code_class = current.code_namespace.CreateInterface(current.objClass.ClassName,
                current.objClass.BaseObjectClass != null
                    ? current.objClass.BaseObjectClass.Module.Namespace + "." + current.objClass.BaseObjectClass.ClassName
                    : "IDataObject");

            foreach (Interface i in current.objClass.ImplementsInterfaces)
            {
                current.code_class.BaseTypes.Add(i.Module.Namespace + "." + i.ClassName);
            }

            current.code_class.AddSummaryComment(current.objClass.Description);

            // Properties
            GenerateInterfaceProperties((CurrentObjectClass)current.Clone(), current.objClass.Properties);

            // Methods
            GenerateInferfaceMethods((CurrentObjectClass)current.Clone(), current.objClass.Methods);

            // Generate the code & save
            SaveFile(current.code, current.task.GetKistObjectsName() + @"\" + current.objClass.ClassName + ".Designer.cs");
        }
        #endregion

        #region GenerateObjects
        protected virtual void GenerateObjects(CurrentObjectClass current)
        {
        }

        private void GenerateObjectsInternal(CurrentObjectClass current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.objClass.Module.Namespace, current.task);
            current.code_namespace.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", current.task)));

            // Create Class
            current.code_class = current.code_namespace.CreateClass(current.objClass.ClassName + Kistl.API.Helper.ImplementationSuffix,
                current.objClass.BaseObjectClass != null
                    ? current.objClass.BaseObjectClass.Module.Namespace + "." + current.objClass.BaseObjectClass.ClassName + Kistl.API.Helper.ImplementationSuffix
                    : string.Format("Base{0}DataObject", current.task));

            // Related Interface
            current.code_class.BaseTypes.Add(current.objClass.ClassName);

            // Implementing Interfaces
            foreach (Interface i in current.objClass.ImplementsInterfaces)
            {
                current.code_class.BaseTypes.Add(i.Module.Namespace + "." + i.ClassName);
            }

            // Constructor
            current.code_constructor = current.code_class.CreateConstructor();

            GenerateObjects(current);

            if (current.objClass.BaseObjectClass == null)
            {
                // Create Default Properties
                GenerateDefaultPropertiesInternal((CurrentObjectClass)current.Clone());
            }

            // Create Properties
            GeneratePropertiesInternal((CurrentObjectClass)current.Clone());

            // Create DataObject Default Methods
            GenerateDefaultMethodsInternal((CurrentObjectClass)current.Clone());

            // Create DataObject Methods
            GenerateMethodsInternal((CurrentObjectClass)current.Clone());

            // Create DataObject StreamingMethods
            GenerateStreamMethodsInternal((CurrentObjectClass)current.Clone(), current.objClass.Properties);

            // Generate the code & save
            SaveFile(current.code, current.task.GetKistObjectsName() + @"\" + current.objClass.ClassName + "." + current.task + ".Designer.cs");
        }
        #endregion

        #region GenerateInterfaces

        #region GenerateInterfaceProperties/Methods
        private void GenerateInterfaceProperties(CurrentBase current, IEnumerable<BaseProperty> properties)
        {
            foreach (BaseProperty p in properties)
            {
                CodeMemberProperty codeProp;
                if (p.IsObjectReferencePropertyList() && !((ObjectReferenceProperty)p).HasStorage())
                {
                    CodeTypeReference type = new CodeTypeReference("ICollection", p.ToCodeTypeReference(current.task));
                    codeProp = current.code_class.CreateProperty(type, p.PropertyName, false);
                }
                else if (p.IsListProperty())
                {
                    CodeTypeReference type = new CodeTypeReference("IList", p.ToCodeTypeReference(current.task));
                    codeProp = current.code_class.CreateProperty(type, p.PropertyName, false);
                }
                else
                {
                    codeProp = current.code_class.CreateProperty(p.ToCodeTypeReference(current.task), p.PropertyName);
                }

                codeProp.AddSummaryComment(p.Description);
            }
        }

        private void GenerateInferfaceMethods(CurrentBase current, IEnumerable<Method> methods)
        {
            foreach (Method method in methods)
            {
                // TODO: I dont know if there's another way
                // Default Methods, do not generate
                if (method.IsDefaultMethod())
                {
                    continue;
                }

                BaseParameter returnParam = method.Parameter.SingleOrDefault(p => p.IsReturnParameter);
                CodeMemberMethod m = current.code_class.CreateMethod(method.MethodName, returnParam.ToCodeTypeReference());
                m.AddSummaryComment(method.Description);
                if (returnParam != null)
                {
                    m.AddReturnsComment(returnParam.Description);
                }

                // added inverse sort by ID to stabilise order of parameters
                // TODO: implement orderable lists and backrefs instead!
                foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter).OrderBy(p => -p.ID))
                {
                    m.Parameters.Add(new CodeParameterDeclarationExpression(
                        new CodeTypeReference(param.GetParameterTypeString()), param.ParameterName));
                    m.AddParamComment(param.ParameterName, param.Description);
                }
            }
        }
        #endregion

        protected virtual void GenerateInterfaces(CurrentInterface current)
        {
        }

        private void GenerateInterfacesInternal(CurrentInterface current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.@interface.Module.Namespace, current.task);

            // Create Class
            current.code_class = current.code_namespace.CreateInterface(current.@interface.ClassName);
            current.code_class.AddSummaryComment(current.@interface.Description);

            // Properties
            GenerateInterfaceProperties((CurrentInterface)current.Clone(), current.@interface.Properties);

            // Methods
            GenerateInferfaceMethods((CurrentInterface)current.Clone(), current.@interface.Methods);

            GenerateInterfaces(current);

            // Generate the code & save
            SaveFile(current.code, current.task.GetKistObjectsName() + @"\" + current.@interface.ClassName + "." + current.task + ".Designer.cs");
        }
        #endregion

        #region GenerateStructInterfacesInternal
        protected virtual void GenerateStructInterfaces(CurrentStruct current)
        {
        }

        private void GenerateStructInterfacesInternal(CurrentStruct current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.@struct.Module.Namespace, current.task);

            // Create Struct class
            current.code_class = current.code_namespace.CreateInterface(current.@struct.ClassName, "IStruct");
            current.code_class.AddSummaryComment(current.@struct.Description);

            // Create Properties
            GenerateInterfaceProperties((CurrentStruct)current.Clone(), current.@struct.Properties);

            // Call derived Classes
            GenerateStructInterfaces(current);

            // Generate the code & save
            SaveFile(current.code, current.task.GetKistObjectsName() + @"\" + current.@struct.ClassName + "." + current.task + ".Designer.cs");
        }
        #endregion

        #region GenerateStructs
        protected virtual void GenerateStructs(CurrentStruct current)
        {
        }

        private void GenerateStructsInternal(CurrentStruct current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.@struct.Module.Namespace, current.task);
            current.code_namespace.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", current.task)));

            // Create Struct class
            current.code_class = current.code_namespace.CreateClass(current.@struct.ClassName + Kistl.API.Helper.ImplementationSuffix, string.Format("Base{0}StructObject", current.task));

            // Related Interface
            current.code_class.BaseTypes.Add(current.@struct.ClassName);

            // Create Properties
            GeneratePropertiesInternal((CurrentStruct)current.Clone());

            // Create Structs StreamingMethods
            GenerateStreamMethodsInternal((CurrentStruct)current.Clone(), current.@struct.Properties);

            // Call derived Classes
            GenerateStructs(current);

            // Generate the code & save
            SaveFile(current.code, current.task.GetKistObjectsName() + @"\" + current.@struct.ClassName + "." + current.task + ".Designer.cs");
        }
        #endregion

        #region GenerateEnumerations
        protected virtual void GenerateEnumerations(CurrentEnumeration current)
        {
        }

        private void GenerateEnumerationsInternal(CurrentEnumeration current)
        {
            current.code = new CodeCompileUnit();

            // Create Namespace
            current.code_namespace = CreateNamespace(current.code, current.enumeration.Module.Namespace, current.task);
            if (current.task != TaskEnum.Interface)
            {
                current.code_namespace.Imports.Add(new CodeNamespaceImport(string.Format("Kistl.API.{0}", current.task)));
            }

            // Create Class
            current.code_class = current.code_namespace.CreateEnum(current.enumeration.ClassName);
            current.code_class.AddSummaryComment(current.enumeration.Description);

            foreach (EnumerationEntry e in current.enumeration.EnumerationEntries)
            {
                CodeMemberField mf = current.code_class.CreateField(typeof(int), e.Name, e.Value.ToString());
                mf.AddSummaryComment(e.Description);
            }

            GenerateEnumerations(current);

            // Generate the code & save
            SaveFile(current.code, current.task.GetKistObjectsName() + @"\" + current.enumeration.ClassName + "." + current.task + ".Designer.cs");
        }
        #endregion

        #region GenerateDefaultProperties
        protected virtual void GenerateDefaultProperty_ID(CurrentObjectClass current)
        {
        }

        private void GenerateDefaultProperty_IDInternal(CurrentObjectClass current)
        {
            // Client does not need such a stupid thing
            if (current.task == TaskEnum.Server)
            {
                // Create _Server_ ID member
                current.code_field = current.code_class.CreateField(typeof(int), "_ID");

                current.code_property = current.code_class.CreateProperty(typeof(int), "ID");
                current.code_property.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                current.code_property.GetStatements.AddExpression("return _ID");
                current.code_property.SetStatements.AddExpression("_ID = value");

                GenerateDefaultProperty_ID(current);
            }
        }

        protected virtual void GenerateDefaultPropertiesInternal(CurrentObjectClass current)
        {
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)current.Clone());
        }
        #endregion

        #region GenerateProperties

        #region GenerateValueTypeProperty
        protected virtual void GenerateProperties_ValueTypeProperty(CurrentBase current)
        {
        }

        private void GenerateProperties_ValueTypePropertyInternal(CurrentBase current)
        {
            current.code_field = current.property.CreateField(current.code_class, current.task);
            current.code_property = current.property.CreateNotifyingProperty(current.code_class, current.task);

            GenerateProperties_ValueTypeProperty(current);
        }
        #endregion

        #region GenerateValueTypeProperty_Collection
        protected virtual void GenerateProperties_ValueTypeProperty_Collection(CurrentObjectClass current,
            CurrentObjectClass collectionClass, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
        }

        private void GenerateProperties_ValueTypeProperty_CollectionInternal(CurrentObjectClass current)
        {
            CurrentObjectClass collectionClass = (CurrentObjectClass)current.Clone();

            collectionClass.code_class = collectionClass.code_namespace.CreateClass(Generator.GetPropertyCollectionObjectType((Property)current.property).Classname + Kistl.API.Helper.ImplementationSuffix,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.task));
            if (current.task != TaskEnum.Interface)
            {
                collectionClass.code_class.BaseTypes.Add(string.Format("ICollectionEntry<{0}, {1}>",
                    current.property.GetPropertyTypeString(), current.objClass.GetTypeMoniker().NameDataObject));
                // collectionClass.code_class.TypeAttributes = TypeAttributes.NotPublic;
            }

            // Create ID
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)collectionClass.Clone());

            // Create Property
            collectionClass.code_field = collectionClass.code_class.CreateField(
                current.property.ToCodeTypeReference(current.task), "_Value");

            collectionClass.code_property = collectionClass.code_class.CreateNotifyingProperty(
                current.property.ToCodeTypeReference(current.task),
                "Value", "_Value", "_Value", "Value");

            // Create Parent
            CurrentObjectClass parent = (CurrentObjectClass)collectionClass.Clone();
            parent.code_property = collectionClass.code_class.CreateProperty(current.objClass.GetTypeMoniker().NameDataObject, "Parent");
            parent.code_property.AddAttribute("XmlIgnore");

            if (current.task == TaskEnum.Client)
            {
                parent.code_property.GetStatements.AddExpression(
                        string.Format(@"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Parent)", current.objClass.ClassName));
                parent.code_property.SetStatements.AddComment(@"TODO: Damit hab ich noch ein Problem. Wenn die Property not nullable ist, dann sollte das eigentlich nicht möglich sein.");
                parent.code_property.SetStatements.AddExpression(@"_fk_Parent = value.ID");
            }

            // Create SerializerParent
            CurrentObjectClass serializerParent = (CurrentObjectClass)collectionClass.Clone();

            // Serializer Parent fk_ Field und Property
            serializerParent.code_field = collectionClass.code_class.CreateField(typeof(int), "_fk_Parent");
            serializerParent.code_property = collectionClass.code_class.CreateProperty(typeof(int), "fk_Parent");

            if (current.task == TaskEnum.Client)
            {
                serializerParent.code_property.GetStatements.AddExpression(@"return _fk_Parent");
                serializerParent.code_property.SetStatements.AddExpression("_fk_Parent = value");
            }

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = current.code_class.CreateProperty((CodeTypeReference)null, current.property.PropertyName, false);
            current.code_property.Type = new CodeTypeReference(string.Format("IList<{0}>", current.property.GetPropertyTypeString()));

            if (current.task == TaskEnum.Client)
            {
                current.code_property.GetStatements.AddExpression(@"return _{0}", current.property.PropertyName);

                current.code_field = current.code_class.CreateField(
                    string.Format("ListPropertyCollection<{0}, {1}, {2}>",
                        current.property.GetPropertyTypeString(),
                        current.objClass.GetTypeMoniker().NameDataObject,
                        collectionClass.code_class.Name),
                    string.Format(@"_{0}", current.property.PropertyName));

                current.code_constructor.Statements.AddExpression(@"_{0} = new ListPropertyCollection<{1}, {2}, {3}>(this, ""{0}"")",
                        current.property.PropertyName,
                        current.property.GetPropertyTypeString(),
                        current.objClass.GetTypeMoniker().NameDataObject,
                        collectionClass.code_class.Name);
            }

            GenerateProperties_ValueTypeProperty_Collection(current, collectionClass, parent, serializerParent);
            GenerateProperties_ValueTypeProperty_Collection_StreamMethods(collectionClass, parent, serializerParent);
        }

        #region GenerateProperties_ValueTypeProperty_Collection_StreamMethods
        private void GenerateProperties_ValueTypeProperty_Collection_StreamMethods(CurrentObjectClass current,
            CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
            // Create ToStream Method
            CodeMemberMethod m = current.code_class.CreateOverrideMethod("ToStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));

            m.Statements.AddExpression("base.ToStream(sw)");
            m.Statements.AddExpression("BinarySerializer.ToBinary(this.Value, sw)");
            m.Statements.AddExpression("BinarySerializer.ToBinary(this.fk_Parent, sw)");

            m = current.code_class.CreateOverrideMethod("FromStream", typeof(void));
            //m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.AddExpression("base.FromStream(sr)");
            m.Statements.AddExpression("BinarySerializer.FromBinary(out this._Value, sr)");
            m.Statements.AddExpression("BinarySerializer.FromBinary(out this._fk_Parent, sr)");

            if (current.task == TaskEnum.Client)
            {
                m = current.code_class.CreateOverrideMethod("ApplyChanges", typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

                m.Statements.AddExpression("base.ApplyChanges(obj)");
                m.Statements.AddExpression("(({0})obj)._Value = this._Value", current.code_class.Name);
                m.Statements.AddExpression("(({0})obj)._fk_Parent = this._fk_Parent", current.code_class.Name);
            }
        }
        #endregion

        #endregion

        #region GenerateProperties_ObjectReferenceProperty
        protected virtual void GenerateProperties_ObjectReferenceProperty(CurrentObjectClass current, CurrentObjectClass serializer)
        {
        }

        private void GenerateProperties_ObjectReferencePropertyInternal(CurrentObjectClass current)
        {
            // Check if Datatype exits
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetPropertyTypeString()) == null)
                throw new ArgumentOutOfRangeException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetPropertyTypeString(), current.objClass.ClassName, current.property.PropertyName));

            ObjectReferenceProperty objRefProp = (ObjectReferenceProperty)current.property;
            current.code_property = current.code_class.CreateProperty(current.property.ToCodeTypeReference(current.task), current.property.PropertyName);
            current.code_property.AddAttribute("XmlIgnore");

            if (current.task == TaskEnum.Client)
            {
                current.code_property.GetStatements.AddExpression(@"if (fk_{1} == null) return null;
                return Context.Find<{0}>(fk_{1}.Value)", current.property.GetPropertyTypeString(), current.property.PropertyName);

                if (objRefProp.GetOpposite() != null && objRefProp.GetRelationType() == RelationType.one_n)
                {
                    current.code_property.SetStatements.AddExpression(@"if (IsReadonly) throw new ReadOnlyObjectException();
                if (value != null)
                {{
                    if (fk_{0} != value.ID && fk_{0} != null) value.{1}.Remove(this);
                    fk_{0} = value.ID;
                    if (!value.{1}.Contains(this)) value.{1}.Add(this);
                }}
                else
                {{
                    if ({0} != null && {0}.{1}.Contains(this)) {0}.{1}.Remove(this);
                    fk_{0} = null;
                }}", objRefProp.PropertyName, objRefProp.GetOpposite().PropertyName);
                }
                else
                {
                    current.code_property.SetStatements.AddExpression(@"fk_{0} = value != null ? (int?)value.ID : null",
                        objRefProp.PropertyName);
                }
            }

            CurrentObjectClass serializer = (CurrentObjectClass)current.Clone();

            // Serializer fk_ Field und Property
            string fieldName = "_fk_" + current.property.PropertyName;
            serializer.code_field = current.code_class.CreateField(typeof(int?), fieldName, "null");

            if (current.task == TaskEnum.Client)
            {
                serializer.code_property = current.code_class.CreateNotifyingProperty(typeof(int?), "fk_" + current.property.PropertyName,
                    fieldName, fieldName, current.property.PropertyName);
            }
            else
            {
                serializer.code_property = current.code_class.CreateProperty(typeof(int?), "fk_" + current.property.PropertyName);
            }

            GenerateProperties_ObjectReferenceProperty(current, serializer);
        }
        #endregion

        #region GenerateProperties_ObjectReferenceProperty_Collection
        protected virtual void GenerateProperties_ObjectReferenceProperty_Collection(CurrentObjectClass current, CurrentObjectClass collectionClass,
            CurrentObjectClass serializerValue, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
        }

        private void GenerateProperties_ObjectReferenceProperty_CollectionInternal(CurrentObjectClass current)
        {
            if (string.IsNullOrEmpty(current.property.GetPropertyTypeString())) throw new ArgumentNullException(
                 string.Format("ValueProperty {0}.{1} has an empty Datatype! Please implement BaseProperty.GetPropertyTypeString()",
                     current.objClass.ClassName, current.property.PropertyName));

            // Check if Datatype exits
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetPropertyTypeString()) == null)
                throw new ArgumentOutOfRangeException(string.Format("ObjectReference {0} not found on ObjectReferenceProperty {1}.{2}",
                    current.property.GetPropertyTypeString(), current.objClass.ClassName, current.property.PropertyName));

            CurrentObjectClass collectionClass = (CurrentObjectClass)current.Clone();

            collectionClass.code_class = collectionClass.code_namespace.CreateClass(Generator.GetPropertyCollectionObjectType((Property)current.property).Classname + Kistl.API.Helper.ImplementationSuffix,
                string.Format("Kistl.API.{0}.Base{0}CollectionEntry", current.task));
            if (current.task != TaskEnum.Interface)
            {
                collectionClass.code_class.BaseTypes.Add(string.Format("ICollectionEntry<{0}, {1}>",
                    current.property.GetPropertyTypeString(), current.objClass.GetTypeMoniker().NameDataObject));
                // collectionClass.code_class.TypeAttributes = TypeAttributes.NotPublic;
            }

            // Create ID
            GenerateDefaultProperty_IDInternal((CurrentObjectClass)collectionClass.Clone());

            // Create Value
            collectionClass.code_property = collectionClass.code_class.CreateProperty(
                collectionClass.property.ToCodeTypeReference(collectionClass.task), "Value");
            collectionClass.code_property.AddAttribute("XmlIgnore");

            // Create Parent
            CurrentObjectClass parent = (CurrentObjectClass)collectionClass.Clone();
            parent.code_property = collectionClass.code_class.CreateProperty(current.objClass.GetTypeMoniker().NameDataObject, "Parent");
            parent.code_property.AddAttribute("XmlIgnore");

            // Create SerializerValue
            CurrentObjectClass serializerValue = (CurrentObjectClass)collectionClass.Clone();

            // Serializer fk_ Field und Property
            serializerValue.code_field = collectionClass.code_class.CreateField(typeof(int), "_fk_Value");
            serializerValue.code_property = collectionClass.code_class.CreateProperty(typeof(int), "fk_Value");

            // Create SerializerParent
            CurrentObjectClass serializerParent = (CurrentObjectClass)collectionClass.Clone();

            // Serializer Parent fk_ Field und Property
            serializerParent.code_field = collectionClass.code_class.CreateField(typeof(int), "_fk_Parent");
            serializerParent.code_property = collectionClass.code_class.CreateProperty(typeof(int), "fk_Parent");

            // Create NavigationProperty Class -> Collectionclass
            current.code_property = current.code_class.CreateProperty((CodeTypeReference)null, current.property.PropertyName, false);
            current.code_property.Type = new CodeTypeReference(string.Format("IList<{0}>", current.property.GetPropertyTypeString()));

            if (current.task == TaskEnum.Client)
            {
                current.code_property.GetStatements.AddExpression(@"return _{0}", current.property.PropertyName);

                current.code_field = current.code_class.CreateField(
                    string.Format("ListPropertyCollection<{0}, {1}, {2}>",
                        current.property.GetPropertyTypeString(),
                        current.objClass.GetTypeMoniker().NameDataObject,
                        collectionClass.code_class.Name),
                    string.Format(@"_{0}", current.property.PropertyName));

                current.code_constructor.Statements.AddExpression(@"_{0} = new ListPropertyCollection<{1}, {2}, {3}>(this, ""{0}"")",
                        current.property.PropertyName,
                        current.property.GetPropertyTypeString(),
                        current.objClass.GetTypeMoniker().NameDataObject,
                        collectionClass.code_class.Name);
            }

            // Get/Set for client
            if (current.task == TaskEnum.Client)
            {
                collectionClass.code_property.GetStatements.AddExpression(
                    @"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Value)",
                    current.property.GetPropertyTypeString());
                collectionClass.code_property.SetStatements.AddExpression(@"fk_Value = value.ID;");

                parent.code_property.GetStatements.AddExpression(
                    @"return Context.GetQuery<{0}>().Single(o => o.ID == fk_Parent)", current.objClass.ClassName);
                parent.code_property.SetStatements.AddExpression(@"_fk_Parent = value.ID");

                serializerValue.code_property.GetStatements.AddExpression(@"return _fk_Value");
                serializerValue.code_property.SetStatements.AddExpression(@"if(_fk_Value != value)
                {
                    base.NotifyPropertyChanging(""Value"");
                    _fk_Value = value;
                    base.NotifyPropertyChanged(""Value"");
                }");

                serializerParent.code_property.GetStatements.AddExpression(@"return _fk_Parent");
                serializerParent.code_property.SetStatements.AddExpression("_fk_Parent = value");
            }

            GenerateProperties_ObjectReferenceProperty_Collection(current, collectionClass, serializerValue, parent, serializerParent);
            GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods(collectionClass, serializerValue, parent, serializerParent);
        }

        #region GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods
        private void GenerateProperties_ObjectReferenceProperty_Collection_StreamMethods(CurrentObjectClass current,
            CurrentObjectClass serializerValue, CurrentObjectClass parent, CurrentObjectClass serializerParent)
        {
            // Create ToStream Method
            CodeMemberMethod m = current.code_class.CreateOverrideMethod("ToStream", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));

            m.Statements.AddExpression("base.ToStream(sw)");
            m.Statements.AddExpression("BinarySerializer.ToBinary(this.fk_Value, sw)");
            m.Statements.AddExpression("BinarySerializer.ToBinary(this.fk_Parent, sw)");

            m = current.code_class.CreateOverrideMethod("FromStream", typeof(void));
            //m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.IKistlContext)), "ctx"));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));

            m.Statements.AddExpression("base.FromStream(sr)");
            m.Statements.AddExpression("BinarySerializer.FromBinary(out this._fk_Value, sr)");
            m.Statements.AddExpression("BinarySerializer.FromBinary(out this._fk_Parent, sr)");

            if (current.task == TaskEnum.Client)
            {
                m = current.code_class.CreateOverrideMethod("ApplyChanges", typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(Kistl.API.ICollectionEntry)), "obj"));

                m.Statements.AddExpression("base.ApplyChanges(obj)");
                m.Statements.AddExpression("(({0})obj)._fk_Value = this.fk_Value", current.code_class.Name);
                m.Statements.AddExpression("(({0})obj)._fk_Parent = this.fk_Parent", current.code_class.Name);
            }
        }

        #endregion

        #endregion

        #region GenerateProperties_BackReferenceSingleProperty
        protected virtual void GenerateProperties_BackReferenceSingleProperty(CurrentObjectClass current, CurrentObjectClass serializer)
        {
        }

        private void GenerateProperties_BackReferenceSinglePropertyInternal(CurrentObjectClass current)
        {
            current.code_property = current.code_class.CreateProperty(current.property.ToCodeTypeReference(current.task), current.property.PropertyName);
            current.code_property.AddAttribute("XmlIgnore");

            if (current.task == TaskEnum.Client)
            {
                current.code_property.GetStatements.AddExpression(@"if (fk_{1} == null) return null;
                return Context.Find<{0}>(fk_{1}.Value)", current.property.GetPropertyTypeString(), current.property.PropertyName);

                current.code_property.SetStatements.AddExpression(@"fk_{0} = value != null ? (int?)value.ID : null",
                    current.property.PropertyName);
            }

            CurrentObjectClass serializer = (CurrentObjectClass)current.Clone();

            // Serializer fk_ Field und Property
            string fieldName = "_fk_" + current.property.PropertyName;
            serializer.code_field = current.code_class.CreateField(typeof(int?), fieldName, "null");

            if (current.task == TaskEnum.Client)
            {
                serializer.code_property = current.code_class.CreateNotifyingProperty(typeof(int?), "fk_" + current.property.PropertyName,
                    fieldName, fieldName, current.property.PropertyName);
            }
            else
            {
                serializer.code_property = current.code_class.CreateProperty(typeof(int?), "fk_" + current.property.PropertyName);
            }

            GenerateProperties_BackReferenceSingleProperty(current, serializer);
        }
        #endregion

        #region GenerateProperties_BackReferenceProperty
        protected virtual void GenerateProperties_BackReferenceProperty(CurrentObjectClass current)
        {
        }

        private void GenerateProperties_BackReferencePropertyInternal(CurrentObjectClass current)
        {
            // Check if Datatype exits
            if (current.ctx.GetQuery<ObjectClass>().ToList().First(o => o.Module.Namespace + "." + o.ClassName == current.property.GetPropertyTypeString()) == null)
                throw new ArgumentOutOfRangeException(string.Format("ObjectReference {0} not found on BackReferenceProperty {1}.{2}",
                    current.property.GetPropertyTypeString(), current.objClass.ClassName, current.property.PropertyName));

            current.code_property = current.code_class.CreateProperty((CodeTypeReference)null, current.property.PropertyName, false);
            current.code_property.AddAttribute("XmlIgnore");

            TypeMoniker childType = new TypeMoniker(current.property.GetPropertyTypeString());
            current.code_property.Type = new CodeTypeReference("ICollection", new CodeTypeReference(childType.NameDataObject));

            if (current.task == TaskEnum.Client)
            {
                current.code_property.GetStatements.AddExpression(
              @"if (_{0} == null)
                {{
                    List<{1}> serverList;
                    if (Helper.IsPersistedObject(this))
                        serverList = Context.GetListOf<{1}>(this, ""{0}"");
                    else
                        serverList = new List<{1}>();

                    _{0} = new BackReferenceCollection<{1}>(
                         ""{2}"", this, serverList);
                }}
                return _{0}",
                            current.property.PropertyName,
                            childType.NameDataObject,
                            ((ObjectReferenceProperty)current.property).GetOpposite().PropertyName);

                CodeMemberField f = new CodeMemberField(new CodeTypeReference("BackReferenceCollection", new CodeTypeReference(childType.NameDataObject)),
                    "_" + current.property.PropertyName);

                current.code_class.Members.Add(f);
            }


            GenerateProperties_BackReferenceProperty(current);
        }
        #endregion

        #region GenerateProperties_StructProperty
        protected virtual void GenerateProperties_StructProperty(CurrentObjectClass current)
        {
        }

        private void GenerateProperties_StructPropertyInternal(CurrentObjectClass current)
        {
            //current.code_field = current.property.CreateField(current.code_class, current.task);
            current.code_field = current.code_class.CreateField(current.property.GetPropertyTypeString() + Kistl.API.Helper.ImplementationSuffix, "_" + current.property.PropertyName);
            current.code_property = current.code_class.CreateProperty(current.property.ToCodeTypeReference(current.task), current.property.PropertyName);

            current.code_property.GetStatements.AddExpression("return _{0}", current.property.PropertyName);
            current.code_property.SetStatements.AddExpression(@"if ({0} != value)
                {{
                    NotifyPropertyChanging(""{0}""); 
                    if (_{0} != null) _{0}.DetachFromObject(this, ""{0}"");
                    _{0} = ({1}{2})value;
                    if (_{0} != null) _{0}.AttachToObject(this, ""{0}"");
                    NotifyPropertyChanged(""{0}"");
                }}", current.property.PropertyName, current.property.GetPropertyTypeString(), Kistl.API.Helper.ImplementationSuffix);

            GenerateProperties_StructProperty(current);
        }
        #endregion

        #region GeneratePropertiesInternal
        private void GeneratePropertiesInternal(CurrentObjectClass current)
        {
            foreach (BaseProperty baseProp in current.objClass.Properties)
            {
                current.property = baseProp;
                CurrentObjectClass currentProperty = (CurrentObjectClass)current.Clone();
                if (baseProp.IsValueTypePropertyList())
                {
                    // Simple Property Collection
                    GenerateProperties_ValueTypeProperty_CollectionInternal(currentProperty);
                }
                else if (baseProp.IsValueTypePropertySingle())
                {
                    // Simple Property
                    GenerateProperties_ValueTypePropertyInternal(currentProperty);
                }
                else if (baseProp.IsObjectReferencePropertyList() && baseProp.HasStorage())
                {
                    // "pointer" Object Collection
                    GenerateProperties_ObjectReferenceProperty_CollectionInternal(currentProperty);
                }
                else if (baseProp.IsObjectReferencePropertySingle() && baseProp.HasStorage())
                {
                    // "pointer" Object
                    GenerateProperties_ObjectReferencePropertyInternal(currentProperty);
                }
                else if (baseProp.IsObjectReferencePropertyList() && !baseProp.HasStorage())
                {
                    // "Backpointer" List
                    GenerateProperties_BackReferencePropertyInternal(currentProperty);
                }
                else if (baseProp.IsObjectReferencePropertySingle() && !baseProp.HasStorage())
                {
                    // "Backpointer" Object
                    GenerateProperties_BackReferenceSinglePropertyInternal(currentProperty);
                }
                else if (baseProp.IsStructPropertySingle())
                {
                    // Struct Property
                    GenerateProperties_StructPropertyInternal(currentProperty);
                }
                else
                {
                    // not supported yet
                    throw new NotSupportedException("Unknown Propertytype " + baseProp.GetType().Name);
                }
            }
        }

        private void GeneratePropertiesInternal(CurrentStruct current)
        {
            foreach (BaseProperty baseProp in current.@struct.Properties)
            {
                current.property = baseProp;
                if (baseProp.IsValueTypePropertySingle())
                {
                    // Simple Property
                    GenerateProperties_ValueTypePropertyInternal((CurrentStruct)current.Clone());
                }
                else
                {
                    // objectreferences not supported yet
                    throw new NotSupportedException("Unknown Propertytype " + baseProp.GetType().Name);
                }
            }
        }
        #endregion

        #endregion

        #region GenerateDefaultMethods
        private void GenerateDefaultMethodsInternal(CurrentObjectClass current)
        {
            // Create ToString Delegate
            CodeMemberEvent e = new CodeMemberEvent();
            current.code_class.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ToStringHandler", new CodeTypeReference(current.objClass.ClassName));
            e.Name = "OnToString_" + current.objClass.ClassName;

            // Create PreSave Delegate
            e = new CodeMemberEvent();
            current.code_class.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(current.objClass.ClassName));
            e.Name = "OnPreSave_" + current.objClass.ClassName;

            // Create PostSave Delegate
            e = new CodeMemberEvent();
            current.code_class.Members.Add(e);

            e.Attributes = MemberAttributes.Public;
            e.Type = new CodeTypeReference("ObjectEventHandler", new CodeTypeReference(current.objClass.ClassName));
            e.Name = "OnPostSave_" + current.objClass.ClassName;

            // Create ToString Method
            CodeMemberMethod m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.AddAttribute("System.Diagnostics.DebuggerHidden");
            m.Name = "ToString";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(string));
            m.Statements.AddExpression(@"MethodReturnEventArgs<string> e = new MethodReturnEventArgs<string>();
            e.Result = base.ToString();
            if (OnToString_{0} != null)
            {{
                OnToString_{0}(this, e);
            }}
            return e.Result", current.objClass.ClassName);

            // Create NotifyPreSave Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "NotifyPreSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.AddExpression(@"base.NotifyPreSave();
            if (OnPreSave_{0} != null) OnPreSave_{0}(this)", current.objClass.ClassName);

            // Create NotifyPostSave Method
            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "NotifyPostSave";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Statements.AddExpression(@"base.NotifyPostSave();
            if (OnPostSave_{0} != null) OnPostSave_{0}(this)", current.objClass.ClassName);

            if (current.task == TaskEnum.Client)
            {
                // Create ApplyChanges Method
                m = new CodeMemberMethod();
                current.code_class.Members.Add(m);
                m.Name = "ApplyChanges";
                m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
                m.ReturnType = new CodeTypeReference(typeof(void));
                m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(IDataObject)), "obj"));
                m.Statements.AddExpression("base.ApplyChanges(obj)");

                foreach (BaseProperty p in current.objClass.Properties.OfType<BaseProperty>())
                {
                    string stmt = "";

                    if (p.IsValueTypePropertySingle())
                    {
                        stmt = string.Format("(({1}{2})obj).{0} = this.{0}", p.PropertyName, current.objClass.ClassName, Kistl.API.Helper.ImplementationSuffix);
                    }
                    else if (p.IsValueTypePropertyList())
                    {
                        stmt = string.Format("this._{0}.ApplyChanges((({1}{2})obj)._{0})", p.PropertyName, current.objClass.ClassName, Kistl.API.Helper.ImplementationSuffix);
                    }
                    else if (p.IsObjectReferencePropertySingle() && p.HasStorage())
                    {
                        stmt = string.Format("(({1}{2})obj).fk_{0} = this.fk_{0}", p.PropertyName, current.objClass.ClassName, Kistl.API.Helper.ImplementationSuffix);
                    }
                    else if (p.IsObjectReferencePropertyList() && p.HasStorage())
                    {
                        stmt = string.Format("this._{0}.ApplyChanges((({1}{2})obj)._{0})", p.PropertyName, current.objClass.ClassName, Kistl.API.Helper.ImplementationSuffix);
                    }
                    else if (p.IsObjectReferencePropertyList() && !p.HasStorage())
                    {
                        stmt = string.Format("if(this._{0} != null) this._{0}.ApplyChanges((({1}{2})obj)._{0}); else (({1}{2})obj)._{0} = null; (({1}{2})obj).NotifyPropertyChanged(\"{0}\")", p.PropertyName, current.objClass.ClassName, Kistl.API.Helper.ImplementationSuffix);
                    }

                    if (!string.IsNullOrEmpty(stmt))
                    {
                        m.Statements.AddExpression(stmt);
                    }
                }
            }

            // Create AttachToContext Method
            m = current.code_class.CreateOverrideMethod("AttachToContext", typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference("IKistlContext"), "ctx"));

            m.Statements.AddExpression("base.AttachToContext(ctx)");

            if (current.task == TaskEnum.Client)
            {
                foreach (Property p in current.objClass.Properties.OfType<Property>().Where(p => p.IsList))
                {
                    m.Statements.AddExpression(@"if(_{0} != null) _{0}.AttachToContext(ctx)", p.PropertyName);
                }
            }
            else
            {
                foreach (Property p in current.objClass.Properties.OfType<Property>().ToList().Where(p => p.IsList && p.HasStorage()))
                {
                    m.Statements.AddComment(@"Use ToList before using foreach - the collection will change in the KistContext.Attach() Method because EntityFramework will need a Trick to attach CollectionEntries correctly");
                    m.Statements.AddExpression(@"{0}{1}.ToList().ForEach<ICollectionEntry>(i => ctx.Attach(i))", p.PropertyName, Kistl.API.Helper.ImplementationSuffix);
                }
            }

            // Create GetPropertyError Method
            m = current.code_class.CreateOverrideMethod("GetPropertyError", typeof(string));
            m.Attributes = MemberAttributes.Family | MemberAttributes.Override;
            m.Parameters.Add(new CodeParameterDeclarationExpression(typeof(string), "prop"));

            if (current.objClass.Properties.Count > 0)
            {
                m.Statements.AddStatement("            switch(prop)");
                m.Statements.AddStatement("            {");

                foreach (BaseProperty p in current.objClass.Properties)
                {
                    m.Statements.AddStatement(@"                case ""{0}"":", p.PropertyName);
                    m.Statements.AddStatement(
@"                    return string.Join(""\n"", 
                        Context.GetReadonlyContext().Find<Kistl.App.Base.BaseProperty>({0}).Constraints
                            .Where(c => !c.IsValid(this, this.{1}))
                            .Select(c => c.GetErrorText(this, this.{1}))
                            .ToArray());",
                        p.ID, p.PropertyName);
                }

                m.Statements.AddStatement("            }");
            }
            m.Statements.AddExpression("return base.GetPropertyError(prop)");
        }
        #endregion

        #region GenerateMethods
        private void GenerateMethodsInternal(CurrentObjectClass current)
        {
            ObjectClass baseObjClass = current.objClass;
            ObjectClass objClass = current.objClass;
            while (objClass != null)
            {
                foreach (Method method in objClass.Methods)
                {
                    // TODO: I dont know if there's another way
                    // Default Methods, do not generate
                    if (method.IsDefaultMethod())
                    {
                        continue;
                    }

                    BaseParameter returnParam = method.Parameter.SingleOrDefault(p => p.IsReturnParameter);

                    if (objClass == baseObjClass)
                    {
                        // Create Delegate
                        // HACK!!! Die TypeParameter scheinen nicht zu funktionieren
                        CodeTypeDelegate d = new CodeTypeDelegate(method.MethodName + "_Handler<T>");

                        current.code_class.Members.Add(d);
                        d.Attributes = MemberAttributes.Public;

                        // HACK!!! Die TypeParameter scheinen nicht zu funktionieren
                        CodeTypeParameter ct = new CodeTypeParameter("T");
                        ct.Constraints.Add(new CodeTypeReference("IDataObject"));
                        d.TypeParameters.Add(ct);
                        // HACK!!! Die TypeParameter scheinen nicht zu funktionieren


                        d.Parameters.Add(new CodeParameterDeclarationExpression("T", "obj"));

                        if (returnParam != null)
                        {
                            d.Parameters.Add(new CodeParameterDeclarationExpression(
                                new CodeTypeReference("MethodReturnEventArgs", returnParam.ToCodeTypeReference()),
                                "e"));
                        }

                        // added inverse sort by ID to stabilise order of parameters
                        // TODO: implement orderable lists and backrefs instead!
                        foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter).OrderBy(p => -p.ID))
                        {
                            d.Parameters.Add(new CodeParameterDeclarationExpression(
                                param.ToCodeTypeReference(), param.ParameterName));
                        }
                    }

                    // Create event
                    CodeMemberEvent e = new CodeMemberEvent();
                    current.code_class.Members.Add(e);

                    e.Attributes = MemberAttributes.Public;
                    e.Type = new CodeTypeReference(method.MethodName + "_Handler",
                        new CodeTypeReference(baseObjClass.ClassName));
                    e.Name = "On" + method.MethodName + "_" + baseObjClass.ClassName;

                    // Create Method
                    CodeMemberMethod m = new CodeMemberMethod();
                    current.code_class.Members.Add(m);
                    m.Name = method.MethodName;
                    m.Attributes = (objClass == baseObjClass) ? (MemberAttributes.Public) : (MemberAttributes.Public | MemberAttributes.Override);

                    m.ReturnType = returnParam.ToCodeTypeReference();

                    // Add Parameter
                    StringBuilder methodCallParameter = new StringBuilder();
                    // added inverse sort by ID to stabilise order of parameters
                    // TODO: implement orderable lists and backrefs instead!
                    foreach (BaseParameter param in method.Parameter.Where(p => !p.IsReturnParameter).OrderBy(p => -p.ID))
                    {
                        m.Parameters.Add(new CodeParameterDeclarationExpression(
                            param.ToCodeTypeReference(), param.ParameterName));
                        if (methodCallParameter.Length > 0)
                        {
                            methodCallParameter.AppendFormat(", {0}", param.ParameterName);
                        }
                        else
                        {
                            methodCallParameter.AppendFormat(param.ParameterName);
                        }
                    }

                    if (returnParam != null)
                    {
                        m.Statements.AddExpression(
                            @"MethodReturnEventArgs<{0}> e = new MethodReturnEventArgs<{0}>()",
                            returnParam.ToCodeTypeReference().BaseType);

                    }

                    if (objClass != baseObjClass)
                    {
                        m.Statements.AddExpression(@"{2}base.{0}({1})",
                            method.MethodName,
                            methodCallParameter.ToString(),
                            returnParam != null ? "e.Result = " : "");
                    }

                    m.Statements.AddExpression(@"if (On{1}_{0} != null)
            {{
                On{1}_{0}(this{2}{3}{4});
            }}",
               baseObjClass.ClassName,
               method.MethodName,
               returnParam != null ? ", e" : "",
               methodCallParameter.Length == 0 ? "" : ", ",
               methodCallParameter.ToString());

                    if (returnParam != null)
                    {
                        m.Statements.AddExpression("return e.Result");
                    }
                }

                // Nächster bitte
                objClass = objClass.BaseObjectClass;
            }
        }
        #endregion

        #region GenerateStreamMethods
        private void GenerateStreamMethodsInternal(CurrentBase current, IEnumerable<BaseProperty> properties)
        {
            // Create ToStream Method
            CodeMemberMethod m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "ToStream";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryWriter)), "sw"));
            m.Statements.AddExpression("base.ToStream(sw)");

            #region ToStream
            foreach (BaseProperty p in properties)
            {
                if (p.IsEnumerationPropertySingle())
                {
                    m.Statements.AddExpression("BinarySerializer.ToBinary((int{1})this._{0}, sw)",
                        p.PropertyName, ((Property)p).IsNullable ? "?" : "");
                }
                else if (p.IsValueTypePropertySingle())
                {
                    m.Statements.AddExpression("BinarySerializer.ToBinary(this._{0}, sw)", p.PropertyName);
                }
                else if (p.IsValueTypePropertyList())
                {
                    if (current.task == TaskEnum.Client)
                        m.Statements.AddExpression("this._{0}.ToStream(sw)", p.PropertyName);
                    else
                        m.Statements.AddExpression("BinarySerializer.ToBinary(this.{0}{1}, sw)", p.PropertyName, Kistl.API.Helper.ImplementationSuffix);
                }
                else if (p.IsStructPropertySingle())
                {
                    m.Statements.AddExpression("BinarySerializer.ToBinary(this._{0}, sw)", p.PropertyName);
                }
                else if (p.IsObjectReferencePropertySingle() && p.HasStorage())
                {
                    m.Statements.AddExpression("BinarySerializer.ToBinary(this.fk_{0}, sw)", p.PropertyName);
                }
                else if (p.IsObjectReferencePropertyList() && p.HasStorage())
                {
                    if (current.task == TaskEnum.Client)
                        m.Statements.AddExpression("this._{0}.ToStream(sw)", p.PropertyName);
                    else
                        m.Statements.AddExpression("BinarySerializer.ToBinary(this.{0}{1}, sw)", p.PropertyName, Kistl.API.Helper.ImplementationSuffix);
                }
                else if (p.IsObjectReferencePropertyList() && !p.HasStorage()
                    && current.task == TaskEnum.Server
                    && false /*((ObjectReferenceProperty)p).PreFetchToClient*/ )
                {
                    m.Statements.AddExpression("BinarySerializer.ToBinary(this.{0}.Cast<IDataObject>(), sw)", p.PropertyName);
                }
            }
            #endregion

            m = new CodeMemberMethod();
            current.code_class.Members.Add(m);
            m.Name = "FromStream";
            m.Attributes = MemberAttributes.Public | MemberAttributes.Override;
            m.ReturnType = new CodeTypeReference(typeof(void));
            m.Parameters.Add(new CodeParameterDeclarationExpression(new CodeTypeReference(typeof(System.IO.BinaryReader)), "sr"));
            m.Statements.AddExpression("base.FromStream(sr)");

            #region FromStream
            foreach (BaseProperty p in properties)
            {
                if (p.IsListProperty() && p.HasStorage())
                {
                    if (current.task == TaskEnum.Client)
                        m.Statements.AddExpression("this._{0}.FromStream(sr)", p.PropertyName);
                    else
                        m.Statements.AddExpression("BinarySerializer.FromBinaryCollectionEntries(this.{0}{1}, sr)", p.PropertyName, Kistl.API.Helper.ImplementationSuffix);
                }
                else if (p is EnumerationProperty)
                {
                    m.Statements.AddExpression("int{2} tmp{0}; BinarySerializer.FromBinary(out tmp{0}, sr); _{0} = ({1})tmp{0}",
                        p.PropertyName, p.ToCodeTypeReference(current.task).BaseType, ((Property)p).IsNullable ? "?" : "");
                }
                else if (p is ValueTypeProperty)
                {
                    m.Statements.AddExpression("BinarySerializer.FromBinary(out this._{0}, sr)", p.PropertyName);
                }
                else if (p.IsStructPropertySingle())
                {
                    m.Statements.AddExpression(@"BinarySerializer.FromBinary(out this._{0}, sr); if (_{0} != null) _{0}.AttachToObject(this, ""{0}"")", p.PropertyName);
                }
                else if (p.IsObjectReferencePropertySingle() && p.HasStorage())
                {
                    m.Statements.AddExpression("BinarySerializer.FromBinary(out this._fk_{0}, sr)", p.PropertyName);
                }
                else if (p.IsObjectReferencePropertySingle() && !p.HasStorage() && false /*((BackReferenceProperty)p).PreFetchToClient*/)
                {
                    m.Statements.AddExpression("BinarySerializer.FromBinary(out this._fk_{0}, sr)", p.PropertyName);
                }
                else if (p.IsObjectReferencePropertyList() && !p.HasStorage()
                    && current.task == TaskEnum.Client
                    && false /*((BackReferenceProperty)p).PreFetchToClient*/)
                {
                    m.Statements.AddExpression(
                        "this._{0} = new BackReferenceCollection<{1}>(\"{2}\", this); BinarySerializer.FromBinary(this._{0}, sr)",
                        p.PropertyName, p.GetPropertyTypeString(),
                        ((ObjectReferenceProperty)p).GetOpposite().PropertyName);
                }
            }
            #endregion
        }
        #endregion
    }

    #region DataObjectGeneratorFactory
    public static class DataObjectGeneratorFactory
    {
        public static BaseDataObjectGenerator GetGenerator()
        {
            return new SQLServer.SQLServerDataObjectGenerator();
        }
    }
    #endregion
}
