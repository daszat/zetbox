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
