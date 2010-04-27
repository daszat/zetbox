
namespace Kistl.API.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    
    using Kistl.API.Configuration;
    
    using NUnit.Framework;

    public class ConfigTestApplicationContext : ApplicationContext
    {
        public ConfigTestApplicationContext()
            : base(HostType.None)
        {

        }
    }

    public class CustomActionsTestApplicationContext : ApplicationContext
    {
        public CustomActionsTestApplicationContext()
            : base(HostType.None)
        { }
    }

    public class TypesTestApplicationContext : ApplicationContext
    {
        public TypesTestApplicationContext()
            : base(HostType.None)
        { }

        // public Type BasePersistenceObjectType { get; protected set; }
        // public Type BaseDataObjectType { get; protected set; }
        // public Type BaseCompoundObjectType { get; protected set; }
        // public Type BaseCollectionEntryType { get; protected set; }

        internal void SetBpoType(Type t) { this.BasePersistenceObjectType = t; }
        internal void SetBdoType(Type t) { this.BaseDataObjectType = t; }
        internal void SetBsoType(Type t) { this.BaseCompoundObjectType = t; }
        internal void SetBceType(Type t) { this.BaseCollectionEntryType = t; }
    }
}
