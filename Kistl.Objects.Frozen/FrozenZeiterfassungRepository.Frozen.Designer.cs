using System;
using System.Collections.Generic;
using System.Linq;

using Kistl.API;

namespace Kistl.App.Zeiterfassung
{

	public class FrozenZeiterfassungRepository
	{
		public FrozenZeiterfassungRepository(IKistlContext ctx)
		{
			this.Context = ctx;
		}
		
		public IKistlContext Context { get; private set; }
		
		/// <summary>List of all Zeitkonto</summary>
		/// 
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
		
		/// <summary>List of all Taetigkeit</summary>
		/// 
		public IQueryable<Taetigkeit> Taetigkeiten
		{ 
			get
			{
				return Context.GetQuery<Taetigkeit>();
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