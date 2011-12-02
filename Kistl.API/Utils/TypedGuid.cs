
namespace Kistl.API.Utils
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public struct TypedGuid<T>
        where T : class, IPersistenceObject
    {
        public TypedGuid(string guidString)
            : this(new Guid(guidString))
        {
        }

        public TypedGuid(Guid guid)
        {
            this._guid = guid;
        }

        private readonly Guid _guid;
        public Guid Guid { get { return _guid; } }

        public T Find(IKistlContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            return ctx.FindPersistenceObject<T>(_guid);
        }
    }


    public struct TypedGuidList<T>
        where T : class, IPersistenceObject
    {
        public TypedGuidList(params string[] guidStrings)
            : this(guidStrings.Select(s => new Guid(s)).ToArray())
        {
        }

        public TypedGuidList(params Guid[] guid)
        {
            this._guid = guid;
        }

        private readonly Guid[] _guid;
        public IEnumerable<Guid> Guid { get { return _guid; } }

        public IEnumerable<T> Find(IKistlContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException("ctx");

            return ctx.FindPersistenceObjects<T>(_guid);
        }
    }
}
