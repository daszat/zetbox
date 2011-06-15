
namespace Kistl.DalProvider.NHibernate.Generator.Templates.Mappings
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Arebis.CodeGeneration;
    using Kistl.API;
    using Kistl.App.Base;

    public partial class JoinedSubclassHbm
    {
        public static void Call(IGenerationHost host, IKistlContext ctx, ObjectClass cls)
        {
            if (host == null) { throw new ArgumentNullException("host"); }
            if (ctx == null) { throw new ArgumentNullException("ctx"); }
            if (cls == null) { throw new ArgumentNullException("cls"); }
            if (cls.BaseObjectClass == null)
            {
                var msg = String.Format("Only subclasses can be joined subclasses, but {0} doesn't have a base class",
                        cls.Name);
                throw new ArgumentOutOfRangeException("cls", msg);
            }

            host.CallTemplate("Mappings.JoinedSubclassHbm",
                new object[] { ctx }
                    .Concat(ObjectClassHbm.MakeArgs(ctx, cls, host.Settings))
                    .ToArray());
        }

        protected virtual void ApplyPropertyDefinitions(List<Property> properties)
        {
            PropertiesHbm.Call(Host, ctx, String.Empty, properties);
        }

        protected virtual void ApplyJoinedSubclasses(List<ObjectClass> subClasses)
        {
            foreach (var subClass in subClasses.OrderBy(cls => cls.Name))
            {
                JoinedSubclassHbm.Call(Host, ctx, subClass);
            }
        }
    }
}
