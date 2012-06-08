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

namespace Zetbox.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Text;

    using Zetbox.API.Utils;

    /// <summary>
    /// This exception is thrown when someone tries to modify a read only context.
    /// </summary>
    [Serializable]
    public class ReadOnlyContextException
        : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        public ReadOnlyContextException()
            : base("This context is readonly")
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        public ReadOnlyContextException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        /// <param name="inner">The exception that caused this exception.</param>
        public ReadOnlyContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyContextException class.
        /// </summary>
        protected ReadOnlyContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    /// <summary>
    /// This exception is thrown when someone tries to modify a read only object.
    /// </summary>
    [Serializable]
    public class ReadOnlyObjectException
        : NotSupportedException
    {
        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        public ReadOnlyObjectException()
            : base("This object is readonly")
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        public ReadOnlyObjectException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        /// <param name="message">A custom message to pass along with this exception.</param>
        /// <param name="inner">The exception that caused this exception.</param>
        public ReadOnlyObjectException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// Initializes a new instance of the ReadOnlyObjectException class.
        /// </summary>
        protected ReadOnlyObjectException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
