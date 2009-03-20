using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;

namespace Kistl.App.Base
{

	public static class FrozenKistlBaseRepository
	{
		
		/// <summary>Frozen List of all Assembly</summary>
		/// 
		public static IQueryable<Assembly> Assemblies
		{ 
			get
			{
				return Assembly__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Assembly>();
			}
		}
		
		/// <summary>Frozen List of all BackReferenceProperty</summary>
		/// Metadefinition Object for BackReference Properties.
		public static IQueryable<BackReferenceProperty> BackReferenceProperties
		{ 
			get
			{
				return BackReferenceProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<BackReferenceProperty>();
			}
		}
		
		/// <summary>Frozen List of all BaseParameter</summary>
		/// Metadefinition Object for Parameter. This class is abstract.
		public static IQueryable<BaseParameter> BaseParameters
		{ 
			get
			{
				return BaseParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<BaseParameter>();
			}
		}
		
		/// <summary>Frozen List of all BaseProperty</summary>
		/// Metadefinition Object for Properties. This class is abstract.
		public static IQueryable<BaseProperty> BaseProperties
		{ 
			get
			{
				return BaseProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<BaseProperty>();
			}
		}
		
		/// <summary>Frozen List of all BoolParameter</summary>
		/// Metadefinition Object for Bool Parameter.
		public static IQueryable<BoolParameter> BoolParameters
		{ 
			get
			{
				return BoolParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<BoolParameter>();
			}
		}
		
		/// <summary>Frozen List of all BoolProperty</summary>
		/// Metadefinition Object for Bool Properties.
		public static IQueryable<BoolProperty> BoolProperties
		{ 
			get
			{
				return BoolProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<BoolProperty>();
			}
		}
		
		/// <summary>Frozen List of all CLRObjectParameter</summary>
		/// Metadefinition Object for CLR Object Parameter.
		public static IQueryable<CLRObjectParameter> CLRObjectParameters
		{ 
			get
			{
				return CLRObjectParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<CLRObjectParameter>();
			}
		}
		
		/// <summary>Frozen List of all Constraint</summary>
		/// 
		public static IQueryable<Constraint> Constraints
		{ 
			get
			{
				return Constraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Constraint>();
			}
		}
		
		/// <summary>Frozen List of all DataType</summary>
		/// Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.
		public static IQueryable<DataType> DataTypes
		{ 
			get
			{
				return DataType__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<DataType>();
			}
		}
		
		/// <summary>Frozen List of all DateTimeParameter</summary>
		/// Metadefinition Object for DateTime Parameter.
		public static IQueryable<DateTimeParameter> DateTimeParameters
		{ 
			get
			{
				return DateTimeParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<DateTimeParameter>();
			}
		}
		
		/// <summary>Frozen List of all DateTimeProperty</summary>
		/// Metadefinition Object for DateTime Properties.
		public static IQueryable<DateTimeProperty> DateTimeProperties
		{ 
			get
			{
				return DateTimeProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<DateTimeProperty>();
			}
		}
		
		/// <summary>Frozen List of all DoubleParameter</summary>
		/// Metadefinition Object for Double Parameter.
		public static IQueryable<DoubleParameter> DoubleParameters
		{ 
			get
			{
				return DoubleParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<DoubleParameter>();
			}
		}
		
		/// <summary>Frozen List of all DoubleProperty</summary>
		/// Metadefinition Object for Double Properties.
		public static IQueryable<DoubleProperty> DoubleProperties
		{ 
			get
			{
				return DoubleProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<DoubleProperty>();
			}
		}
		
		/// <summary>Frozen List of all Enumeration</summary>
		/// Metadefinition Object for Enumerations.
		public static IQueryable<Enumeration> Enumerations
		{ 
			get
			{
				return Enumeration__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Enumeration>();
			}
		}
		
