using System.Linq;

using Kistl.API;

namespace Kistl.App.Base
{

	public class KistlBaseRepository
	{
		public KistlBaseRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
		
		/// <summary>List of all ViewDescriptor</summary>
		/// 
		public IQueryable<ViewDescriptor> ViewDescriptors
		{ 
			get
			{
				return Context.GetQuery<ViewDescriptor>();
			}
		}
		
		/// <summary>List of all ValueTypeProperty</summary>
		/// Metadefinition Object for ValueType Properties. This class is abstract.
		public IQueryable<ValueTypeProperty> ValueTypeProperties
		{ 
			get
			{
				return Context.GetQuery<ValueTypeProperty>();
			}
		}
		
		/// <summary>List of all TypeRef</summary>
		/// This class models a reference to a specific, concrete Type. Generic Types have all parameters filled.
		public IQueryable<TypeRef> TypeRefs
		{ 
			get
			{
				return Context.GetQuery<TypeRef>();
			}
		}
		
		/// <summary>List of all StructProperty</summary>
		/// Metadefinition Object for Struct Properties.
		public IQueryable<StructProperty> StructProperties
		{ 
			get
			{
				return Context.GetQuery<StructProperty>();
			}
		}
		
		/// <summary>List of all Struct</summary>
		/// Metadefinition Object for Structs.
		public IQueryable<Struct> Structs
		{ 
			get
			{
				return Context.GetQuery<Struct>();
			}
		}
		
		/// <summary>List of all StringRangeConstraint</summary>
		/// 
		public IQueryable<StringRangeConstraint> StringRangeConstraints
		{ 
			get
			{
				return Context.GetQuery<StringRangeConstraint>();
			}
		}
		
		/// <summary>List of all StringProperty</summary>
		/// Metadefinition Object for String Properties.
		public IQueryable<StringProperty> StringProperties
		{ 
			get
			{
				return Context.GetQuery<StringProperty>();
			}
		}
		
		/// <summary>List of all StringParameter</summary>
		/// Metadefinition Object for String Parameter.
		public IQueryable<StringParameter> StringParameters
		{ 
			get
			{
				return Context.GetQuery<StringParameter>();
			}
		}
		
		/// <summary>List of all RelationEnd</summary>
		/// Describes one end of a relation between two object classes
		public IQueryable<RelationEnd> RelationEnds
		{ 
			get
			{
				return Context.GetQuery<RelationEnd>();
			}
		}
		
		/// <summary>List of all Relation</summary>
		/// Describes a Relation between two Object Classes
		public IQueryable<Relation> Relations
		{ 
			get
			{
				return Context.GetQuery<Relation>();
			}
		}
		
		/// <summary>List of all Property</summary>
		/// Metadefinition Object for Properties. This class is abstract.
		public IQueryable<Property> Properties
		{ 
			get
			{
				return Context.GetQuery<Property>();
			}
		}
		
		/// <summary>List of all ObjectReferenceProperty</summary>
		/// Metadefinition Object for ObjectReference Properties.
		public IQueryable<ObjectReferenceProperty> ObjectReferenceProperties
		{ 
			get
			{
				return Context.GetQuery<ObjectReferenceProperty>();
			}
		}
		
		/// <summary>List of all ObjectParameter</summary>
		/// Metadefinition Object for Object Parameter.
		public IQueryable<ObjectParameter> ObjectParameters
		{ 
			get
			{
				return Context.GetQuery<ObjectParameter>();
			}
		}
		
		/// <summary>List of all ObjectClass</summary>
		/// Metadefinition Object for ObjectClasses.
		public IQueryable<ObjectClass> ObjectClasses
		{ 
			get
			{
				return Context.GetQuery<ObjectClass>();
			}
		}
		
		/// <summary>List of all NotNullableConstraint</summary>
		/// 
		public IQueryable<NotNullableConstraint> NotNullableConstraints
		{ 
			get
			{
				return Context.GetQuery<NotNullableConstraint>();
			}
		}
		
		/// <summary>List of all Module</summary>
		/// Metadefinition Object for Modules.
		public IQueryable<Module> Modules
		{ 
			get
			{
				return Context.GetQuery<Module>();
			}
		}
		
		/// <summary>List of all MethodInvocationConstraint</summary>
		/// 
		public IQueryable<MethodInvocationConstraint> MethodInvocationConstraints
		{ 
			get
			{
				return Context.GetQuery<MethodInvocationConstraint>();
			}
		}
		
		/// <summary>List of all MethodInvocation</summary>
		/// Metadefinition Object for a MethodInvocation on a Method of a DataType.
		public IQueryable<MethodInvocation> MethodInvocations
		{ 
			get
			{
				return Context.GetQuery<MethodInvocation>();
			}
		}
		
		/// <summary>List of all Method</summary>
		/// Metadefinition Object for Methods.
		public IQueryable<Method> Methods
		{ 
			get
			{
				return Context.GetQuery<Method>();
			}
		}
		
		/// <summary>List of all IsValidNamespaceConstraint</summary>
		/// 
		public IQueryable<IsValidNamespaceConstraint> IsValidNamespaceConstraints
		{ 
			get
			{
				return Context.GetQuery<IsValidNamespaceConstraint>();
			}
		}
		
