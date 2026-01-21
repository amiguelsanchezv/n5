USE [master]
GO

-- Drop database if exists to ensure clean setup
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'N5')
BEGIN
    ALTER DATABASE [N5] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [N5];
END
GO

/****** Object:  Database [N5]    Script Date: 6/11/2023 11:54:40 p. m. ******/
CREATE DATABASE [N5]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'N5', FILENAME = N'/var/opt/mssql/data/N5.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'N5_log', FILENAME = N'/var/opt/mssql/data/N5_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [N5].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [N5] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [N5] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [N5] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [N5] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [N5] SET ARITHABORT OFF 
GO

ALTER DATABASE [N5] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [N5] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [N5] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [N5] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [N5] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [N5] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [N5] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [N5] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [N5] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [N5] SET  DISABLE_BROKER 
GO

ALTER DATABASE [N5] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [N5] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [N5] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [N5] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [N5] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [N5] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [N5] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [N5] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [N5] SET  MULTI_USER 
GO

ALTER DATABASE [N5] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [N5] SET DB_CHAINING OFF 
GO

ALTER DATABASE [N5] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [N5] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [N5] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [N5] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [N5] SET QUERY_STORE = ON
GO

ALTER DATABASE [N5] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [N5] SET  READ_WRITE 
GO
USE [N5]
GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 6/11/2023 11:52:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] varchar(max) NOT NULL,
	[EmployeeLastName] varchar(max) NOT NULL,
	[PermissionType] [int] NOT NULL,
	[PermissionDate] [date] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PermissionTypes]    Script Date: 6/11/2023 11:52:58 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PermissionTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] varchar(max) NOT NULL,
 CONSTRAINT [PK_PermissionTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_PermissionTypes_Permissions] FOREIGN KEY([PermissionType])
REFERENCES [dbo].[PermissionTypes] ([Id])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_PermissionTypes_Permissions]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Forename' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'EmployeeName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Employee Surname' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'EmployeeLastName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission Type' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'PermissionType'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission granted on Date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Permissions', @level2type=N'COLUMN',@level2name=N'PermissionDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Unique ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionTypes', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Permission description' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PermissionTypes', @level2type=N'COLUMN',@level2name=N'Description'
GO

-- Clear existing data and reset identity seeds
USE [N5]
GO

-- Delete all existing data (if any)
IF EXISTS (SELECT * FROM sys.tables WHERE name = 'Permissions' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    DELETE FROM [dbo].[Permissions]
END
GO

IF EXISTS (SELECT * FROM sys.tables WHERE name = 'PermissionTypes' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    DELETE FROM [dbo].[PermissionTypes]
END
GO

-- Reset identity seeds
DBCC CHECKIDENT ('[dbo].[Permissions]', RESEED, 0)
GO

DBCC CHECKIDENT ('[dbo].[PermissionTypes]', RESEED, 0)
GO

-- Insert initial PermissionTypes data
INSERT INTO [dbo].[PermissionTypes] ([Description]) VALUES ('Administrator')
GO

INSERT INTO [dbo].[PermissionTypes] ([Description]) VALUES ('Consult')
GO

INSERT INTO [dbo].[PermissionTypes] ([Description]) VALUES ('Commercial')
GO