use Kistl

alter table dbo.ObjectClasses add 
	[Namespace] nvarchar(100) not null default('Kistl.App'),
	[TableName] nvarchar(50) not null default('NewTable')
go

update dbo.ObjectClasses set [Namespace] = 'Kistl.App.Base',		[TableName] = 'ObjectClasses' where [ClassName] = 'ObjectClass'
update dbo.ObjectClasses set [Namespace] = 'Kistl.App.Projekte',	[TableName] = 'Projekte' where [ClassName] = 'Projekt'
update dbo.ObjectClasses set [Namespace] = 'Kistl.App.Projekte',	[TableName] = 'Tasks' where [ClassName] = 'Task'
go

create table ObjectProperties (
	[ID] int identity(1,1) not null,
	[fk_ObjectClass] int not null,
	[PropertyName] nvarchar(50) not null,
	[DataType] nvarchar(150) not null,
	[AssociationClass] nvarchar(50) null
	CONSTRAINT [PK_ObjectProperties] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)
go

INSERT INTO [ObjectClasses] ([ClassName], [ServerObject], [ClientObject],
			[DataObject], [Namespace], [TableName])
     VALUES
           ('BaseProperty'
           ,'Kistl.App.Base.BasePropertyServer, Kistl.App.Projekte'
           ,'Kistl.App.Base.BasePropertyClient, Kistl.App.Projekte'
           ,'Kistl.App.Base.BaseProperty, Kistl.App.Projekte'
           ,'Kistl.App.Base'
           ,'ObjectProperties')
