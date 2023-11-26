﻿USE [PhoneBookApp]
GO
IF NOT EXISTS (SELECT 1 FROM [dbo].[Label] WHERE [Id] IN (1, 2))
    BEGIN
        SET IDENTITY_INSERT [dbo].[Label] ON 
        INSERT [dbo].[Label] ([Id], [Name], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (1, N'Family', 1, 0, CAST(N'2023-11-26T04:21:47.4933333+00:00' AS DateTimeOffset), NULL, NULL)
        INSERT [dbo].[Label] ([Id], [Name], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (2, N'Colleague', 1, 0, CAST(N'2023-11-26T04:22:12.4800000+00:00' AS DateTimeOffset), NULL, NULL)
        SET IDENTITY_INSERT [dbo].[Label] OFF
    END
IF NOT EXISTS (SELECT 1 FROM [dbo].[Contact] WHERE [Id] IN (1, 2))
    BEGIN
        SET IDENTITY_INSERT [dbo].[Contact] ON 
        INSERT [dbo].[Contact] ([Id], [GivenName], [FamilyName], [MobileNumber], [BirthDate], [LabelId], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (1, N'Jose', N'Rizal', N'+639171234567', CAST(N'1861-06-19' AS Date), 2, 1, 0, CAST(N'2023-11-26T04:23:54.4300000+00:00' AS DateTimeOffset), NULL, NULL)
        INSERT [dbo].[Contact] ([Id], [GivenName], [FamilyName], [MobileNumber], [BirthDate], [LabelId], [IsActive], [IsDeleted], [CreatedDate], [UpdatedDate], [DeletedDate]) VALUES (2, N'Andres', N'Bonifacio', N'+639181234567', CAST(N'1863-11-30' AS Date), 2, 1, 0, CAST(N'2023-11-26T04:25:30.2166667+00:00' AS DateTimeOffset), NULL, NULL)
        SET IDENTITY_INSERT [dbo].[Contact] OFF
    END