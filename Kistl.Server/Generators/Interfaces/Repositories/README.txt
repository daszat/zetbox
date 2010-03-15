The Repository contains accessors to all classes of objects.

public interface BaseRepository 
{

	IQueryable<Assembly> Assemblies { get; }
	IQueryable<DataType> DataTypes { get; }
	IQueryable<Constraint> Constraints { get; }
	
}


With LINQ, this can always be used as a starting point for queries.

    repo.Assemblies.Where(a => a.IsClientAssembly);


To avoid name clashes, there is one Repository interface per Module, called `I{Name}Repository` and one `IModuleRepository` listing all modules:

public interface ModuleRepository 
{
	BaseRepository Base { get; }
	GuiRepository Gui { get; }
	ProjekteRepository Projekte { get; }
}


See http://blogs.hibernatingrhinos.com/nhibernate/archive/2008/10/08/the-repository-pattern.aspx for a extended version of this pattern.

See http://in.relation.to/Bloggers/RepositoryPatternVsTransparentPersistence for a more controversial discussion of this pattern.