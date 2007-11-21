ALTER procedure [dbo].[sp_CheckObjectClass]
	(@namespace nvarchar(100)
	,@classname nvarchar(50)
	,@tablename nvarchar(50)
	)  
as
IF NOT EXISTS (select * from dbo.ObjectClasses where 
	[Namespace] = @namespace and [ClassName] = @classname)
BEGIN
	PRINT N'Inserting ' + @namespace + '.' + @classname + ' Class'
	INSERT INTO [dbo].[ObjectClasses]
			   ([ClassName]
				,[Namespace]
				,[TableName])
		 VALUES
			   ('ObjectClass'
				,@namespace
				,@tablename)
END
ELSE
BEGIN
	PRINT 'Updateing Kistl.App.Base.ObjectClass Class'
	UPDATE dbo.ObjectClasses SET 
		[TableName] = @tablename
	WHERE [Namespace] = @namespace and [ClassName] = @classname
END

go

Create procedure [dbo].[sp_DropBaseProperty]
	(@namespace nvarchar(100)
	,@classname nvarchar(50)
	,@propertyname nvarchar(50)
	)
as
declare @fk_ObjClass int

select @fk_ObjClass = [ID] from dbo.ObjectClasses where 
	[Namespace] = @namespace and [ClassName] = @classname

IF EXISTS (select * from dbo.ObjectProperties where 
	[fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname)
BEGIN
	PRINT N'Dropping ' + @namespace + N'.' + @classname + N'.' + @propertyname + N' Property'
	delete from [dbo].[ObjectProperties]
     WHERE [fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname
END

GO


ALTER procedure [dbo].[sp_CheckBaseTables] as

/******************* Create Tables if they dont exist *******************/
IF dbo.fn_TableExists(N'[dbo].[ObjectClasses]') = 0
BEGIN
PRINT 'Creating table [ObjectClasses]'
CREATE TABLE [dbo].[ObjectClasses] (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[Namespace] nvarchar(100) not null,
	[TableName] nvarchar(50) not null,
	CONSTRAINT [PK_ObjectClasses] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
) 
END

IF dbo.fn_TableExists(N'[dbo].[ObjectProperties]') = 0
BEGIN
PRINT 'Creating table [ObjectProperties]'
CREATE TABLE [dbo].[ObjectProperties] (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[fk_ObjectClass] [int] NOT NULL,
	[PropertyName] [nvarchar](50) NOT NULL,
	[DataType] [nvarchar](150) NOT NULL,
	[IsList] [bit] NOT NULL default(0),
	[IsAssociation] [bit] NOT NULL default(0),
	CONSTRAINT [PK_ObjectProperties] PRIMARY KEY CLUSTERED 
	(
		[ID] ASC
	)
)
END

/******************* Check each Column *******************/
/******************* Only missing Columns are supported yet *******************/
IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'ID') = 0
BEGIN
	PRINT 'Adding Column [ObjectClasses].[ID]'
	alter table [dbo].[ObjectClasses] add
		[ID] [int] IDENTITY(1,1) NOT NULL,
		CONSTRAINT [PK_ObjectClasses] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)	
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'ClassName') = 0
BEGIN
	PRINT 'Adding Column [ObjectClasses].[ClassName]'
	alter table [dbo].[ObjectClasses] add
		[ClassName] [nvarchar](50) NOT NULL
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'ServerObject') = 1
BEGIN
	PRINT 'Dropping Column [ObjectClasses].[ServerObject]'
	alter table [dbo].[ObjectClasses] drop column
		[ServerObject]
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'ClientObject') = 1
BEGIN
	PRINT 'Dropping Column [ObjectClasses].[ClientObject]'
	alter table [dbo].[ObjectClasses] drop column
		[ClientObject]
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'DataObject') = 1
BEGIN
	PRINT 'Dropping Column [ObjectClasses].[DataObject]'
	alter table [dbo].[ObjectClasses] drop column
		[DataObject]
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'Namespace') = 0
BEGIN
	PRINT 'Adding Column [ObjectClasses].[Namespace]'
	alter table [dbo].[ObjectClasses] add
		[Namespace] nvarchar(100) not null
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'TableName') = 0
BEGIN
	PRINT 'Adding Column [ObjectClasses].[TableName]'
	alter table [dbo].[ObjectClasses] add
		[TableName] nvarchar(50) not null
