USE [master]
GO
/****** Object:  Database [LeaveManagementDb]    Script Date: 4/17/2018 1:55:03 AM ******/
CREATE DATABASE [LeaveManagementDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'LeaveManagementDb', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LeaveManagementDb.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'LeaveManagementDb_log', FILENAME = N'C:\Program Files (x86)\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\LeaveManagementDb_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [LeaveManagementDb] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [LeaveManagementDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [LeaveManagementDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [LeaveManagementDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [LeaveManagementDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [LeaveManagementDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [LeaveManagementDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [LeaveManagementDb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET RECOVERY FULL 
GO
ALTER DATABASE [LeaveManagementDb] SET  MULTI_USER 
GO
ALTER DATABASE [LeaveManagementDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [LeaveManagementDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [LeaveManagementDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [LeaveManagementDb] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [LeaveManagementDb]
GO
/****** Object:  Table [dbo].[tb_AllocationLeave]    Script Date: 4/17/2018 1:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tb_AllocationLeave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NULL,
	[LeaveTypeId] [int] NULL,
	[NumberOfLeave] [int] NULL,
 CONSTRAINT [PK_tb_AllocationLeave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tb_Designation]    Script Date: 4/17/2018 1:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Designation](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DisignationName] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_Designation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_Employee]    Script Date: 4/17/2018 1:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeName] [varchar](50) NOT NULL,
	[Email] [varchar](50) NOT NULL,
	[DesignationId] [int] NOT NULL,
	[Password] [varchar](50) NULL,
	[UserTypeId] [int] NULL,
 CONSTRAINT [PK_tb_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_EmployeeLeave]    Script Date: 4/17/2018 1:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_EmployeeLeave](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [int] NOT NULL,
	[LeaveTypeId] [int] NOT NULL,
	[StartDate] [date] NOT NULL,
	[EndDate] [date] NOT NULL,
	[TotalDay] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[EntryDate] [date] NULL,
 CONSTRAINT [PK_tb_EmployeeLeave] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tb_LeaveType]    Script Date: 4/17/2018 1:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tb_LeaveType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeaveType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tb_LeaveType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserType]    Script Date: 4/17/2018 1:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserType] [varchar](50) NULL,
 CONSTRAINT [PK_UserType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tb_AllocationLeave] ON 

INSERT [dbo].[tb_AllocationLeave] ([Id], [EmployeeId], [LeaveTypeId], [NumberOfLeave]) VALUES (34, 17, 1, 15)
INSERT [dbo].[tb_AllocationLeave] ([Id], [EmployeeId], [LeaveTypeId], [NumberOfLeave]) VALUES (35, 17, 2, 15)
INSERT [dbo].[tb_AllocationLeave] ([Id], [EmployeeId], [LeaveTypeId], [NumberOfLeave]) VALUES (36, 18, 1, 15)
INSERT [dbo].[tb_AllocationLeave] ([Id], [EmployeeId], [LeaveTypeId], [NumberOfLeave]) VALUES (37, 18, 2, 15)
INSERT [dbo].[tb_AllocationLeave] ([Id], [EmployeeId], [LeaveTypeId], [NumberOfLeave]) VALUES (38, 19, 1, 15)
INSERT [dbo].[tb_AllocationLeave] ([Id], [EmployeeId], [LeaveTypeId], [NumberOfLeave]) VALUES (39, 19, 2, 15)
SET IDENTITY_INSERT [dbo].[tb_AllocationLeave] OFF
SET IDENTITY_INSERT [dbo].[tb_Designation] ON 

INSERT [dbo].[tb_Designation] ([Id], [DisignationName]) VALUES (4, N'CEO')
INSERT [dbo].[tb_Designation] ([Id], [DisignationName]) VALUES (5, N'Intern')
INSERT [dbo].[tb_Designation] ([Id], [DisignationName]) VALUES (6, N'Developer')
SET IDENTITY_INSERT [dbo].[tb_Designation] OFF
SET IDENTITY_INSERT [dbo].[tb_Employee] ON 

INSERT [dbo].[tb_Employee] ([Id], [EmployeeName], [Email], [DesignationId], [Password], [UserTypeId]) VALUES (17, N'Md Nora Alam', N'nora.alam07@gmail.com', 6, N'123456', 1)
INSERT [dbo].[tb_Employee] ([Id], [EmployeeName], [Email], [DesignationId], [Password], [UserTypeId]) VALUES (18, N'Md Jasim Uddin', N'ujasim683@gmail.com', 6, N'123456', 2)
INSERT [dbo].[tb_Employee] ([Id], [EmployeeName], [Email], [DesignationId], [Password], [UserTypeId]) VALUES (19, N'Nasir Rana', N'nasirhrana@gmail.com', 6, N'123456', 2)
SET IDENTITY_INSERT [dbo].[tb_Employee] OFF
SET IDENTITY_INSERT [dbo].[tb_EmployeeLeave] ON 

INSERT [dbo].[tb_EmployeeLeave] ([Id], [EmployeeId], [LeaveTypeId], [StartDate], [EndDate], [TotalDay], [Status], [EntryDate]) VALUES (89, 18, 1, CAST(0x113E0B00 AS Date), CAST(0x123E0B00 AS Date), 2, N'Approve', CAST(0x203E0B00 AS Date))
INSERT [dbo].[tb_EmployeeLeave] ([Id], [EmployeeId], [LeaveTypeId], [StartDate], [EndDate], [TotalDay], [Status], [EntryDate]) VALUES (90, 19, 1, CAST(0x163E0B00 AS Date), CAST(0x173E0B00 AS Date), 2, N'Approve', CAST(0x203E0B00 AS Date))
SET IDENTITY_INSERT [dbo].[tb_EmployeeLeave] OFF
SET IDENTITY_INSERT [dbo].[tb_LeaveType] ON 

INSERT [dbo].[tb_LeaveType] ([Id], [LeaveType]) VALUES (1, N'Sick Leave')
INSERT [dbo].[tb_LeaveType] ([Id], [LeaveType]) VALUES (2, N'Casual Leave')
SET IDENTITY_INSERT [dbo].[tb_LeaveType] OFF
SET IDENTITY_INSERT [dbo].[UserType] ON 

INSERT [dbo].[UserType] ([Id], [UserType]) VALUES (1, N'admin')
INSERT [dbo].[UserType] ([Id], [UserType]) VALUES (2, N'user')
SET IDENTITY_INSERT [dbo].[UserType] OFF
ALTER TABLE [dbo].[tb_AllocationLeave]  WITH CHECK ADD  CONSTRAINT [FK_tb_AllocationLeave_tb_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tb_Employee] ([Id])
GO
ALTER TABLE [dbo].[tb_AllocationLeave] CHECK CONSTRAINT [FK_tb_AllocationLeave_tb_Employee]
GO
ALTER TABLE [dbo].[tb_AllocationLeave]  WITH CHECK ADD  CONSTRAINT [FK_tb_AllocationLeave_tb_LeaveType] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[tb_LeaveType] ([Id])
GO
ALTER TABLE [dbo].[tb_AllocationLeave] CHECK CONSTRAINT [FK_tb_AllocationLeave_tb_LeaveType]
GO
ALTER TABLE [dbo].[tb_Employee]  WITH CHECK ADD  CONSTRAINT [FK_tb_Employee_tb_Designation] FOREIGN KEY([DesignationId])
REFERENCES [dbo].[tb_Designation] ([Id])
GO
ALTER TABLE [dbo].[tb_Employee] CHECK CONSTRAINT [FK_tb_Employee_tb_Designation]
GO
ALTER TABLE [dbo].[tb_Employee]  WITH CHECK ADD  CONSTRAINT [FK_tb_Employee_UserType] FOREIGN KEY([UserTypeId])
REFERENCES [dbo].[UserType] ([Id])
GO
ALTER TABLE [dbo].[tb_Employee] CHECK CONSTRAINT [FK_tb_Employee_UserType]
GO
ALTER TABLE [dbo].[tb_EmployeeLeave]  WITH CHECK ADD  CONSTRAINT [FK_tb_EmployeeLeave_tb_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[tb_Employee] ([Id])
GO
ALTER TABLE [dbo].[tb_EmployeeLeave] CHECK CONSTRAINT [FK_tb_EmployeeLeave_tb_Employee]
GO
ALTER TABLE [dbo].[tb_EmployeeLeave]  WITH CHECK ADD  CONSTRAINT [FK_tb_EmployeeLeave_tb_LeaveType] FOREIGN KEY([LeaveTypeId])
REFERENCES [dbo].[tb_LeaveType] ([Id])
GO
ALTER TABLE [dbo].[tb_EmployeeLeave] CHECK CONSTRAINT [FK_tb_EmployeeLeave_tb_LeaveType]
GO
USE [master]
GO
ALTER DATABASE [LeaveManagementDb] SET  READ_WRITE 
GO
