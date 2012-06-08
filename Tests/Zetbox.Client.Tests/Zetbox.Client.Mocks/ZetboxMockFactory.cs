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

namespace Zetbox.Client.Mocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using Zetbox.API;
    using Zetbox.App.Base;
    using Zetbox.App.GUI;

    using Moq;
    using Zetbox.Client.Presentables;

    public class ZetboxMockFactory
    {
        public ZetboxMockFactory(InterfaceType.Factory iftFactory)
        {
        }

        public Mock<ObjectReferenceProperty> CreateObjectReferenceProperty(
            string propertyName,
            bool isList
            )
        {
            var result = new Mock<ObjectReferenceProperty>();

            result.SetupAllProperties();
            result.Setup(orp => orp.GetPropertyTypeString()).Returns(typeof(ObjectReferenceProperty).FullName);
            result.Setup(orp => orp.Name).Returns(propertyName);
            result.Setup(orp => orp.GetIsList()).Returns(isList);

            return result;
        }

        public Mock<TestObject> CreateTestObject()
        {
            var result = new Mock<TestObject>();
            //result.Setup(obj => obj.GetInterfaceType()).Returns(new InterfaceType(typeof(TestObject)));
            return result;
        }

        public static Mock<IZetboxContext> CreateContext()
        {
            var result = new Mock<IZetboxContext>();
            return result;
        }

        public static Mock<IViewModelFactory> CreateFactory(Dictionary<IDataObject, ViewModel> backingStore)
        {
            var result = new Mock<IViewModelFactory>();
            // TODO: ????
            //result.Setup(mf => mf.CreateDefaultModel(It.IsAny<IZetboxContext>(), It.IsAny<IDataObject>(), It.Is<object>(o => o == null))).Returns((IZetboxContext ctx, IDataObject obj, object nothing) =>
            //{
            //    if (backingStore.ContainsKey(obj))
            //    {
            //        return backingStore[obj];
            //    }
            //    else
            //    {
            //        return (backingStore[obj] = new DataObjectViewModel(null, ctx, obj));
            //    }
            //});
            return result;
        }
    }
}
