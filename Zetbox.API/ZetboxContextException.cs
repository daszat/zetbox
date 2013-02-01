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
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Zetbox.API.Async;
using System.Xml.Serialization;

namespace Zetbox.API
{
    #region Serialization helper
    [Serializable]
    [XmlRoot(Namespace = "http://dasz.at/zetbox/ZetboxContextExceptionSerializationHelper")]
    public class ExceptionSerializationHelperContainer
    {
        [XmlElement(Type = typeof(ConcurrencyExceptionSerializationHelper), ElementName = "ConcurrencyExceptionSerializationHelper")]
        [XmlElement(Type = typeof(FKViolationExceptionSerializationHelper), ElementName = "FKViolationExceptionSerializationHelper")]
        [XmlElement(typeof(UniqueConstraintViolationExceptionSerializationHelper), ElementName = "UniqueConstraintViolationExceptionSerializationHelper")]
        public ZetboxContextExceptionSerializationHelper Exception { get; set; }
    }

    [Serializable]
    public class ZetboxContextExceptionSerializationHelper
    {
        public ZetboxContextExceptionSerializationHelper()
        {
        }

        public ZetboxContextExceptionSerializationHelper(Exception ex)
        {
            if (ex == null) throw new ArgumentNullException("ex");
            this.Message = ex.Message;
        }

        public string Message { get; set; }

        public virtual ZetboxContextException ToException()
        {
            throw new NotImplementedException("Must be implemented in derived classes");
        }
    }

    [Serializable]
    public class ConcurrencyExceptionSerializationHelper : ZetboxContextExceptionSerializationHelper
    {
        public ConcurrencyExceptionSerializationHelper()
        {

        }

        public ConcurrencyExceptionSerializationHelper(Exception ex)
            : base(ex)
        {
        }

        public override ZetboxContextException ToException()
        {
            return new ConcurrencyException(Message);
        }
    }

    [Serializable]
    public class FKViolationExceptionSerializationHelper : ZetboxContextExceptionSerializationHelper
    {
        public FKViolationExceptionSerializationHelper()
        {

        }

        public FKViolationExceptionSerializationHelper(Exception ex)
            : base(ex)
        {
        }

        public override ZetboxContextException ToException()
        {
            return new FKViolationException(Message);
        }
    }

    [Serializable]
    public class UniqueConstraintViolationExceptionSerializationHelper : ZetboxContextExceptionSerializationHelper
    {
        public UniqueConstraintViolationExceptionSerializationHelper()
        {

        }

        public UniqueConstraintViolationExceptionSerializationHelper(Exception ex)
            : base(ex)
        {
        }
        public override ZetboxContextException ToException()
        {
            return new UniqueConstraintViolationException(Message);
        }
    }

    #endregion

    [Serializable]
    public class ZetboxContextException
        : Exception
    {
        public ZetboxContextException()
            : base()
        {
        }

        public ZetboxContextException(string message)
            : base(message)
        {
        }

        public ZetboxContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ZetboxContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public virtual void ToXmlStream(System.IO.Stream s)
        {
            throw new NotImplementedException();
        }
    }

    [Serializable]
    public class ZetboxContextDisposedException
        : ZetboxContextException
    {
        public ZetboxContextDisposedException()
            : base("Context has been disposed. Reusing is not allowed.")
        {
        }

        public ZetboxContextDisposedException(string message)
            : base(message)
        {
        }

        public ZetboxContextDisposedException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ZetboxContextDisposedException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class WrongZetboxContextException
        : ZetboxContextException
    {
        public WrongZetboxContextException()
            : base("Operation on a Context, where the IPersistanceObject does not belong to is not allowed")
        {
        }

        public WrongZetboxContextException(string message)
            : base(message)
        {
        }

        public WrongZetboxContextException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected WrongZetboxContextException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }

    [Serializable]
    public class ConcurrencyException
        : ZetboxContextException
    {
        private const string DEFAULT_MESSAGE = "At least one object has changed between fetch and submit changes";

        [NonSerialized]
        private IEnumerable<IDataObject> objects;

        [XmlIgnore]
        public IEnumerable<IDataObject> Objects
        {
            get
            {
                return objects;
            }
        }

        public ConcurrencyException()
            : base(DEFAULT_MESSAGE)
        {
        }

        public ConcurrencyException(string message)
            : base(message)
        {
        }

        public ConcurrencyException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public ConcurrencyException(Exception inner)
            : base(DEFAULT_MESSAGE, inner)
        {
        }

        protected ConcurrencyException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public ConcurrencyException(IEnumerable<IDataObject> objects)
            : base(string.Format("{0} object(s) has changed between fetch and submit changes", objects != null ? objects.Count().ToString() : "?"))
        {
            this.objects = objects;
        }

        public override void ToXmlStream(System.IO.Stream s)
        {
            new ExceptionSerializationHelperContainer()
            {
                Exception = new ConcurrencyExceptionSerializationHelper(this)
            }.ToXmlStream(s);
        }
    }

    [Serializable]
    public class FKViolationException
        : ZetboxContextException
    {
        private const string DEFAULT_MESSAGE = "At least one foreign key constraint has been violated";

        public FKViolationException()
            : base(DEFAULT_MESSAGE)
        {
        }

        public FKViolationException(string message)
            : base(message)
        {
        }

        public FKViolationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public FKViolationException(Exception inner)
            : base(DEFAULT_MESSAGE, inner)
        {
        }

        protected FKViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void ToXmlStream(System.IO.Stream s)
        {
            new ExceptionSerializationHelperContainer()
            {
                Exception = new FKViolationExceptionSerializationHelper(this)
            }.ToXmlStream(s);
        }
    }

    [Serializable]
    public class UniqueConstraintViolationException
        : ZetboxContextException
    {
        private const string DEFAULT_MESSAGE = "At least one unique constraint has been violated";

        public UniqueConstraintViolationException()
            : base(DEFAULT_MESSAGE)
        {
        }

        public UniqueConstraintViolationException(string message)
            : base(message)
        {
        }

        public UniqueConstraintViolationException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public UniqueConstraintViolationException(Exception inner)
            : base(DEFAULT_MESSAGE, inner)
        {
        }

        protected UniqueConstraintViolationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public override void ToXmlStream(System.IO.Stream s)
        {
            new ExceptionSerializationHelperContainer()
            {
                Exception = new UniqueConstraintViolationExceptionSerializationHelper(this)
            }.ToXmlStream(s);
        }
    }
}
