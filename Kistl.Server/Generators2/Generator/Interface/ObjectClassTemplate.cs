using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kistl.API;
using Kistl.API.Server;
using Kistl.App.Base;
using Kistl.Server.Generators;
using Kistl.Server.Generators.Helper;


namespace Kistl.Server.Generators2.EntityFramework.Interface
{
    public class ObjectClassTemplate : Kistl.Server.Generators2.Templates.Interface.ObjectClassTemplate
    {
        public ObjectClassTemplate(Arebis.CodeGeneration.IGenerationHost _host, ObjectClass objClass)
            : base(_host, objClass)
        {
            System.Diagnostics.Debugger.Launch();
        }

        protected override string GetBaseClass()
        {
            return base.GetBaseClass();
        }
    }
}
