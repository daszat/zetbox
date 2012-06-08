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

namespace Zetbox.DalProvider.Base
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Zetbox.API;

    public abstract class CompoundCollectionEntryBaseImpl<AType, AImplType, BType, BImplType>
        : CollectionEntryBaseImpl //, IValueCollectionEntry, IValueCollectionEntry<AType, BType>
        where AType : class, IDataObject
        where AImplType : class, IDataObject, AType
        where BType : class, ICompoundObject
        where BImplType : class, ICompoundObject, BType
    {
        protected CompoundCollectionEntryBaseImpl(Func<IFrozenContext> lazyCtx)
            : base(lazyCtx)
        {
            //fkA = null;
            //fkAGuid = null;
            //fkB = null;
            //fkBGuid = null;
        }

        //#region A-Reference

        //public AType A
        //{
        //    get
        //    {
        //        return AImpl;
        //    }
        //    set
        //    {
        //        // TODO: NotifyPropertyChanged()
        //        if (((IPersistenceObject)this).IsReadonly)
        //            throw new ReadOnlyObjectException();
        //        if (value != null && value.Context != this.Context)
        //            throw new WrongZetboxContextException();

        //        // only accept "our" types
        //        AImpl = (AImplType)value;
        //    }
        //}

        //public abstract AImplType AImpl
        //{
        //    get;
        //    set;
        //}

        //IDataObject IValueCollectionEntry.ParentObject
        //{
        //    get
        //    {
        //        return A;
        //    }
        //    set
        //    {
        //        A = (AType)value;
        //    }
        //}

        //AType IValueCollectionEntry<AType, BType>.Parent
        //{
        //    get
        //    {
        //        return A;
        //    }
        //    set
        //    {
        //        A = (AType)value;
        //    }
        //}

        //protected int? fkA { get; set; }
        //protected Guid? fkAGuid { get; set; }

        //#endregion

        //#region B-Reference

        //public BType B
        //{
        //    get
        //    {
        //        return BImpl;
        //    }
        //    set
        //    {
        //        // TODO: NotifyPropertyChanged()
        //        if (((IPersistenceObject)this).IsReadonly)
        //            throw new ReadOnlyObjectException();
        //        //if (value != null && value.Context != this.Context)
        //        //    throw new WrongZetboxContextException();

        //        // only accept "our" types
        //        BImpl = (BImplType)value;
        //    }
        //}

        //public abstract BImplType BImpl
        //{
        //    get;
        //    set;
        //}

        //object IValueCollectionEntry.ValueObject
        //{
        //    get
        //    {
        //        return B;
        //    }
        //    set
        //    {
        //        B = (BType)value;
        //    }
        //}

        //BType IValueCollectionEntry<AType, BType>.Value
        //{
        //    get
        //    {
        //        return B;
        //    }
        //    set
        //    {
        //        B = (BType)value;
        //    }
        //}

        //protected int? fkB { get; set; }
        //protected Guid? fkBGuid { get; set; }

        //#endregion
    }
}