		/// <summary>Frozen List of all EnumerationEntry</summary>
		/// Metadefinition Object for an Enumeration Entry.
		public static IQueryable<EnumerationEntry> EnumerationEntries
		{ 
			get
			{
				return EnumerationEntry__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<EnumerationEntry>();
			}
		}
		
		/// <summary>Frozen List of all EnumerationProperty</summary>
		/// Metadefinition Object for Enumeration Properties.
		public static IQueryable<EnumerationProperty> EnumerationProperties
		{ 
			get
			{
				return EnumerationProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<EnumerationProperty>();
			}
		}
		
		/// <summary>Frozen List of all IntegerRangeConstraint</summary>
		/// 
		public static IQueryable<IntegerRangeConstraint> IntegerRangeConstraints
		{ 
			get
			{
				return IntegerRangeConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IntegerRangeConstraint>();
			}
		}
		
		/// <summary>Frozen List of all Interface</summary>
		/// Metadefinition Object for Interfaces.
		public static IQueryable<Interface> Interfaces
		{ 
			get
			{
				return Interface__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Interface>();
			}
		}
		
		/// <summary>Frozen List of all IntParameter</summary>
		/// Metadefinition Object for Int Parameter.
		public static IQueryable<IntParameter> IntParameters
		{ 
			get
			{
				return IntParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IntParameter>();
			}
		}
		
		/// <summary>Frozen List of all IntProperty</summary>
		/// Metadefinition Object for Int Properties.
		public static IQueryable<IntProperty> IntProperties
		{ 
			get
			{
				return IntProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IntProperty>();
			}
		}
		
		/// <summary>Frozen List of all IsValidIdentifierConstraint</summary>
		/// 
		public static IQueryable<IsValidIdentifierConstraint> IsValidIdentifierConstraints
		{ 
			get
			{
				return IsValidIdentifierConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IsValidIdentifierConstraint>();
			}
		}
		
		/// <summary>Frozen List of all IsValidNamespaceConstraint</summary>
		/// 
		public static IQueryable<IsValidNamespaceConstraint> IsValidNamespaceConstraints
		{ 
			get
			{
				return IsValidNamespaceConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<IsValidNamespaceConstraint>();
			}
		}
		
		/// <summary>Frozen List of all Method</summary>
		/// Metadefinition Object for Methods.
		public static IQueryable<Method> Methods
		{ 
			get
			{
				return Method__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Method>();
			}
		}
		
		/// <summary>Frozen List of all MethodInvocation</summary>
		/// Metadefinition Object for a MethodInvocation on a Method of a DataType.
		public static IQueryable<MethodInvocation> MethodInvocations
		{ 
			get
			{
				return MethodInvocation__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<MethodInvocation>();
			}
		}
		
		/// <summary>Frozen List of all MethodInvocationConstraint</summary>
		/// 
		public static IQueryable<MethodInvocationConstraint> MethodInvocationConstraints
		{ 
			get
			{
				return MethodInvocationConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<MethodInvocationConstraint>();
			}
		}
		
		/// <summary>Frozen List of all Module</summary>
		/// Metadefinition Object for Modules.
		public static IQueryable<Module> Modules
		{ 
			get
			{
				return Module__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Module>();
			}
		}
		
		/// <summary>Frozen List of all NotNullableConstraint</summary>
		/// 
		public static IQueryable<NotNullableConstraint> NotNullableConstraints
		{ 
			get
			{
				return NotNullableConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<NotNullableConstraint>();
			}
		}
		
		/// <summary>Frozen List of all ObjectClass</summary>
		/// Metadefinition Object for ObjectClasses.
		public static IQueryable<ObjectClass> ObjectClasses
		{ 
			get
			{
				return ObjectClass__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<ObjectClass>();
			}
		}
		
		/// <summary>Frozen List of all ObjectParameter</summary>
		/// Metadefinition Object for Object Parameter.
		public static IQueryable<ObjectParameter> ObjectParameters
		{ 
			get
			{
				return ObjectParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<ObjectParameter>();
			}
		}
		
