CREATE DATABASE [PhoneBookApp]
GO
USE [PhoneBookApp]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/27/2023 8:52:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 11/27/2023 8:52:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GivenName] [nvarchar](20) NOT NULL,
	[FamilyName] [nvarchar](20) NOT NULL,
	[MobileNumber] [nvarchar](20) NOT NULL,
	[BirthDate] [date] NULL,
	[LabelId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Label]    Script Date: 11/27/2023 8:52:32 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Label](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[CreatedDate] [datetimeoffset](7) NOT NULL,
	[UpdatedDate] [datetimeoffset](7) NULL,
	[DeletedDate] [datetimeoffset](7) NULL,
 CONSTRAINT [PK_Label] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231126053337_CreateLabelSchema', N'8.0.0')
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20231126053626_CreateContactSchema', N'8.0.0')
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 
GO
INSERT [dbo].[Contact] ([Id], [GivenName], [FamilyName], [MobileNumber], [BirthDate], [LabelId], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (1, N'Jose', N'Rizal', N'+639171234567', CAST(N'1861-06-19' AS Date), 2, 1, 0, CAST(N'2023-11-26T04:23:54.4300000+00:00' AS DateTimeOffset), NULL, NULL)
GO
INSERT [dbo].[Contact] ([Id], [GivenName], [FamilyName], [MobileNumber], [BirthDate], [LabelId], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (2, N'Andres', N'Bonifacio', N'+639181234567', CAST(N'1863-11-30' AS Date), 2, 1, 0, CAST(N'2023-11-26T04:25:30.2166667+00:00' AS DateTimeOffset), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[Label] ON 
GO
INSERT [dbo].[Label] ([Id], [Name], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (1, N'Family', 1, 0, CAST(N'2023-11-26T04:21:47.4933333+00:00' AS DateTimeOffset), NULL, NULL)
GO
INSERT [dbo].[Label] ([Id], [Name], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (2, N'Colleague', 1, 0, CAST(N'2023-11-26T04:22:12.4800000+00:00' AS DateTimeOffset), NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Label] OFF
GO
/****** Object:  Index [IX_Contact_LabelId]    Script Date: 11/27/2023 8:52:32 AM ******/
CREATE NONCLUSTERED INDEX [IX_Contact_LabelId] ON [dbo].[Contact]
(
	[LabelId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Contact] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Label] ADD  DEFAULT (CONVERT([bit],(1))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Label] ADD  DEFAULT (getutcdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[Contact]  WITH CHECK ADD  CONSTRAINT [FK_Contact_Label] FOREIGN KEY([LabelId])
REFERENCES [dbo].[Label] ([Id])
GO
ALTER TABLE [dbo].[Contact] CHECK CONSTRAINT [FK_Contact_Label]
GO
USE [master]
GO
ALTER DATABASE [PhoneBookApp] SET  READ_WRITE 
GO
