USE [master]
GO
/****** Object:  Database [ADMIN_1]    Script Date: 09.12.2021 6:42:17 PM ******/
CREATE DATABASE [ADMIN_1]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ADMIN_1', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRES\MSSQL\DATA\ADMIN_1.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ADMIN_1_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRES\MSSQL\DATA\ADMIN_1_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ADMIN_1] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ADMIN_1].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ADMIN_1] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ADMIN_1] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ADMIN_1] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ADMIN_1] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ADMIN_1] SET ARITHABORT OFF 
GO
ALTER DATABASE [ADMIN_1] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ADMIN_1] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ADMIN_1] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ADMIN_1] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ADMIN_1] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ADMIN_1] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ADMIN_1] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ADMIN_1] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ADMIN_1] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ADMIN_1] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ADMIN_1] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ADMIN_1] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ADMIN_1] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ADMIN_1] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ADMIN_1] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ADMIN_1] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ADMIN_1] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ADMIN_1] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ADMIN_1] SET  MULTI_USER 
GO
ALTER DATABASE [ADMIN_1] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ADMIN_1] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ADMIN_1] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ADMIN_1] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ADMIN_1] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ADMIN_1] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ADMIN_1] SET QUERY_STORE = OFF
GO
USE [ADMIN_1]
GO
/****** Object:  Table [dbo].[Auctions]    Script Date: 09.12.2021 6:42:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Auctions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Detail] [nvarchar](150) NULL,
	[Price] [int] NULL,
	[LastBidUser] [int] NULL,
	[IsClosed] [bit] NOT NULL,
 CONSTRAINT [PK__Auctions__3214EC078721141D] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 09.12.2021 6:42:18 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[UserPass] [nvarchar](50) NULL,
	[DisplayName] [nvarchar](50) NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Auctions] ON 

INSERT [dbo].[Auctions] ([Id], [Name], [Detail], [Price], [LastBidUser], [IsClosed]) VALUES (1, N'RayBan Sunglass', N'Gold-tone Aviator Classic', 152, 4, 1)
INSERT [dbo].[Auctions] ([Id], [Name], [Detail], [Price], [LastBidUser], [IsClosed]) VALUES (2, N'Phone 11', N'Aluminum frame, Gorilla Glass front with oleophobic coating', 453, 4, 1)
INSERT [dbo].[Auctions] ([Id], [Name], [Detail], [Price], [LastBidUser], [IsClosed]) VALUES (6, N'Dolce Gusto Piccolo', N'Coffee machine', 54, 2, 1)
INSERT [dbo].[Auctions] ([Id], [Name], [Detail], [Price], [LastBidUser], [IsClosed]) VALUES (7, N'Jack Daniels Whiskey', N'Tennesee Sour Mash Whiskey 50%', 31, 17, 1)
INSERT [dbo].[Auctions] ([Id], [Name], [Detail], [Price], [LastBidUser], [IsClosed]) VALUES (8, N'Skii Gear Fischer', N'Carving, 165 cm, Radius 17m', 150, NULL, 0)
SET IDENTITY_INSERT [dbo].[Auctions] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (1, N'darthvader', N'pera321', N'Anakin Skaywalker', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (2, N'c3po', N'newhope', N'C3PO', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (3, N'lukes', N'newhope', N'Luke Skaywalker', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (4, N'force', N'newhope', N'Joda Master', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (8, N'princesslea', N'newhope', N'Lea Skaywalker', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (10, N'lara33', N'larica22', N'Lara Croft', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (11, N'admin', N'admin', N'Administrator', 1)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (15, N'hsolo', N'newhope', N'Han Solo', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (16, N'iron', N'man', N'Iron Man', 0)
INSERT [dbo].[User] ([Id], [UserName], [UserPass], [DisplayName], [IsAdmin]) VALUES (17, N'user', N'user', N'User', 0)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
USE [master]
GO
ALTER DATABASE [ADMIN_1] SET  READ_WRITE 
GO
