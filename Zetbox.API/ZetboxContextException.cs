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
    [Serializable]
    public abstract class ZetboxContextException
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
    }

    [Serializable]
    public abstract class ZetboxContextErrorException
        : ZetboxContextException
    {
        public ZetboxContextErrorException()
            : base()
        {
        }

        public ZetboxContextErrorException(string message)
            : base(message)
        {
        }

        public ZetboxContextErrorException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ZetboxContextErrorException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public abstract ZetboxContextExceptionMessage ToExceptionMessage();
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
    [DataContract]
    public class ConcurrencyExceptionDetail
    {
        public ConcurrencyExceptionDetail()
        {
        }

        public ConcurrencyExceptionDetail(Guid clsGuid, int id, string objectAsString, DateTime changedOn, string changedBy)
        {
            this.ClsGuid = clsGuid;
            this.ID = id;
            this.ObjectAsString = objectAsString;
            this.ChangedOn = changedOn;
            this.ChangedBy = changedBy;
        }

        [DataMember]
        public Guid ClsGuid { get; set; }
        [DataMember]
        public int ID { get; set; }
        [DataMember]
        public string ObjectAsString { get; set; }
        [DataMember]
        public DateTime ChangedOn { get; set; }
        [DataMember]
        public string ChangedBy { get; set; }
    }

    [Serializable]
    public class ConcurrencyException
        : ZetboxContextErrorException
    {
        private const string DEFAULT_MESSAGE = "At least one object has changed between fetch and submit changes";

        private static readonly List<ConcurrencyExceptionDetail> Empty = new List<ConcurrencyExceptionDetail>();

        private List<ConcurrencyExceptionDetail> _details;
        public List<ConcurrencyExceptionDetail> Details
        {
            get
            {
                return _details ?? Empty;
            }
            set
            {
                _details = value;
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

        public ConcurrencyException(List<ConcurrencyExceptionDetail> details)
            : base(string.Format("{0} object(s) has changed between fetch and submit changes", details != null ? details.Count().ToString() : "?"))
        {
            this._details = details;
        }

        public ConcurrencyException(string message, List<ConcurrencyExceptionDetail> details)
            : base(message)
        {
            this._details = details;
        }

        public override ZetboxContextExceptionMessage ToExceptionMessage()
        {
            return new ZetboxContextExceptionMessage()
            {
                Exception = new ConcurrencyExceptionMessage(this)
            };
        }
    }

    [Serializable]
    [DataContract]
    public class FKViolationExceptionDetail
    {
        public FKViolationExceptionDetail()
        {
        }

        
    }

    [Serializable]
    public class FKViolationException
        : ZetboxContextErrorException
    {
        private const string DEFAULT_MESSAGE = "At least one foreign key constraint has been violated";

        private static readonly List<FKViolationExceptionDetail> Empty = new List<FKViolationExceptionDetail>();

        private List<FKViolationExceptionDetail> _details;
        public List<FKViolationExceptionDetail> Details
        {
            get
            {
                return _details ?? Empty;
            }
            set
            {
                _details = value;
            }
        }

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

        public FKViolationException(List<FKViolationExceptionDetail> details)
            : base(string.Format("{0} object(s) has changed between fetch and submit changes", details != null ? details.Count().ToString() : "?"))
        {
            this._details = details;
        }

        public FKViolationException(string message, List<FKViolationExceptionDetail> details)
            : base(message)
        {
            this._details = details;
        }

        public override ZetboxContextExceptionMessage ToExceptionMessage()
        {
            return new ZetboxContextExceptionMessage()
            {
                Exception = new FKViolationExceptionMessage(this)
            };
        }
    }

    [Serializable]
    [DataContract]
    public class UniqueConstraintViolationExceptionDetail
    {
        public UniqueConstraintViolationExceptionDetail()
        {
        }


    }

    [Serializable]
    public class UniqueConstraintViolationException
        : ZetboxContextErrorException
    {
        private const string DEFAULT_MESSAGE = "At least one unique constraint has been violated";

        private static readonly List<UniqueConstraintViolationExceptionDetail> Empty = new List<UniqueConstraintViolationExceptionDetail>();

        private List<UniqueConstraintViolationExceptionDetail> _details;
        public List<UniqueConstraintViolationExceptionDetail> Details
        {
            get
            {
                return _details ?? Empty;
            }
            set
            {
                _details = value;
            }
        }

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

        public UniqueConstraintViolationException(List<UniqueConstraintViolationExceptionDetail> details)
            : base(string.Format("{0} object(s) has changed between fetch and submit changes", details != null ? details.Count().ToString() : "?"))
        {
            this._details = details;
        }

        public UniqueConstraintViolationException(string message, List<UniqueConstraintViolationExceptionDetail> details)
            : base(message)
        {
            this._details = details;
        }

        public override ZetboxContextExceptionMessage ToExceptionMessage()
        {
            return new ZetboxContextExceptionMessage()
            {
                Exception = new UniqueConstraintViolationExceptionMessage(this)
            };
        }
    }
}
