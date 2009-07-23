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
            if (m == null && obj.BaseObjectClass == null)
            {
                // Only for BaseClasses
                m = obj.Context.Create<Method>();
                m.MethodName = methodName;
                m.Module = kistlModule;
                obj.Methods.Add(m);
            }
            else if (m != null && obj.BaseObjectClass != null)
            {
                // Delete if BaseClass is declared
                obj.Context.Delete(m);
            }
        }

        private void EnsureDefaultMethods(ObjectClass obj)
        {
            Module kistlModule = obj.Context.GetQuery<Module>().First(md => md.ModuleName == "KistlBase");
            CheckDefaultMethod(obj, "ToString", kistlModule);
            CheckDefaultMethod(obj, "PreSave", kistlModule);
            CheckDefaultMethod(obj, "PostSave", kistlModule);
            CheckDefaultMethod(obj, "Created", kistlModule);
            CheckDefaultMethod(obj, "Deleting", kistlModule);
        }
        #endregion

        public void OnPreSave_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            EnsureDefaultMethods(obj);
        }

        public void OnCreated_ObjectClass(Kistl.App.Base.ObjectClass obj)
        {
            EnsureDefaultMethods(obj);
        }
        #endregion

        #region Relation
        public void OnCreated_Relation(Kistl.App.Base.Relation obj)
        {
            obj.A = obj.Context.Create<RelationEnd>();
            obj.B = obj.Context.Create<RelationEnd>();

            obj.A.Role = (int)RelationEndRole.A;
            obj.B.Role = (int)RelationEndRole.B;
        }
        #endregion
    }
}
