using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;

namespace Kistl.App.Base
{

	public static class FrozenKistlBaseRepository
	{
		
		/// <summary>Frozen List of all ViewDescriptor</summary>
		/// 
		public static IQueryable<ViewDescriptor> ViewDescriptors
		{ 
			get
			{
				return ViewDescriptor__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<ViewDescriptor>();
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
		
		/// <summary>Frozen List of all Relation</summary>
		/// Describes a Relation between two Object Classes
		public static IQueryable<Relation> Relations
		{ 
			get
			{
				return Relation__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Relation>();
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
		
		/// <summary>Frozen List of all MethodInvocation</summary>
		/// Metadefinition Object for a MethodInvocation on a Method of a DataType.
		public static IQueryable<MethodInvocation> MethodInvocations
		{ 
			get
			{
				return MethodInvocation__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<MethodInvocation>();
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
		
		/// <summary>Frozen List of all EnumerationEntry</summary>
		/// Metadefinition Object for an Enumeration Entry.
		public static IQueryable<EnumerationEntry> EnumerationEntries
		{ 
			get
			{
				return EnumerationEntry__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<EnumerationEntry>();
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
		
		/// <summary>Frozen List of all Constraint</summary>
		/// 
		public static IQueryable<Constraint> Constraints
		{ 
			get
			{
				return Constraint__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Constraint>();
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
		
		/// <summary>Frozen List of all BaseParameter</summary>
		/// Metadefinition Object for Parameter. This class is abstract.
		public static IQueryable<BaseParameter> BaseParameters
		{ 
			get
			{
				return BaseParameter__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<BaseParameter>();
			}
		}
		
		/// <summary>Frozen List of all Assembly</summary>
		/// 
		public static IQueryable<Assembly> Assemblies
		{ 
			get
			{
				return Assembly__Implementation__Frozen.DataStore.Values.AsQueryable().Cast<Assembly>();
			}
		}
		

		internal static void FillDataStore()
		{
				ViewDescriptor__Implementation__Frozen.FillDataStore();
				TypeRef__Implementation__Frozen.FillDataStore();
				Relation__Implementation__Frozen.FillDataStore();
				Module__Implementation__Frozen.FillDataStore();
				MethodInvocation__Implementation__Frozen.FillDataStore();
				Method__Implementation__Frozen.FillDataStore();
				EnumerationEntry__Implementation__Frozen.FillDataStore();
				DataType__Implementation__Frozen.FillDataStore();
				Constraint__Implementation__Frozen.FillDataStore();
				BaseProperty__Implementation__Frozen.FillDataStore();
				BaseParameter__Implementation__Frozen.FillDataStore();
				Assembly__Implementation__Frozen.FillDataStore();
		}
	}
	
}