		/// <summary>List of all IsValidIdentifierConstraint</summary>
		/// 
		public IQueryable<IsValidIdentifierConstraint> IsValidIdentifierConstraints
		{ 
			get
			{
				return Context.GetQuery<IsValidIdentifierConstraint>();
			}
		}
		
		/// <summary>List of all IntProperty</summary>
		/// Metadefinition Object for Int Properties.
		public IQueryable<IntProperty> IntProperties
		{ 
			get
			{
				return Context.GetQuery<IntProperty>();
			}
		}
		
		/// <summary>List of all IntParameter</summary>
		/// Metadefinition Object for Int Parameter.
		public IQueryable<IntParameter> IntParameters
		{ 
			get
			{
				return Context.GetQuery<IntParameter>();
			}
		}
		
		/// <summary>List of all Interface</summary>
		/// Metadefinition Object for Interfaces.
		public IQueryable<Interface> Interfaces
		{ 
			get
			{
				return Context.GetQuery<Interface>();
			}
		}
		
		/// <summary>List of all IntegerRangeConstraint</summary>
		/// 
		public IQueryable<IntegerRangeConstraint> IntegerRangeConstraints
		{ 
			get
			{
				return Context.GetQuery<IntegerRangeConstraint>();
			}
		}
		
		/// <summary>List of all EnumerationProperty</summary>
		/// Metadefinition Object for Enumeration Properties.
		public IQueryable<EnumerationProperty> EnumerationProperties
		{ 
			get
			{
				return Context.GetQuery<EnumerationProperty>();
			}
		}
		
		/// <summary>List of all EnumerationEntry</summary>
		/// Metadefinition Object for an Enumeration Entry.
		public IQueryable<EnumerationEntry> EnumerationEntries
		{ 
			get
			{
				return Context.GetQuery<EnumerationEntry>();
			}
		}
		
		/// <summary>List of all Enumeration</summary>
		/// Metadefinition Object for Enumerations.
		public IQueryable<Enumeration> Enumerations
		{ 
			get
			{
				return Context.GetQuery<Enumeration>();
			}
		}
		
		/// <summary>List of all DoubleProperty</summary>
		/// Metadefinition Object for Double Properties.
		public IQueryable<DoubleProperty> DoubleProperties
		{ 
			get
			{
				return Context.GetQuery<DoubleProperty>();
			}
		}
		
		/// <summary>List of all DoubleParameter</summary>
		/// Metadefinition Object for Double Parameter.
		public IQueryable<DoubleParameter> DoubleParameters
		{ 
			get
			{
				return Context.GetQuery<DoubleParameter>();
			}
		}
		
		/// <summary>List of all DateTimeProperty</summary>
		/// Metadefinition Object for DateTime Properties.
		public IQueryable<DateTimeProperty> DateTimeProperties
		{ 
			get
			{
				return Context.GetQuery<DateTimeProperty>();
			}
		}
		
		/// <summary>List of all DateTimeParameter</summary>
		/// Metadefinition Object for DateTime Parameter.
		public IQueryable<DateTimeParameter> DateTimeParameters
		{ 
			get
			{
				return Context.GetQuery<DateTimeParameter>();
			}
		}
		
		/// <summary>List of all DataType</summary>
		/// Base Metadefinition Object for Objectclasses, Interfaces, Structs and Enumerations.
		public IQueryable<DataType> DataTypes
		{ 
			get
			{
				return Context.GetQuery<DataType>();
			}
		}
		
		/// <summary>List of all Constraint</summary>
		/// 
		public IQueryable<Constraint> Constraints
		{ 
			get
			{
				return Context.GetQuery<Constraint>();
			}
		}
		
		/// <summary>List of all CLRObjectParameter</summary>
		/// Metadefinition Object for CLR Object Parameter.
		public IQueryable<CLRObjectParameter> CLRObjectParameters
		{ 
			get
			{
				return Context.GetQuery<CLRObjectParameter>();
			}
		}
		
		/// <summary>List of all BoolProperty</summary>
		/// Metadefinition Object for Bool Properties.
		public IQueryable<BoolProperty> BoolProperties
		{ 
			get
			{
				return Context.GetQuery<BoolProperty>();
			}
		}
		
		/// <summary>List of all BoolParameter</summary>
		/// Metadefinition Object for Bool Parameter.
		public IQueryable<BoolParameter> BoolParameters
		{ 
			get
			{
				return Context.GetQuery<BoolParameter>();
			}
		}
		
		/// <summary>List of all BaseProperty</summary>
		/// Metadefinition Object for Properties. This class is abstract.
		public IQueryable<BaseProperty> BaseProperties
		{ 
			get
			{
				return Context.GetQuery<BaseProperty>();
			}
		}
		
		/// <summary>List of all BaseParameter</summary>
		/// Metadefinition Object for Parameter. This class is abstract.
		public IQueryable<BaseParameter> BaseParameters
		{ 
			get
			{
				return Context.GetQuery<BaseParameter>();
			}
		}
		
		/// <summary>List of all Assembly</summary>
		/// 
		public IQueryable<Assembly> Assemblies
		{ 
			get
			{
				return Context.GetQuery<Assembly>();
			}
		}
		
	
	}
	
}