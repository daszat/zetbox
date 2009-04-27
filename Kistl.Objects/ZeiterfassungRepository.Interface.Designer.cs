using System.Linq;

using Kistl.API;

namespace Kistl.App.Zeiterfassung
{

	public class ZeiterfassungRepository
	{
		public ZeiterfassungRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
		
		/// <summary>List of all Zeitkonto</summary>
		/// en:TimeAccount; Ein Konto für die Leistungserfassung. Es können nicht mehr als MaxStunden auf ein Konto gebucht werden.
		public IQueryable<Zeitkonto> Zeitkonten
		{ 
			get
			{
				return Context.GetQuery<Zeitkonto>();
			}
		}
		
		/// <summary>List of all TaetigkeitsArt</summary>
		/// 
		public IQueryable<TaetigkeitsArt> TaetigkeitsArten
		{ 
			get
			{
				return Context.GetQuery<TaetigkeitsArt>();
			}
		}
		
		/// <summary>List of all Kostentraeger</summary>
		/// 
		public IQueryable<Kostentraeger> Kostentraeger
		{ 
			get
			{
				return Context.GetQuery<Kostentraeger>();
			}
		}
		
		/// <summary>List of all Kostenstelle</summary>
		/// 
		public IQueryable<Kostenstelle> Kostenstellen
		{ 
			get
			{
				return Context.GetQuery<Kostenstelle>();
			}
		}
		
	
	}
	
}