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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using Zetbox.API;
using Zetbox.API.Client;
using Zetbox.Client.ASPNET.Toolkit;

namespace Zetbox.Client.ASPNET.Toolkit
{
    [ServiceContract(Namespace = "Zetbox.Client.ASPNET")] // Client side namespace
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class AJAXService
    {
        [OperationContract]
        public List<JavaScriptObjectMoniker> GetList(SerializableType type)
        {
            if (type == null) { throw new ArgumentNullException("type"); }

            throw new NotImplementedException();
            //try
            //{
            //    return ZetboxContextManagerModule.ZetboxContext.GetQuery(ZetboxContextManagerModule.IftFactory(type.GetSystemType()))
            //        .Select(i => new JavaScriptObjectMoniker(ZetboxContextManagerModule.ZetboxContext, i)).ToList();
            //}
            //catch (Exception ex)
            //{
            //    throw new FaultException(ex.Message);
            //}
        }
    }
}