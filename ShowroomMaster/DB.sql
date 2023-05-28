USE [master]
GO
/****** Object:  Database [ShowroomMaster]    Script Date: 28.05.2023 23:59:34 ******/
CREATE DATABASE [ShowroomMaster]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ShowroomMaster', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ShowroomMaster.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ShowroomMaster_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ShowroomMaster_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ShowroomMaster] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ShowroomMaster].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ShowroomMaster] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ShowroomMaster] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ShowroomMaster] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ShowroomMaster] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ShowroomMaster] SET ARITHABORT OFF 
GO
ALTER DATABASE [ShowroomMaster] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ShowroomMaster] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ShowroomMaster] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ShowroomMaster] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ShowroomMaster] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ShowroomMaster] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ShowroomMaster] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ShowroomMaster] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ShowroomMaster] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ShowroomMaster] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ShowroomMaster] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ShowroomMaster] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ShowroomMaster] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ShowroomMaster] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ShowroomMaster] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ShowroomMaster] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ShowroomMaster] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ShowroomMaster] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ShowroomMaster] SET  MULTI_USER 
GO
ALTER DATABASE [ShowroomMaster] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ShowroomMaster] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ShowroomMaster] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ShowroomMaster] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ShowroomMaster] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ShowroomMaster] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'ShowroomMaster', N'ON'
GO
ALTER DATABASE [ShowroomMaster] SET QUERY_STORE = ON
GO
ALTER DATABASE [ShowroomMaster] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ShowroomMaster]
GO
/****** Object:  Table [dbo].[ACCOUNT]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ACCOUNT](
	[MANF_ORDER] [varchar](15) NULL,
	[CUST_ORDER] [varchar](15) NULL,
	[AMOUNT] [int] NULL,
	[IS_PAID] [varchar](5) NOT NULL,
	[PAYMENT_DATE] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CAR]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CAR](
	[CAR_ID] [varchar](15) NOT NULL,
	[CAR_NAME] [varchar](20) NOT NULL,
	[CAR_MODEL] [char](4) NOT NULL,
	[CAR_COMPANY] [varchar](15) NOT NULL,
	[CAR_STATUS] [varchar](10) NOT NULL,
	[CAR_PRICE] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CAR_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CUSTOMER]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUSTOMER](
	[CUSTOMER_TC] [char](11) NOT NULL,
	[CUSTOMER_NAME] [varchar](25) NOT NULL,
	[CUSTOMER_CONTACT] [char](11) NOT NULL,
	[CUSTOMER_ADDRESS] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CUSTOMER_TC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CUSTOMER_ORDER]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CUSTOMER_ORDER](
	[ORDER_ID] [varchar](15) NOT NULL,
	[PERSONEL_ID] [varchar](15) NULL,
	[CAR_ID] [varchar](15) NULL,
	[CUSTOMER_TC] [char](11) NULL,
	[ORDER_DATE] [date] NOT NULL,
	[BILL] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ORDER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MANUF_ORDER]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANUF_ORDER](
	[ORDER_ID] [varchar](15) NOT NULL,
	[PERSONEL_ID] [varchar](15) NULL,
	[CAR_ID] [varchar](15) NULL,
	[MANUFACTURER_ID] [varchar](15) NULL,
	[ORDER_DATE] [date] NOT NULL,
	[BILL] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ORDER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MANUFACTURER]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MANUFACTURER](
	[MANUFACTURER_ID] [varchar](15) NOT NULL,
	[MANUFACTURER_NAME] [varchar](25) NOT NULL,
	[MANUFACTURER_EMAIL] [varchar](25) NOT NULL,
	[MANUFACTURER_ADDRESS] [varchar](50) NOT NULL,
	[MANUFACTURER_CONTACT] [char](11) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[MANUFACTURER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PERSONEL]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PERSONEL](
	[PERSONEL_ID] [varchar](15) NOT NULL,
	[PERSONEL_NAME] [varchar](25) NOT NULL,
	[PERSONEL_PASSWORD] [char](8) NOT NULL,
	[PERSONEL_CONTACT] [char](11) NOT NULL,
	[PERSONEL_ADDRESS] [varchar](50) NOT NULL,
	[PERSONEL_EMAIL] [varchar](25) NOT NULL,
	[PERSONEL_DESIGNATION] [varchar](15) NOT NULL,
	[PERSONEL_HIREDATE] [date] NOT NULL,
	[PERSONEL_FIREDATE] [date] NULL,
	[PERSONEL_STATUS] [varchar](10) NOT NULL,
	[PERSONEL_SALES] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[PERSONEL_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SELL_PAYMENT]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SELL_PAYMENT](
	[ORDER_ID] [varchar](15) NULL,
	[PAYMENT_DATE] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCK]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCK](
	[ORDER_ID] [varchar](15) NULL,
	[CAR_ID] [varchar](15) NULL,
	[REC_DATE] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[STOCK_PAYMENT]    Script Date: 28.05.2023 23:59:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[STOCK_PAYMENT](
	[ORDER_ID] [varchar](15) NULL,
	[PAYMENT_DATE] [date] NOT NULL
) ON [PRIMARY]
GO
INSERT [dbo].[ACCOUNT] ([MANF_ORDER], [CUST_ORDER], [AMOUNT], [IS_PAID], [PAYMENT_DATE]) VALUES (N'URS123', NULL, 3500000, N'TRUE', CAST(N'2023-04-30' AS Date))
GO
INSERT [dbo].[ACCOUNT] ([MANF_ORDER], [CUST_ORDER], [AMOUNT], [IS_PAID], [PAYMENT_DATE]) VALUES (NULL, N'MTS0124', 3745000, N'FALSE', CAST(N'2023-04-30' AS Date))
GO
INSERT [dbo].[CAR] ([CAR_ID], [CAR_NAME], [CAR_MODEL], [CAR_COMPANY], [CAR_STATUS], [CAR_PRICE]) VALUES (N'A1245', N'Z4', N'2019', N'BMW', N'Mevcut', 4500000)
GO
INSERT [dbo].[CAR] ([CAR_ID], [CAR_NAME], [CAR_MODEL], [CAR_COMPANY], [CAR_STATUS], [CAR_PRICE]) VALUES (N'A1356', N'Model X', N'2020', N'Tesla', N'Mevcut', 2500000)
GO
INSERT [dbo].[CAR] ([CAR_ID], [CAR_NAME], [CAR_MODEL], [CAR_COMPANY], [CAR_STATUS], [CAR_PRICE]) VALUES (N'A3241', N'GLB', N'2020', N'Mercedes', N'Satıldı', 3500000)
GO
INSERT [dbo].[CAR] ([CAR_ID], [CAR_NAME], [CAR_MODEL], [CAR_COMPANY], [CAR_STATUS], [CAR_PRICE]) VALUES (N'A45464', N'S40', N'2023', N'Volvo', N'Mevcut', 1200000)
GO
INSERT [dbo].[CUSTOMER] ([CUSTOMER_TC], [CUSTOMER_NAME], [CUSTOMER_CONTACT], [CUSTOMER_ADDRESS]) VALUES (N'11111111111', N'Beyza Koç', N'05426542351', N'Göztepe/İzmir')
GO
INSERT [dbo].[CUSTOMER] ([CUSTOMER_TC], [CUSTOMER_NAME], [CUSTOMER_CONTACT], [CUSTOMER_ADDRESS]) VALUES (N'12736482920', N'Seyit Akpancar', N'05537457050', N'Atabey/Isparta')
GO
INSERT [dbo].[CUSTOMER] ([CUSTOMER_TC], [CUSTOMER_NAME], [CUSTOMER_CONTACT], [CUSTOMER_ADDRESS]) VALUES (N'32366675412', N'Hasan', N'05555555555', N'Atabey')
GO
INSERT [dbo].[CUSTOMER_ORDER] ([ORDER_ID], [PERSONEL_ID], [CAR_ID], [CUSTOMER_TC], [ORDER_DATE], [BILL]) VALUES (N'MTS0124', N'YT666', N'A3241', N'11111111111', CAST(N'2023-04-30' AS Date), 3745000)
GO
INSERT [dbo].[CUSTOMER_ORDER] ([ORDER_ID], [PERSONEL_ID], [CAR_ID], [CUSTOMER_TC], [ORDER_DATE], [BILL]) VALUES (N'MTS123', N'YT666', N'A1245', N'12736482920', CAST(N'2023-04-30' AS Date), 3355000)
GO
INSERT [dbo].[MANUF_ORDER] ([ORDER_ID], [PERSONEL_ID], [CAR_ID], [MANUFACTURER_ID], [ORDER_DATE], [BILL]) VALUES (N'URS123', N'YT666', N'A3241', N'UR123', CAST(N'2023-04-30' AS Date), 3555000)
GO
INSERT [dbo].[MANUFACTURER] ([MANUFACTURER_ID], [MANUFACTURER_NAME], [MANUFACTURER_EMAIL], [MANUFACTURER_ADDRESS], [MANUFACTURER_CONTACT]) VALUES (N'UR123', N'MERCEDES', N'benz@mercedes.com', N'Berlin', N'23456789087')
GO
INSERT [dbo].[MANUFACTURER] ([MANUFACTURER_ID], [MANUFACTURER_NAME], [MANUFACTURER_EMAIL], [MANUFACTURER_ADDRESS], [MANUFACTURER_CONTACT]) VALUES (N'UR56446', N'Volvo', N'info@volvo.com', N'Roma', N'32156478951')
GO
INSERT [dbo].[PERSONEL] ([PERSONEL_ID], [PERSONEL_NAME], [PERSONEL_PASSWORD], [PERSONEL_CONTACT], [PERSONEL_ADDRESS], [PERSONEL_EMAIL], [PERSONEL_DESIGNATION], [PERSONEL_HIREDATE], [PERSONEL_FIREDATE], [PERSONEL_STATUS], [PERSONEL_SALES]) VALUES (N'ST123', N'Hasan Can', N'123456  ', N'05325216262', N'Merkez/Isparta', N'hasancan@gmail.com', N'SATICI', CAST(N'2023-04-30' AS Date), NULL, N'Calisiyor', 0)
GO
INSERT [dbo].[PERSONEL] ([PERSONEL_ID], [PERSONEL_NAME], [PERSONEL_PASSWORD], [PERSONEL_CONTACT], [PERSONEL_ADDRESS], [PERSONEL_EMAIL], [PERSONEL_DESIGNATION], [PERSONEL_HIREDATE], [PERSONEL_FIREDATE], [PERSONEL_STATUS], [PERSONEL_SALES]) VALUES (N'ST124', N'Salih YALÇIN', N'456123  ', N'05488547255', N'Çankaya/ANKARA', N'salih@yahoo.com', N'SATICI', CAST(N'2023-04-30' AS Date), NULL, N'Calisiyor', 0)
GO
INSERT [dbo].[PERSONEL] ([PERSONEL_ID], [PERSONEL_NAME], [PERSONEL_PASSWORD], [PERSONEL_CONTACT], [PERSONEL_ADDRESS], [PERSONEL_EMAIL], [PERSONEL_DESIGNATION], [PERSONEL_HIREDATE], [PERSONEL_FIREDATE], [PERSONEL_STATUS], [PERSONEL_SALES]) VALUES (N'ST125', N'Merve Yılmaz', N'456788  ', N'05426527898', N'Mecidiyeköy/İstanbul', N'merveylmz@outlook.com.tr', N'SATICI', CAST(N'2023-04-30' AS Date), NULL, N'Calisiyor', 0)
GO
INSERT [dbo].[PERSONEL] ([PERSONEL_ID], [PERSONEL_NAME], [PERSONEL_PASSWORD], [PERSONEL_CONTACT], [PERSONEL_ADDRESS], [PERSONEL_EMAIL], [PERSONEL_DESIGNATION], [PERSONEL_HIREDATE], [PERSONEL_FIREDATE], [PERSONEL_STATUS], [PERSONEL_SALES]) VALUES (N'ST126', N'Oğuzhan', N'12345678', N'05316542834', N'New York', N'oguzhan@showroom.com', N'SATICI', CAST(N'2023-05-28' AS Date), NULL, N'Calisiyor', 0)
GO
INSERT [dbo].[PERSONEL] ([PERSONEL_ID], [PERSONEL_NAME], [PERSONEL_PASSWORD], [PERSONEL_CONTACT], [PERSONEL_ADDRESS], [PERSONEL_EMAIL], [PERSONEL_DESIGNATION], [PERSONEL_HIREDATE], [PERSONEL_FIREDATE], [PERSONEL_STATUS], [PERSONEL_SALES]) VALUES (N'YT555', N'Seyit Akpancar', N'123456  ', N'05537454550', N'Atabey/Isparta', N'sakpancar@isparta.edu.tr', N'YONETICI', CAST(N'2023-04-30' AS Date), NULL, N'Calisiyor', 1)
GO
INSERT [dbo].[PERSONEL] ([PERSONEL_ID], [PERSONEL_NAME], [PERSONEL_PASSWORD], [PERSONEL_CONTACT], [PERSONEL_ADDRESS], [PERSONEL_EMAIL], [PERSONEL_DESIGNATION], [PERSONEL_HIREDATE], [PERSONEL_FIREDATE], [PERSONEL_STATUS], [PERSONEL_SALES]) VALUES (N'YT666', N'Ahmet Çimen', N'654321  ', N'05533479032', N'Lara/Antalya', N'ahmad@gmail.com', N'YONETICI', CAST(N'2023-04-30' AS Date), NULL, N'Calisiyor', 1)
GO
INSERT [dbo].[SELL_PAYMENT] ([ORDER_ID], [PAYMENT_DATE]) VALUES (N'MTS0124', CAST(N'2023-04-30' AS Date))
GO
INSERT [dbo].[STOCK] ([ORDER_ID], [CAR_ID], [REC_DATE]) VALUES (N'URS123', NULL, CAST(N'2023-04-30' AS Date))
GO
INSERT [dbo].[STOCK_PAYMENT] ([ORDER_ID], [PAYMENT_DATE]) VALUES (N'URS123', CAST(N'2023-04-30' AS Date))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__MANUFACT__971192EED63448D0]    Script Date: 28.05.2023 23:59:34 ******/
ALTER TABLE [dbo].[MANUFACTURER] ADD UNIQUE NONCLUSTERED 
(
	[MANUFACTURER_EMAIL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__PERSONEL__8B9688FAE53730C3]    Script Date: 28.05.2023 23:59:34 ******/
ALTER TABLE [dbo].[PERSONEL] ADD UNIQUE NONCLUSTERED 
(
	[PERSONEL_EMAIL] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ACCOUNT]  WITH CHECK ADD FOREIGN KEY([CUST_ORDER])
REFERENCES [dbo].[CUSTOMER_ORDER] ([ORDER_ID])
GO
ALTER TABLE [dbo].[ACCOUNT]  WITH CHECK ADD FOREIGN KEY([MANF_ORDER])
REFERENCES [dbo].[MANUF_ORDER] ([ORDER_ID])
GO
ALTER TABLE [dbo].[CUSTOMER_ORDER]  WITH CHECK ADD FOREIGN KEY([CAR_ID])
REFERENCES [dbo].[CAR] ([CAR_ID])
GO
ALTER TABLE [dbo].[CUSTOMER_ORDER]  WITH CHECK ADD FOREIGN KEY([CUSTOMER_TC])
REFERENCES [dbo].[CUSTOMER] ([CUSTOMER_TC])
GO
ALTER TABLE [dbo].[CUSTOMER_ORDER]  WITH CHECK ADD FOREIGN KEY([PERSONEL_ID])
REFERENCES [dbo].[PERSONEL] ([PERSONEL_ID])
GO
ALTER TABLE [dbo].[MANUF_ORDER]  WITH CHECK ADD FOREIGN KEY([CAR_ID])
REFERENCES [dbo].[CAR] ([CAR_ID])
GO
ALTER TABLE [dbo].[MANUF_ORDER]  WITH CHECK ADD FOREIGN KEY([MANUFACTURER_ID])
REFERENCES [dbo].[MANUFACTURER] ([MANUFACTURER_ID])
GO
ALTER TABLE [dbo].[MANUF_ORDER]  WITH CHECK ADD FOREIGN KEY([PERSONEL_ID])
REFERENCES [dbo].[PERSONEL] ([PERSONEL_ID])
GO
ALTER TABLE [dbo].[SELL_PAYMENT]  WITH CHECK ADD FOREIGN KEY([ORDER_ID])
REFERENCES [dbo].[CUSTOMER_ORDER] ([ORDER_ID])
GO
ALTER TABLE [dbo].[STOCK]  WITH CHECK ADD FOREIGN KEY([CAR_ID])
REFERENCES [dbo].[CAR] ([CAR_ID])
GO
ALTER TABLE [dbo].[STOCK]  WITH CHECK ADD FOREIGN KEY([ORDER_ID])
REFERENCES [dbo].[MANUF_ORDER] ([ORDER_ID])
GO
ALTER TABLE [dbo].[STOCK_PAYMENT]  WITH CHECK ADD FOREIGN KEY([ORDER_ID])
REFERENCES [dbo].[MANUF_ORDER] ([ORDER_ID])
GO
USE [master]
GO
ALTER DATABASE [ShowroomMaster] SET  READ_WRITE 
GO
