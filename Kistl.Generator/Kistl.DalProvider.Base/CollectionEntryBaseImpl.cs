
namespace Kistl.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Kistl.API;

    public abstract class CollectionEntryBaseImpl : PersistenceObjectBaseImpl
    {
        protected CollectionEntryBaseImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
        }

        /// <summary>
        /// Always returns <value>true</value>. CollectionEntries are checked via their navigators or relations.
        /// </summary>
        /// <returns><value>true</value></returns>
        public override bool IsValid()
        {
            return true;
        }

        /// <summary>
        /// Always returns <value>true</value>. CollectionEntries are checked via their navigators or relations.
        /// </summary>
        /// <param name="prop">is ignored</param>
        /// <returns><value>true</value></returns>
        protected override string GetPropertyError(string prop)
        {
            return String.Empty;
        }
    }
}
