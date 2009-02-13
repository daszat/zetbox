using System.Linq;

using Kistl.API;

namespace Kistl.App.Test
{

	public class TestModuleRepository
	{
		public TestModuleRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
		
		/// <summary>List of all TestObjClass</summary>
		/// 
		public IQueryable<TestObjClass> TestObjClasses
		{ 
			get
			{
				return Context.GetQuery<TestObjClass>();
			}
		}
		
		/// <summary>List of all TestCustomObject</summary>
		/// 
		public IQueryable<TestCustomObject> TestCustomObjects
		{ 
			get
			{
				return Context.GetQuery<TestCustomObject>();
			}
		}
		
		/// <summary>List of all Muhblah</summary>
		/// 
		public IQueryable<Muhblah> Muhblas
		{ 
			get
			{
				return Context.GetQuery<Muhblah>();
			}
		}
		
		/// <summary>List of all LastTest</summary>
		/// 
		public IQueryable<LastTest> LastTests
		{ 
			get
			{
				return Context.GetQuery<LastTest>();
			}
		}
		
		/// <summary>List of all AnotherTest</summary>
		/// 
		public IQueryable<AnotherTest> AnotherTests
		{ 
			get
			{
				return Context.GetQuery<AnotherTest>();
			}
		}
		
	
	}
	
}