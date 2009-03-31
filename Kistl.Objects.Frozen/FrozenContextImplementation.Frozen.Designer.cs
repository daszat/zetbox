using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;

namespace Kistl.App
{

	public class FrozenContextImplementation : IKistlContext
	{
		public FrozenContextImplementation()
		{
		}

		static FrozenContextImplementation()
		{
				Kistl.App.GUI.FrozenGUIRepository.CreateInstances();
				Kistl.App.Base.FrozenKistlBaseRepository.CreateInstances();

				Kistl.App.GUI.FrozenGUIRepository.FillDataStore();
				Kistl.App.Base.FrozenKistlBaseRepository.FillDataStore();
		}
		

		IPersistenceObject IKistlContext.Attach(IPersistenceObject obj)
		{
			throw new NotImplementedException();
		}
		
		void IKistlContext.Detach(IPersistenceObject obj)
		{
			throw new NotImplementedException();
		}

		void IKistlContext.Delete(IPersistenceObject obj)
		{
			throw new ReadOnlyContextException();
		}

		public IQueryable<T> GetQuery<T>()
			where T : class, IDataObject
		{
			return GetQuery(new InterfaceType(typeof(T))).Cast<T>();
		}

		public IQueryable<IDataObject> GetQuery(InterfaceType ifType)
		{
			if (ifType == typeof(Kistl.App.GUI.Icon))
				return Kistl.App.GUI.Icon__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Assembly))
				return Kistl.App.Base.Assembly__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.BaseParameter))
				return Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.BoolParameter))
				return Kistl.App.Base.BoolParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.BoolProperty))
				return Kistl.App.Base.BoolProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.CLRObjectParameter))
				return Kistl.App.Base.CLRObjectParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Constraint))
				return Kistl.App.Base.Constraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.DataType))
				return Kistl.App.Base.DataType__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.DateTimeParameter))
				return Kistl.App.Base.DateTimeParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.DateTimeProperty))
				return Kistl.App.Base.DateTimeProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.DoubleParameter))
				return Kistl.App.Base.DoubleParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.DoubleProperty))
				return Kistl.App.Base.DoubleProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Enumeration))
				return Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.EnumerationEntry))
				return Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.EnumerationProperty))
				return Kistl.App.Base.EnumerationProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.IntegerRangeConstraint))
				return Kistl.App.Base.IntegerRangeConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Interface))
				return Kistl.App.Base.Interface__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.IntParameter))
				return Kistl.App.Base.IntParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.IntProperty))
				return Kistl.App.Base.IntProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.IsValidIdentifierConstraint))
				return Kistl.App.Base.IsValidIdentifierConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.IsValidNamespaceConstraint))
				return Kistl.App.Base.IsValidNamespaceConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Method))
				return Kistl.App.Base.Method__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.MethodInvocation))
				return Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.MethodInvocationConstraint))
				return Kistl.App.Base.MethodInvocationConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Module))
				return Kistl.App.Base.Module__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.NotNullableConstraint))
				return Kistl.App.Base.NotNullableConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.ObjectClass))
				return Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.ObjectParameter))
				return Kistl.App.Base.ObjectParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.ObjectReferenceProperty))
				return Kistl.App.Base.ObjectReferenceProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Property))
				return Kistl.App.Base.Property__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.StringParameter))
				return Kistl.App.Base.StringParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.StringProperty))
				return Kistl.App.Base.StringProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.StringRangeConstraint))
				return Kistl.App.Base.StringRangeConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.Struct))
				return Kistl.App.Base.Struct__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.StructProperty))
				return Kistl.App.Base.StructProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.TypeRef))
				return Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.ValueTypeProperty))
				return Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			if (ifType == typeof(Kistl.App.Base.ViewDescriptor))
				return Kistl.App.Base.ViewDescriptor__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IDataObject>();
			throw new NotImplementedException();
		}

		List<T> IKistlContext.GetListOf<T>(IDataObject obj, string propertyName)
		{
			throw new NotImplementedException();
		}
		
        List<T> IKistlContext.GetListOf<T>(InterfaceType ifType, int ID, string propertyName)
		{
			throw new NotImplementedException();
		}
		
		ICollection<INewCollectionEntry<A, B>> IKistlContext.FetchRelation<A, B>(int relId, RelationEndRole role, IDataObject parent)
		{
			throw new NotImplementedException();
		}

        IPersistenceObject IKistlContext.ContainsObject(InterfaceType type, int ID)
        {
			return Find(type, ID);
        }

        public IEnumerable<IPersistenceObject> AttachedObjects
        {
			get
			{
				return new List<IPersistenceObject>(0)
					.Concat(GetQuery<Kistl.App.GUI.Icon>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Assembly>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.BaseParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.BoolParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.BoolProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.CLRObjectParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Constraint>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.DataType>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.DateTimeParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.DateTimeProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.DoubleParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.DoubleProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Enumeration>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.EnumerationEntry>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.EnumerationProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.IntegerRangeConstraint>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Interface>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.IntParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.IntProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.IsValidIdentifierConstraint>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.IsValidNamespaceConstraint>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Method>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.MethodInvocation>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.MethodInvocationConstraint>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Module>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.NotNullableConstraint>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.ObjectClass>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.ObjectParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.ObjectReferenceProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Property>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.StringParameter>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.StringProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.StringRangeConstraint>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.Struct>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.StructProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.TypeRef>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.ValueTypeProperty>().Cast<IPersistenceObject>())
					.Concat(GetQuery<Kistl.App.Base.ViewDescriptor>().Cast<IPersistenceObject>())
;
			}
		}

        int IKistlContext.SubmitChanges() { throw new NotImplementedException(); }

        bool IKistlContext.IsDisposed { get { return false; } }

        bool IKistlContext.IsReadonly { get { return true; } }

        IDataObject IKistlContext.Create(InterfaceType ifType) { throw new ReadOnlyContextException(); }
        T IKistlContext.Create<T>() { throw new ReadOnlyContextException(); }
        
        ICollectionEntry IKistlContext.CreateCollectionEntry(InterfaceType ifType) { throw new ReadOnlyContextException(); }
        T IKistlContext.CreateCollectionEntry<T>() { throw new ReadOnlyContextException(); }

        IStruct IKistlContext.CreateStruct(InterfaceType ifType) { throw new ReadOnlyContextException(); }
        T IKistlContext.CreateStruct<T>() { throw new ReadOnlyContextException(); }

        public IDataObject Find(InterfaceType ifType, int ID)
		{
			if (ifType == typeof(Kistl.App.GUI.Icon))
				return Kistl.App.GUI.Icon__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Assembly))
				return Kistl.App.Base.Assembly__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.BaseParameter))
				return Kistl.App.Base.BaseParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.BoolParameter))
				return Kistl.App.Base.BoolParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.BoolProperty))
				return Kistl.App.Base.BoolProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.CLRObjectParameter))
				return Kistl.App.Base.CLRObjectParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Constraint))
				return Kistl.App.Base.Constraint__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.DataType))
				return Kistl.App.Base.DataType__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.DateTimeParameter))
				return Kistl.App.Base.DateTimeParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.DateTimeProperty))
				return Kistl.App.Base.DateTimeProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.DoubleParameter))
				return Kistl.App.Base.DoubleParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.DoubleProperty))
				return Kistl.App.Base.DoubleProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Enumeration))
				return Kistl.App.Base.Enumeration__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.EnumerationEntry))
				return Kistl.App.Base.EnumerationEntry__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.EnumerationProperty))
				return Kistl.App.Base.EnumerationProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.IntegerRangeConstraint))
				return Kistl.App.Base.IntegerRangeConstraint__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Interface))
				return Kistl.App.Base.Interface__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.IntParameter))
				return Kistl.App.Base.IntParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.IntProperty))
				return Kistl.App.Base.IntProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.IsValidIdentifierConstraint))
				return Kistl.App.Base.IsValidIdentifierConstraint__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.IsValidNamespaceConstraint))
				return Kistl.App.Base.IsValidNamespaceConstraint__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Method))
				return Kistl.App.Base.Method__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.MethodInvocation))
				return Kistl.App.Base.MethodInvocation__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.MethodInvocationConstraint))
				return Kistl.App.Base.MethodInvocationConstraint__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Module))
				return Kistl.App.Base.Module__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.NotNullableConstraint))
				return Kistl.App.Base.NotNullableConstraint__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.ObjectClass))
				return Kistl.App.Base.ObjectClass__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.ObjectParameter))
				return Kistl.App.Base.ObjectParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.ObjectReferenceProperty))
				return Kistl.App.Base.ObjectReferenceProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Property))
				return Kistl.App.Base.Property__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.StringParameter))
				return Kistl.App.Base.StringParameter__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.StringProperty))
				return Kistl.App.Base.StringProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.StringRangeConstraint))
				return Kistl.App.Base.StringRangeConstraint__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.Struct))
				return Kistl.App.Base.Struct__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.StructProperty))
				return Kistl.App.Base.StructProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.TypeRef))
				return Kistl.App.Base.TypeRef__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.ValueTypeProperty))
				return Kistl.App.Base.ValueTypeProperty__Implementation__Frozen.DataStore[ID];
			if (ifType == typeof(Kistl.App.Base.ViewDescriptor))
				return Kistl.App.Base.ViewDescriptor__Implementation__Frozen.DataStore[ID];
			throw new NotImplementedException();
		}

        public T Find<T>(int ID)
			where T : class, IDataObject
        {
			return (T)Find(new InterfaceType(typeof(T)), ID);
        }


        IKistlContext IKistlContext.GetReadonlyContext() { throw new NotImplementedException(); }

        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectCreated
        {
			add { throw new ReadOnlyContextException(); }
			remove { throw new ReadOnlyContextException(); }
		}

        /// <summary>
        /// Is fired when an object is deleted in this Context.
        /// The delted object is passed as Data.
        /// </summary>
        event GenericEventHandler<IPersistenceObject> IKistlContext.ObjectDeleted
        {
			add { throw new ReadOnlyContextException(); }
			remove { throw new ReadOnlyContextException(); }
		}
		
		public virtual void Dispose() {}
	
	}
	
}