END

-------------

IF dbo.fn_ColumnExists(N'[dbo].[ObjectProperties]', N'ID') = 0
BEGIN
	PRINT 'Adding Column [ObjectProperties].[ID]'
	alter table [dbo].[ObjectProperties] add
		[ID] [int] IDENTITY(1,1) NOT NULL,
		CONSTRAINT [PK_ObjectProperties] PRIMARY KEY CLUSTERED 
		(
			[ID] ASC
		)
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectProperties]', N'fk_ObjectClass') = 0
BEGIN
	PRINT 'Adding Column [ObjectProperties].[fk_ObjectClass]'
	alter table [dbo].[ObjectProperties] add
		[fk_ObjectClass] [int] NOT NULL
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectProperties]', N'PropertyName') = 0
BEGIN
	PRINT 'Adding Column [ObjectProperties].[PropertyName]'
	alter table [dbo].[ObjectProperties] add
		[PropertyName] [nvarchar](50) NOT NULL
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectProperties]', N'DataType') = 0
BEGIN
	PRINT 'Adding Column [ObjectProperties].[DataType]'
	alter table [dbo].[ObjectProperties] add
		[DataType] [nvarchar](150) NOT NULL
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectProperties]', N'IsList') = 0
BEGIN
	PRINT 'Adding Column [ObjectProperties].[IsList]'
	alter table [dbo].[ObjectProperties] add
		[IsList] [bit] NOT NULL default(0)
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectProperties]', N'IsAssociation') = 0
BEGIN
	PRINT 'Adding Column [ObjectProperties].[IsAssociation]'
	alter table [dbo].[ObjectProperties] add
		[IsAssociation] [bit] NOT NULL default(0)
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectProperties]', N'AssociationClass') = 1
BEGIN
	PRINT 'Dropping Column [ObjectProperties].[AssociationClass]'
	alter table [dbo].[ObjectProperties] drop column
		AssociationClass
END


/******************* Check Content of ObjectClass *******************/
exec sp_CheckObjectClass N'Kistl.App.Base', N'ObjectClass', N'ObjectClasses'
exec sp_CheckObjectClass N'Kistl.App.Base', N'BaseProperty', N'ObjectProperties'

/******************* Check Content of BaseProperty *******************/
exec sp_CheckBaseProperty N'Kistl.App.Base', N'ObjectClass', N'ClassName', N'System.String', 0, 0
exec sp_CheckBaseProperty N'Kistl.App.Base', N'ObjectClass', N'Namespace', N'System.String', 0, 0
exec sp_CheckBaseProperty N'Kistl.App.Base', N'ObjectClass', N'TableName', N'System.String', 0, 0
exec sp_CheckBaseProperty N'Kistl.App.Base', N'ObjectClass', N'Properties', N'Kistl.App.Base.BaseProperty', 1, 1

exec sp_DropBaseProperty N'Kistl.App.Base', N'ObjectClass', N'ServerObject'
exec sp_DropBaseProperty N'Kistl.App.Base', N'ObjectClass', N'ClientObject'
exec sp_DropBaseProperty N'Kistl.App.Base', N'ObjectClass', N'DataObject'


exec sp_CheckBaseProperty N'Kistl.App.Base', N'BaseProperty', N'fk_ObjectClass', N'System.Int32', 0, 1
exec sp_CheckBaseProperty N'Kistl.App.Base', N'BaseProperty', N'PropertyName', N'System.String', 0, 0
exec sp_CheckBaseProperty N'Kistl.App.Base', N'BaseProperty', N'DataType', N'System.String', 0, 0
exec sp_CheckBaseProperty N'Kistl.App.Base', N'BaseProperty', N'IsList', N'System.Boolean', 0, 0
exec sp_CheckBaseProperty N'Kistl.App.Base', N'BaseProperty', N'IsAssociation', N'System.Boolean', 0, 0

GO

exec sp_CheckBaseTables