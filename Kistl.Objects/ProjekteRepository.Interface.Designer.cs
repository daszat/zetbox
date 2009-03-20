using System.Linq;

using Kistl.API;

namespace Kistl.App.Projekte
{

	public class ProjekteRepository
	{
		public ProjekteRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
		
		/// <summary>List of all Kunde</summary>
		/// 
		public IQueryable<Kunde> Kunden
		{ 
			get
			{
				return Context.GetQuery<Kunde>();
			}
		}
		
		/// <summary>List of all Auftrag</summary>
		/// 
		public IQueryable<Auftrag> Auftraege
		{ 
			get
			{
				return Context.GetQuery<Auftrag>();
			}
		}
		
		/// <summary>List of all Mitarbeiter</summary>
		/// 
		public IQueryable<Mitarbeiter> Mitarbeiter
		{ 
			get
			{
				return Context.GetQuery<Mitarbeiter>();
			}
		}
		
		/// <summary>List of all Task</summary>
		/// 
		public IQueryable<Task> Tasks
		{ 
			get
			{
				return Context.GetQuery<Task>();
			}
		}
		
		/// <summary>List of all Projekt</summary>
		/// 
		public IQueryable<Projekt> Projekte
		{ 
			get
			{
				return Context.GetQuery<Projekt>();
			}
		}
		
	
	}
	
}