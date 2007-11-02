USE [master]
GO
/****** Object:  Database [Kistl]    Script Date: 11/02/2007 11:10:00 ******/
CREATE DATABASE [Kistl] ON  PRIMARY 
GO

USE [Kistl]
GO
/****** Object:  Table [dbo].[Projekte]    Script Date: 11/02/2007 11:08:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Projekte]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Projekte](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
 CONSTRAINT [PK_Projekte] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[ObjectClasses]    Script Date: 11/02/2007 11:08:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ObjectClasses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[ObjectClasses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClassName] [nvarchar](50) NOT NULL,
	[ServerObject] [nvarchar](500) NOT NULL,
	[ClientObject] [nvarchar](500) NOT NULL,
	[DataObject] [nvarchar](500) NOT NULL,
 CONSTRAINT [PK_ObjectClasses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 11/02/2007 11:08:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tasks]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Tasks](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[DatumVon] [datetime] NULL,
	[DatumBis] [datetime] NULL,
	[Aufwand] [float] NULL,
	[fk_Projekt] [int] NOT NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  ForeignKey [FK_Tasks_Projekte]    Script Date: 11/02/2007 11:08:15 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Tasks_Projekte]') AND parent_object_id = OBJECT_ID(N'[dbo].[Tasks]'))
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Projekte] FOREIGN KEY([fk_Projekt])
REFERENCES [dbo].[Projekte] ([ID])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Projekte]
GO

/*********** Insert Data *****************/
INSERT INTO [Kistl].[dbo].[ObjectClasses]
           ([ClassName]
           ,[ServerObject]
           ,[ClientObject]
           ,[DataObject])
     VALUES
           ('ObjectClass'
           ,'Kistl.App.Base.ObjectClassServer, Kistl.App.Projekte'
           ,'Kistl.App.Base.ObjectClassClient, Kistl.App.Projekte'
           ,'Kistl.App.Base.ObjectClass, Kistl.App.Projekte')

INSERT INTO [Kistl].[dbo].[ObjectClasses]
           ([ClassName]
           ,[ServerObject]
           ,[ClientObject]
           ,[DataObject])
     VALUES
           ('Projekt'
           ,'Kistl.App.Projekte.ProjektServer, Kistl.App.Projekte'
           ,'Kistl.App.Projekte.ProjektClient, Kistl.App.Projekte'
           ,'Kistl.App.Projekte.Projekt, Kistl.App.Projekte')

INSERT INTO [Kistl].[dbo].[ObjectClasses]
           ([ClassName]
           ,[ServerObject]
           ,[ClientObject]
           ,[DataObject])
     VALUES
           ('Task'
           ,'Kistl.App.Projekte.TaskServer, Kistl.App.Projekte'
           ,'Kistl.App.Projekte.TaskClient, Kistl.App.Projekte'
           ,'Kistl.App.Projekte.Task, Kistl.App.Projekte')

GO