		/// <summary>Frozen List of all ObjectReferenceProperty</summary>
		/// Metadefinition Object for ObjectReference Properties.
		public static IQueryable<ObjectReferenceProperty> ObjectReferenceProperties
		{ 
			get
			{
				return ObjectReferenceProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<ObjectReferenceProperty>();
			}
		}
		
		/// <summary>Frozen List of all Property</summary>
		/// Metadefinition Object for Properties. This class is abstract.
		public static IQueryable<Property> Properties
		{ 
			get
			{
				return Property__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Property>();
			}
		}
		
		/// <summary>Frozen List of all StringParameter</summary>
		/// Metadefinition Object for String Parameter.
		public static IQueryable<StringParameter> StringParameters
		{ 
			get
			{
				return StringParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<StringParameter>();
			}
		}
		
		/// <summary>Frozen List of all StringProperty</summary>
		/// Metadefinition Object for String Properties.
		public static IQueryable<StringProperty> StringProperties
		{ 
			get
			{
				return StringProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<StringProperty>();
			}
		}
		
		/// <summary>Frozen List of all StringRangeConstraint</summary>
		/// 
		public static IQueryable<StringRangeConstraint> StringRangeConstraints
		{ 
			get
			{
				return StringRangeConstraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<StringRangeConstraint>();
			}
		}
		
		/// <summary>Frozen List of all Struct</summary>
		/// Metadefinition Object for Structs.
		public static IQueryable<Struct> Structs
		{ 
			get
			{
				return Struct__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Struct>();
			}
		}
		
		/// <summary>Frozen List of all StructProperty</summary>
		/// Metadefinition Object for Struct Properties.
		public static IQueryable<StructProperty> StructProperties
		{ 
			get
			{
				return StructProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<StructProperty>();
			}
		}
		
		/// <summary>Frozen List of all TypeRef</summary>
		/// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
		public static IQueryable<TypeRef> TypeRefs
		{ 
			get
			{
				return TypeRef__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<TypeRef>();
			}
		}
		
		/// <summary>Frozen List of all ValueTypeProperty</summary>
		/// Metadefinition Object for ValueType Properties. This class is abstract.
		public static IQueryable<ValueTypeProperty> ValueTypeProperties
		{ 
			get
			{
				return ValueTypeProperty__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<ValueTypeProperty>();
			}
		}
		
		/// <summary>Frozen List of all ViewDescriptor</summary>
		/// 
		public static IQueryable<ViewDescriptor> ViewDescriptors
		{ 
			get
			{
				return ViewDescriptor__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<ViewDescriptor>();
			}
		}
		

		internal static void CreateInstances()
		{
				Assembly__Implementation__Frozen.CreateInstances();
				BackReferenceProperty__Implementation__Frozen.CreateInstances();
				BaseParameter__Implementation__Frozen.CreateInstances();
				BaseProperty__Implementation__Frozen.CreateInstances();
				BoolParameter__Implementation__Frozen.CreateInstances();
				BoolProperty__Implementation__Frozen.CreateInstances();
				CLRObjectParameter__Implementation__Frozen.CreateInstances();
				Constraint__Implementation__Frozen.CreateInstances();
				DataType__Implementation__Frozen.CreateInstances();
				DateTimeParameter__Implementation__Frozen.CreateInstances();
				DateTimeProperty__Implementation__Frozen.CreateInstances();
				DoubleParameter__Implementation__Frozen.CreateInstances();
				DoubleProperty__Implementation__Frozen.CreateInstances();
				Enumeration__Implementation__Frozen.CreateInstances();
				EnumerationEntry__Implementation__Frozen.CreateInstances();
				EnumerationProperty__Implementation__Frozen.CreateInstances();
				IntegerRangeConstraint__Implementation__Frozen.CreateInstances();
				Interface__Implementation__Frozen.CreateInstances();
				IntParameter__Implementation__Frozen.CreateInstances();
				IntProperty__Implementation__Frozen.CreateInstances();
				IsValidIdentifierConstraint__Implementation__Frozen.CreateInstances();
				IsValidNamespaceConstraint__Implementation__Frozen.CreateInstances();
				Method__Implementation__Frozen.CreateInstances();
				MethodInvocation__Implementation__Frozen.CreateInstances();
				MethodInvocationConstraint__Implementation__Frozen.CreateInstances();
				Module__Implementation__Frozen.CreateInstances();
				NotNullableConstraint__Implementation__Frozen.CreateInstances();
				ObjectClass__Implementation__Frozen.CreateInstances();
				ObjectParameter__Implementation__Frozen.CreateInstances();
				ObjectReferenceProperty__Implementation__Frozen.CreateInstances();
				Property__Implementation__Frozen.CreateInstances();
				StringParameter__Implementation__Frozen.CreateInstances();
				StringProperty__Implementation__Frozen.CreateInstances();
				StringRangeConstraint__Implementation__Frozen.CreateInstances();
				Struct__Implementation__Frozen.CreateInstances();
				StructProperty__Implementation__Frozen.CreateInstances();
				TypeRef__Implementation__Frozen.CreateInstances();
				ValueTypeProperty__Implementation__Frozen.CreateInstances();
				ViewDescriptor__Implementation__Frozen.CreateInstances();
		}


