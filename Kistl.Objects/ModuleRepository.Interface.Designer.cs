using Kistl.API;

namespace Kistl.App
{

	public class ModuleRepository
	{
		public ModuleRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
	
		/// <summary>Repository for KistlBase</summary>
		/// 
		public Kistl.App.Base.KistlBaseRepository KistlBase
		{
			get
			{
				return new Kistl.App.Base.KistlBaseRepository(Context);
			}
		}
		
		/// <summary>Repository for Projekte</summary>
		/// 
		public Kistl.App.Projekte.ProjekteRepository Projekte
		{
			get
			{
				return new Kistl.App.Projekte.ProjekteRepository(Context);
			}
		}
		
		/// <summary>Repository for Zeiterfassung</summary>
		/// 
		public Kistl.App.Zeiterfassung.ZeiterfassungRepository Zeiterfassung
		{
			get
			{
				return new Kistl.App.Zeiterfassung.ZeiterfassungRepository(Context);
			}
		}
		
		/// <summary>Repository for GUI</summary>
		/// 
		public Kistl.App.GUI.GUIRepository GUI
		{
			get
			{
				return new Kistl.App.GUI.GUIRepository(Context);
			}
		}
		
		/// <summary>Repository for TestModule</summary>
		/// 
		public Kistl.App.Test.TestModuleRepository TestModule
		{
			get
			{
				return new Kistl.App.Test.TestModuleRepository(Context);
			}
		}
		
	
	}
	
}