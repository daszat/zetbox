using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

using Kistl.API;
using System.Diagnostics;

namespace Kistl.App.Base
{
    public partial class CustomCommonActions_KistlBase
    {
        #region ObjectClass
        
        #region EnsureDefaultMethods
        private void CheckDefaultMethod(ObjectClass obj, string methodName, Module kistlModule)
        {
            var m = obj.Methods.SingleOrDefault(i => i.MethodName == methodName && i.Module == kistlModule);
            if (m == null)
            {
                m = obj.Context.Create<Method>();
                m.MethodName = methodName;
                m.Module = kistlModule;
                obj.Methods.Add(m);
            }
        }

        private void EnsureDefaultMethods(ObjectClass obj)
        {
            // Only for BaseClasses
            if (obj.BaseObjectClass == null)
            {
                Module kistlModule = obj.Context.GetQuery<Module>().First(md => md.ModuleName == "KistlBase");
                CheckDefaultMethod(obj, "ToString", kistlModule);
                CheckDefaultMethod(obj, "PreSave", kistlModule);
                CheckDefaultMethod(obj, "PostSave", kistlModule);
                CheckDefaultMethod(obj, "Created", kistlModule);
                CheckDefaultMethod(obj, "Deleting", kistlModule);
            }
        }
        #endregion
        
        public void OnPreSave_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            if (!System.CodeDom.Compiler.CodeGenerator.IsValidLanguageIndependentIdentifier(obj.ClassName))
            {
                throw new ArgumentException(string.Format("ClassName {0} has some illegal chars", obj.ClassName));
            }

            EnsureDefaultMethods(obj);
        }

        public void OnCreated_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            EnsureDefaultMethods(obj);
        }
        #endregion
    }
}
