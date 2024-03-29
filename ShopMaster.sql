USE [master]
GO
/****** Object:  Database [ShopMaster]    Script Date: 6/28/2019 10:09:45 AM ******/
CREATE DATABASE [ShopMaster]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShopMaster', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ShopMaster.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShopMaster_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\ShopMaster_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [ShopMaster] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShopMaster].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShopMaster] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShopMaster] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShopMaster] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShopMaster] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShopMaster] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShopMaster] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShopMaster] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShopMaster] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShopMaster] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShopMaster] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShopMaster] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShopMaster] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShopMaster] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShopMaster] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShopMaster] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShopMaster] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShopMaster] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShopMaster] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShopMaster] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShopMaster] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShopMaster] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShopMaster] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShopMaster] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ShopMaster] SET  MULTI_USER 
GO
ALTER DATABASE [ShopMaster] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShopMaster] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShopMaster] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShopMaster] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShopMaster] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ShopMaster] SET QUERY_STORE = OFF
GO
USE [ShopMaster]
GO
/****** Object:  Table [dbo].[LoginReportData]    Script Date: 6/28/2019 10:09:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoginReportData](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[data] [varchar](350) NULL,
	[date] [date] NULL,
 CONSTRAINT [PK_LoginReportData] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[loginTable]    Script Date: 6/28/2019 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[loginTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullname] [varchar](50) NULL,
	[telephone] [varchar](50) NULL,
	[username] [varchar](50) NULL,
	[password] [varchar](50) NULL,
	[access_type] [varchar](50) NULL,
 CONSTRAINT [PK_loginTable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[productsmarket_Table]    Script Date: 6/28/2019 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[productsmarket_Table](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[productname] [varchar](50) NULL,
	[prod_description] [varchar](50) NULL,
	[price] [decimal](18, 2) NULL,
	[productQty] [int] NULL,
 CONSTRAINT [PK_productsmarket_Table] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[salesTransactionTable]    Script Date: 6/28/2019 10:09:49 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[salesTransactionTable](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[invoiceNumber] [varchar](50) NULL,
	[data] [varchar](350) NULL,
	[transactionDate] [date] NULL,
 CONSTRAINT [PK_salesTransactionTable] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [master]
GO
ALTER DATABASE [ShopMaster] SET  READ_WRITE 
GO
