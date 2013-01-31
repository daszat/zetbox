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

namespace Zetbox.Server
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using Autofac;
    using Zetbox.API;
    using Zetbox.API.Utils;

    /// <summary>
    /// Server Helper
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Handles an Error
        /// </summary>
        /// <param name="ex">Exception to handle</param>
        public static void ThrowFaultException(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");
            Logging.Log.Error("Error in Facade: " + ex.Message, ex);

            if (ex is ConcurrencyException)
            {
                throw new FaultException<ConcurrencyException>((ConcurrencyException)ex);
            }
            else if (ex is FKViolationException)
            {
                throw new FaultException<FKViolationException>((FKViolationException)ex);
            }
            else if (ex is UniqueConstraintViolationException)
            {
                throw new FaultException<UniqueConstraintViolationException>((UniqueConstraintViolationException)ex);
            }
            else if (ex is InvalidZetboxGeneratedVersionException)
            {
                throw new FaultException<InvalidZetboxGeneratedVersionException>((InvalidZetboxGeneratedVersionException)ex);
            }
            else
            {
#if DEBUG
                if (ex is System.Data.DataException && ex.InnerException != null)
                {
                    throw new FaultException(ex.InnerException.Message);
                }
                else
                {
                    throw new FaultException(ex.Message);
                }
#else
                throw new FaultException("An error ocurred while processing this request.");
#endif
            }
        }
    }
}
