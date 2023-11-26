CREATE TABLE [dbo].[Label] (
    [Id]          INT                IDENTITY (1, 1) NOT NULL,
    [Name]        NVARCHAR (20)      NOT NULL,
    [IsActive]    BIT                DEFAULT ((1)) NOT NULL,
    [IsDeleted]   BIT                DEFAULT ((0)) NOT NULL,
    [CreatedDate] DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [UpdatedDate] DATETIMEOFFSET (7) NULL,
    [DeletedDate] DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_Label] PRIMARY KEY CLUSTERED ([Id] ASC)
);

