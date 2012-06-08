
//namespace Zetbox.DalProvider.NHibernate.Generator.Templates.ObjectClasses
//{
//    using System;
//    using System.CodeDom;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Text;
//    using Zetbox.API;
//    using Zetbox.App.Base;
//    using Templates = Zetbox.Generator.Templates;

//    public class Method
//        : Templates.ObjectClasses.Method
//    {
//        public Method(Arebis.CodeGeneration.IGenerationHost _host, IZetboxContext ctx, DataType dt, Zetbox.App.Base.Method m, int index, string indexSuffix, string eventName)
//            : base(_host, ctx, dt, m, index, indexSuffix, eventName)
//        {
//        }

//        protected override MemberAttributes ModifyMemberAttributes(MemberAttributes methodAttributes)
//        {
//            // all methods have to be virtual to allow overriding by the proxy object
//            return base.ModifyMemberAttributes(methodAttributes) & ~MemberAttributes.Final;
//        }
//    }
//}
