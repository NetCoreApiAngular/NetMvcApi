USE master
GO
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'HrManagement')
BEGIN
  CREATE DATABASE MyTestDataBase;
END;
GO

USE [HrManagement]
GO
/****** Object:  Table [dbo].[AccountToken]    Script Date: 3/23/2022 10:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountToken](
	[AccountTokenId] [int] IDENTITY(1,1) NOT NULL,
	[AccountId] [int] NOT NULL,
	[IsAdminAccountSide] [bit] NULL,
	[TokenKey] [varchar](500) NULL,
	[ExpiredDate] [datetime] NULL,
	[TokenType] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [varchar](50) NULL,
 CONSTRAINT [PK_AccountToken] PRIMARY KEY CLUSTERED 
(
	[AccountTokenId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 3/23/2022 10:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [varchar](50) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Status] [int] NOT NULL,
	[IsCalculateSalaryAssignment] [bit] NOT NULL,
	[NumericalOrder] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[UpdatedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ModuleAction]    Script Date: 3/23/2022 10:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ModuleAction](
	[ModuleActionId] [int] IDENTITY(1,1) NOT NULL,
	[Module] [varchar](50) NULL,
	[Action] [varchar](50) NULL,
	[Description] [varchar](500) NULL,
	[OrderIndex] [int] NULL,
	[Group] [int] NULL,
 CONSTRAINT [PK_ModuleAction] PRIMARY KEY CLUSTERED 
(
	[ModuleActionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RoleModuleAction]    Script Date: 3/23/2022 10:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RoleModuleAction](
	[RoleModuleActionID] [int] IDENTITY(1,1) NOT NULL,
	[RoleID] [int] NOT NULL,
	[ModuleActionID] [int] NOT NULL,
 CONSTRAINT [PK_RoleModuleAction] PRIMARY KEY CLUSTERED 
(
	[RoleModuleActionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 3/23/2022 10:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](50) NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Note] [nvarchar](300) NULL,
	[Status] [bit] NOT NULL,
	[Type] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[CreatedByUserId] [nvarchar](50) NULL,
	[UpdatedUserId] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/23/2022 10:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Email] [nvarchar](250) NULL,
	[Password] [nvarchar](500) NULL,
	[PasswordSalt] [nvarchar](500) NULL,
	[IsSupperAdmin] [bit] NOT NULL,
	[ApplicationId] [int] NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NULL,
	[LastLoginDate] [datetime] NULL,
	[LastActivityDate] [datetime] NULL,
	[IsLockedOut] [bit] NOT NULL,
	[Avatar] [nvarchar](500) NULL,
	[Status] [bit] NOT NULL,
	[FullName] [nvarchar](150) NULL,
	[Tel] [nvarchar](50) NULL,
	[IpAddress] [nvarchar](50) NULL,
	[Country] [nvarchar](150) NULL,
	[CountryCode] [nvarchar](50) NULL,
	[Description] [nvarchar](1000) NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 3/23/2022 10:21:44 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserRoleId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RoleId] [int] NOT NULL,
	[CreatedDate] [datetime] NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserRoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[User] ON 
GO
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [PasswordSalt], [IsSupperAdmin], [ApplicationId], [CreatedDate], [UpdatedDate], [LastLoginDate], [LastActivityDate], [IsLockedOut], [Avatar], [Status], [FullName], [Tel], [IpAddress], [Country], [CountryCode], [Description]) VALUES (1, N'Admin', N'admin@nhonhoa.vn', N'1000:paqv42x0zf32cagcDdRNXcpLPTqVDovH:0+A7K0tvhPf6HV3HU2CZ2tUYYIAlTu9X', N'1000:4XwRFklHMIqnUCvhhZxx8Njut1rgzX3+:UiUrwx8Kuhyw3aRx0f2JZETaWv6PjXyw', 1, NULL, CAST(N'2016-10-04T00:00:00.000' AS DateTime), CAST(N'2016-10-04T00:00:00.000' AS DateTime), CAST(N'2016-10-04T00:00:00.000' AS DateTime), CAST(N'2016-10-04T00:00:00.000' AS DateTime), 0, NULL, 1, N'', NULL, NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [PasswordSalt], [IsSupperAdmin], [ApplicationId], [CreatedDate], [UpdatedDate], [LastLoginDate], [LastActivityDate], [IsLockedOut], [Avatar], [Status], [FullName], [Tel], [IpAddress], [Country], [CountryCode], [Description]) VALUES (2, N'user1', N'user1@gmail.com', N'1000:nhHLGB7wN3Dtp0JaiGxiWTSpiKcgV0Nu:Qxr89lLdYRpyVxQx1gpUYZBdGweZPg8o', N'1000:J76un78GG2bX+ZuZem3DDHxtK52S4i8m:5RbwVxA/OOoekNqH9lj2rMhIsUxfh5Ck', 0, NULL, CAST(N'2020-04-27T17:47:13.577' AS DateTime), NULL, NULL, NULL, 0, N'', 1, NULL, N'string', NULL, NULL, NULL, NULL)
GO
INSERT [dbo].[User] ([UserId], [UserName], [Email], [Password], [PasswordSalt], [IsSupperAdmin], [ApplicationId], [CreatedDate], [UpdatedDate], [LastLoginDate], [LastActivityDate], [IsLockedOut], [Avatar], [Status], [FullName], [Tel], [IpAddress], [Country], [CountryCode], [Description]) VALUES (3, N'kinhdoanh', N'admin22@nhonhoa.vn', N'1000:Uqzoos1oUtbPn3vjvJCWW+NHuKwrq+wA:VD7xdx6vfOYExDkXgXvow7Wf36MtBXjH', N'1000:aiScx23bzlCxBX0MQ/XKfjSh4i0illLx:/00f7lBthrrbFDujdhDtukgfh0W3qefw', 0, NULL, CAST(N'2020-07-06T07:00:42.353' AS DateTime), NULL, NULL, NULL, 0, NULL, 1, N'cccccccccc', NULL, NULL, NULL, NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Department] ADD  CONSTRAINT [DF_Department_IsCalculateSalaryAssignment]  DEFAULT ((0)) FOR [IsCalculateSalaryAssignment]
GO
ALTER TABLE [dbo].[RoleModuleAction]  WITH CHECK ADD  CONSTRAINT [FK_RoleModuleAction_ModuleAction] FOREIGN KEY([ModuleActionID])
REFERENCES [dbo].[ModuleAction] ([ModuleActionId])
GO
ALTER TABLE [dbo].[RoleModuleAction] CHECK CONSTRAINT [FK_RoleModuleAction_ModuleAction]
GO
ALTER TABLE [dbo].[RoleModuleAction]  WITH CHECK ADD  CONSTRAINT [FK_RoleModuleAction_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[RoleModuleAction] CHECK CONSTRAINT [FK_RoleModuleAction_Roles]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'AccountType = 0(User  of client side), AccountType=1 User  of admin side' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'AccountToken', @level2type=N'COLUMN',@level2name=N'IsAdminAccountSide'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Mã bộ phận' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'DepartmentId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Tên bộ phận' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'Name'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'Trạng thái' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'Department', @level2type=N'COLUMN',@level2name=N'Status'
GO
USE [master]
GO
ALTER DATABASE [HrManagement] SET  READ_WRITE 
GO
