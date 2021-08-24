USE [master]
GO
/****** Object:  Database [SymphonyLimited]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE DATABASE [SymphonyLimited]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SymphonyLimited', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SymphonyLimited.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SymphonyLimited_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\SymphonyLimited_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SymphonyLimited] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SymphonyLimited].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SymphonyLimited] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SymphonyLimited] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SymphonyLimited] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SymphonyLimited] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SymphonyLimited] SET ARITHABORT OFF 
GO
ALTER DATABASE [SymphonyLimited] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SymphonyLimited] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SymphonyLimited] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SymphonyLimited] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SymphonyLimited] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SymphonyLimited] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SymphonyLimited] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SymphonyLimited] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SymphonyLimited] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SymphonyLimited] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SymphonyLimited] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SymphonyLimited] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SymphonyLimited] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SymphonyLimited] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SymphonyLimited] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SymphonyLimited] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [SymphonyLimited] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SymphonyLimited] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SymphonyLimited] SET  MULTI_USER 
GO
ALTER DATABASE [SymphonyLimited] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SymphonyLimited] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SymphonyLimited] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SymphonyLimited] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [SymphonyLimited] SET DELAYED_DURABILITY = DISABLED 
GO
USE [SymphonyLimited]
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Admissions]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admissions](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[StartTime] [datetime] NOT NULL,
	[EndTime] [datetime] NOT NULL,
	[BillTime] [datetime] NOT NULL,
	[PassedMark] [float] NOT NULL,
	[MaxMark] [float] NOT NULL,
	[Price] [float] NOT NULL,
	[CourseId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Admissions] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Branches]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Branches](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Time] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Branches] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Businesses]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Businesses](
	[EntityId] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Businesses] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Categories]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Path] [nvarchar](max) NULL,
	[Level] [int] NOT NULL,
	[ParentId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Categories] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Classes]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[QuantityStudent] [int] NOT NULL,
	[Status] [bit] NOT NULL,
	[AdmissionId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Classes] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CoreConfigDatas]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoreConfigDatas](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
	[Code] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.CoreConfigDatas] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Courses]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Courses](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Time] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Price] [real] NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
	[Certificate] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[CategoryId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Courses] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Faqs]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Faqs](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](max) NOT NULL,
	[Answer] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Faqs] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupPermissions]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupPermissions](
	[GroupId] [int] NOT NULL,
	[PermissionId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.GroupPermissions] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC,
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GroupUsers]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GroupUsers](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.GroupUsers] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PaidRegisters]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PaidRegisters](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[RollNumber] [nvarchar](max) NULL,
	[Result] [float] NOT NULL,
	[Tested] [bit] NOT NULL,
	[Passed] [bit] NOT NULL,
	[RegisterInfoId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.PaidRegisters] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Partners]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partners](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Partners] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Permissions]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Permissions](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Description] [nvarchar](max) NULL,
	[BusinessId] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Permissions] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RegisterInfoes]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegisterInfoes](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Comment] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[Status] [bit] NOT NULL,
	[AdmissionId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RegisterInfoes] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Students]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[RegisterInfoId] [int] NOT NULL,
	[ClassId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Students] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Teachers]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Teachers](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[Specialize] [nvarchar](max) NOT NULL,
	[Subject] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Teachers] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 8/24/2021 11:15:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[EntityId] [int] IDENTITY(1,1) NOT NULL,
	[Account] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NULL,
	[FullName] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Phone] [nvarchar](max) NOT NULL,
	[Image] [nvarchar](max) NULL,
	[GroupId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[EntityId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Index [IX_CourseId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_CourseId] ON [dbo].[Admissions]
(
	[CourseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdmissionId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdmissionId] ON [dbo].[Classes]
(
	[AdmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_CategoryId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_CategoryId] ON [dbo].[Courses]
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_GroupId] ON [dbo].[GroupPermissions]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_PermissionId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_PermissionId] ON [dbo].[GroupPermissions]
(
	[PermissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [IX_BusinessId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_BusinessId] ON [dbo].[Permissions]
(
	[BusinessId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_AdmissionId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_AdmissionId] ON [dbo].[RegisterInfoes]
(
	[AdmissionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [IX_GroupId]    Script Date: 8/24/2021 11:15:19 PM ******/
CREATE NONCLUSTERED INDEX [IX_GroupId] ON [dbo].[Users]
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Admissions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Admissions_dbo.Courses_CourseId] FOREIGN KEY([CourseId])
REFERENCES [dbo].[Courses] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Admissions] CHECK CONSTRAINT [FK_dbo.Admissions_dbo.Courses_CourseId]
GO
ALTER TABLE [dbo].[Classes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Classes_dbo.Admissions_AdmissionId] FOREIGN KEY([AdmissionId])
REFERENCES [dbo].[Admissions] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Classes] CHECK CONSTRAINT [FK_dbo.Classes_dbo.Admissions_AdmissionId]
GO
ALTER TABLE [dbo].[Courses]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Courses_dbo.Categories_CategoryId] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Courses] CHECK CONSTRAINT [FK_dbo.Courses_dbo.Categories_CategoryId]
GO
ALTER TABLE [dbo].[GroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.GroupPermissions_dbo.GroupUsers_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[GroupUsers] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupPermissions] CHECK CONSTRAINT [FK_dbo.GroupPermissions_dbo.GroupUsers_GroupId]
GO
ALTER TABLE [dbo].[GroupPermissions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.GroupPermissions_dbo.Permissions_PermissionId] FOREIGN KEY([PermissionId])
REFERENCES [dbo].[Permissions] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[GroupPermissions] CHECK CONSTRAINT [FK_dbo.GroupPermissions_dbo.Permissions_PermissionId]
GO
ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Permissions_dbo.Businesses_BusinessId] FOREIGN KEY([BusinessId])
REFERENCES [dbo].[Businesses] ([EntityId])
GO
ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_dbo.Permissions_dbo.Businesses_BusinessId]
GO
ALTER TABLE [dbo].[RegisterInfoes]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RegisterInfoes_dbo.Admissions_AdmissionId] FOREIGN KEY([AdmissionId])
REFERENCES [dbo].[Admissions] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RegisterInfoes] CHECK CONSTRAINT [FK_dbo.RegisterInfoes_dbo.Admissions_AdmissionId]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.GroupUsers_GroupId] FOREIGN KEY([GroupId])
REFERENCES [dbo].[GroupUsers] ([EntityId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.GroupUsers_GroupId]
GO
USE [master]
GO
ALTER DATABASE [SymphonyLimited] SET  READ_WRITE 
GO
