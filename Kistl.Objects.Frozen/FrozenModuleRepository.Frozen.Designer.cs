using Kistl.API;

namespace Kistl.App.Frozen
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
		
		/// <summary>Repository for GUI</summary>
		/// 
		public Kistl.App.GUI.GUIRepository GUI
		{
			get
			{
				return new Kistl.App.GUI.GUIRepository(Context);
			}
		}
		
	
	}
	
}