		internal static void FillDataStore()
		{
				Assembly__Implementation__Frozen.FillDataStore();
				BackReferenceProperty__Implementation__Frozen.FillDataStore();
				BaseParameter__Implementation__Frozen.FillDataStore();
				BaseProperty__Implementation__Frozen.FillDataStore();
				BoolParameter__Implementation__Frozen.FillDataStore();
				BoolProperty__Implementation__Frozen.FillDataStore();
				CLRObjectParameter__Implementation__Frozen.FillDataStore();
				Constraint__Implementation__Frozen.FillDataStore();
				DataType__Implementation__Frozen.FillDataStore();
				DateTimeParameter__Implementation__Frozen.FillDataStore();
				DateTimeProperty__Implementation__Frozen.FillDataStore();
				DoubleParameter__Implementation__Frozen.FillDataStore();
				DoubleProperty__Implementation__Frozen.FillDataStore();
				Enumeration__Implementation__Frozen.FillDataStore();
				EnumerationEntry__Implementation__Frozen.FillDataStore();
				EnumerationProperty__Implementation__Frozen.FillDataStore();
				IntegerRangeConstraint__Implementation__Frozen.FillDataStore();
				Interface__Implementation__Frozen.FillDataStore();
				IntParameter__Implementation__Frozen.FillDataStore();
				IntProperty__Implementation__Frozen.FillDataStore();
				IsValidIdentifierConstraint__Implementation__Frozen.FillDataStore();
				IsValidNamespaceConstraint__Implementation__Frozen.FillDataStore();
				Method__Implementation__Frozen.FillDataStore();
				MethodInvocation__Implementation__Frozen.FillDataStore();
				MethodInvocationConstraint__Implementation__Frozen.FillDataStore();
				Module__Implementation__Frozen.FillDataStore();
				NotNullableConstraint__Implementation__Frozen.FillDataStore();
				ObjectClass__Implementation__Frozen.FillDataStore();
				ObjectParameter__Implementation__Frozen.FillDataStore();
				ObjectReferenceProperty__Implementation__Frozen.FillDataStore();
				Property__Implementation__Frozen.FillDataStore();
				StringParameter__Implementation__Frozen.FillDataStore();
				StringProperty__Implementation__Frozen.FillDataStore();
				StringRangeConstraint__Implementation__Frozen.FillDataStore();
				Struct__Implementation__Frozen.FillDataStore();
				StructProperty__Implementation__Frozen.FillDataStore();
				TypeRef__Implementation__Frozen.FillDataStore();
				ValueTypeProperty__Implementation__Frozen.FillDataStore();
				ViewDescriptor__Implementation__Frozen.FillDataStore();
		}
	}
	
}