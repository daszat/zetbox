use Kistl
go
IF OBJECT_ID ( '[dbo].[fn_ColumnExists]', 'FN' ) IS NOT NULL 
    DROP FUNCTION [dbo].[fn_ColumnExists];
GO

CREATE function [dbo].[fn_ColumnExists] (@table nvarchar(500), @column nvarchar(500)) 
returns bit
begin
declare @result int
SELECT @result = count(*) FROM sys.objects o inner join sys.columns c on c.object_id=o.object_id
	WHERE o.object_id = OBJECT_ID(@table) 
		AND o.type in (N'U')
		AND c.Name = @column
return convert(bit, @result)
end

GO
IF OBJECT_ID ( '[dbo].[fn_TableExists]', 'FN' ) IS NOT NULL 
    DROP FUNCTION [dbo].[fn_TableExists];
GO
CREATE function [dbo].[fn_TableExists] (@table nvarchar(500)) returns bit
begin
declare @result int
SELECT @result = count(*) FROM sys.objects 
	WHERE object_id = OBJECT_ID(@table) AND type in (N'U')
return convert(bit, @result)
end

GO

IF OBJECT_ID ( '[dbo].[sp_DropBaseTables]', 'P' ) IS NOT NULL 
    DROP PROCEDURE [dbo].[sp_DropBaseTables];
GO
CREATE procedure [dbo].[sp_DropBaseTables] 
as

IF dbo.fn_TableExists(N'[dbo].[ObjectClasses]') = 1
BEGIN
	drop table dbo.ObjectProperties
END


IF dbo.fn_TableExists(N'[dbo].[ObjectProperties]') = 1
BEGIN
	drop table dbo.ObjectClasses
END
GO


IF OBJECT_ID ( '[dbo].[sp_CheckObjectClass]', 'P' ) IS NOT NULL 
    DROP PROCEDURE [dbo].[sp_CheckObjectClass];
GO
CREATE procedure [dbo].[sp_CheckObjectClass]
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
				,[ServerObject]
				,[ClientObject]
				,[DataObject]
				,[Namespace]
				,[TableName])
		 VALUES
			   ('ObjectClass'
				,@namespace + '.' + @classname + 'Server, Kistl.App.Projekte'
				,@namespace + '.' + @classname + 'Client, Kistl.App.Projekte'
				,@namespace + '.' + @classname + ', Kistl.App.Projekte'
				,@namespace
				,@tablename)
END
ELSE
BEGIN
	PRINT 'Updateing Kistl.App.Base.ObjectClass Class'
	UPDATE dbo.ObjectClasses SET 
		[ServerObject] = @namespace + '.' + @classname + 'Server, Kistl.App.Projekte'
		,[ClientObject] = @namespace + '.' + @classname + 'Client, Kistl.App.Projekte'
		,[DataObject] = @namespace + '.' + @classname + ', Kistl.App.Projekte'
		,[TableName] = @tablename
	WHERE [Namespace] = @namespace and [ClassName] = @classname
END
GO

IF OBJECT_ID ( '[dbo].[sp_CheckObjectProperty]', 'P' ) IS NOT NULL 
    DROP PROCEDURE [dbo].[sp_CheckObjectProperty];
GO
CREATE procedure [dbo].[sp_CheckObjectProperty]
	(@namespace nvarchar(100)
	,@classname nvarchar(50)
	,@propertyname nvarchar(50)
	,@datatype nvarchar(150)
	,@IsList bit
	,@IsAssociation bit
	)
as
declare @fk_ObjClass int

select @fk_ObjClass = [ID] from dbo.ObjectClasses where 
	[Namespace] = @namespace and [ClassName] = @classname

IF NOT EXISTS (select * from dbo.ObjectProperties where 
	[fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname)
BEGIN
	PRINT N'Inserting ' + @namespace + N'.' + @classname + N'.' + @propertyname + N' Property'
	INSERT INTO [dbo].[ObjectProperties]
			([fk_ObjectClass], [PropertyName], [DataType], [IsList], [IsAssociation])
     VALUES (@fk_ObjClass, @propertyname, @datatype, @IsList, @IsAssociation)
END
ELSE
BEGIN
	PRINT N'Updateting ' + @namespace + N'.' + @classname + N'.' + @propertyname + N' Property'
	UPDATE [dbo].[ObjectProperties]
			SET [DataType] = @datatype, [IsList] = @IsList, [IsAssociation] = @IsAssociation
     WHERE [fk_ObjectClass] = @fk_ObjClass and [PropertyName] = @propertyname
END

GO
IF OBJECT_ID ( '[dbo].[sp_CheckBaseTables]', 'P' ) IS NOT NULL 
    DROP PROCEDURE [dbo].[sp_CheckBaseTables];
GO
CREATE procedure [dbo].[sp_CheckBaseTables] as

/******************* Create Tables if they dont exist *******************/
IF dbo.fn_TableExists(N'[dbo].[ObjectClasses]') = 0
BEGIN
PRINT 'Creating table [ObjectClasses]'
CREATE TABLE [dbo].[ObjectClasses] (
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ServerObject] [nvarchar](500) NOT NULL,
	[ClientObject] [nvarchar](500) NOT NULL,
	[DataObject] [nvarchar](500) NOT NULL,
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

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'ServerObject') = 0
BEGIN
	PRINT 'Adding Column [ObjectClasses].[ServerObject]'
	alter table [dbo].[ObjectClasses] add
		[ServerObject] [nvarchar](500) NOT NULL
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'ClientObject') = 0
BEGIN
	PRINT 'Adding Column [ObjectClasses].[ClientObject]'
	alter table [dbo].[ObjectClasses] add
		[ClientObject] [nvarchar](500) NOT NULL
END

IF dbo.fn_ColumnExists(N'[dbo].[ObjectClasses]', N'DataObject') = 0
BEGIN
	PRINT 'Adding Column [ObjectClasses].[DataObject]'
	alter table [dbo].[ObjectClasses] add
		[DataObject] [nvarchar](500) NOT NULL
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
exec sp_CheckObjectClass N'Kistl.App.Base', N'ObjectProperty', N'ObjectProperties'

/******************* Check Content of ObjectProperty *******************/
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectClass', N'ClassName', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectClass', N'ServerObject', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectClass', N'ClientObject', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectClass', N'DataObject', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectClass', N'Namespace', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectClass', N'TableName', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectClass', N'Properties', N'Kistl.App.Base.ObjectProperty', 1, 1

exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectProperty', N'fk_ObjectClass', N'System.Int32', 0, 1
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectProperty', N'PropertyName', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectProperty', N'DataType', N'System.String', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectProperty', N'IsList', N'System.Boolean', 0, 0
exec sp_CheckObjectProperty N'Kistl.App.Base', N'ObjectProperty', N'IsAssociation', N'System.Boolean', 0, 0

GO

exec sp_CheckBaseTables