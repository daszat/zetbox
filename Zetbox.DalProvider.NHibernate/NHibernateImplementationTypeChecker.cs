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

namespace Zetbox.DalProvider.NHibernate
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.API.Utils;
    using Zetbox.App.Base;

    public interface INHibernateImplementationTypeChecker
        : IImplementationTypeChecker
    {
    }

    public sealed class NHibernateImplementationType
        : ImplementationType
    {
        public delegate NHibernateImplementationType Factory(Type type);

        private readonly INHibernateImplementationTypeChecker _typeChecker;

        public NHibernateImplementationType(Type type, InterfaceType.Factory iftFactory, INHibernateImplementationTypeChecker typeChecker)
            : base(
                // translate from NHibernate dynamic proxies if type is not in the generated assembly, but the BaseType is.
                (type != null 
                    && typeChecker != null
                    && type.Assembly != typeChecker.GetType().Assembly 
                    && type.BaseType != null 
                    && type.BaseType.Assembly == typeChecker.GetType().Assembly)
                ? type.BaseType 
                : type, 
            iftFactory, 
            typeChecker)
        {
            _typeChecker = typeChecker;
        }

        public override InterfaceType ToInterfaceType()
        {
            if (Type.IsGenericType)
            {
                // convert args of things like Generic Collections
                Type genericType = Type.GetGenericTypeDefinition();
                var genericArguments = Type.GetGenericArguments().Select(t => new NHibernateImplementationType(t, IftFactory, _typeChecker).ToInterfaceType().Type).ToArray();
                return IftFactory(genericType.MakeGenericType(genericArguments));
            }
            else
            {
                // TODO: #1570 using wrong suffix
                var ifTypeName = Type.FullName.Replace("NHibernate" + Helper.ImplementationSuffix, String.Empty).Split('+')[0];
                return IftFactory(Type.GetType(ifTypeName + ", " + typeof(ObjectClass).Assembly.FullName, true));
            }
        }
    }
}
