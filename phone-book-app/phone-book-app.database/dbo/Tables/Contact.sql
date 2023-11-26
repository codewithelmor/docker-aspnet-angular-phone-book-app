CREATE TABLE [dbo].[Contact] (
    [Id]           INT                IDENTITY (1, 1) NOT NULL,
    [GivenName]    NVARCHAR (20)      NOT NULL,
    [FamilyName]   NVARCHAR (20)      NOT NULL,
    [MobileNumber] NVARCHAR (20)      NOT NULL,
    [BirthDate]    DATE               NULL,
    [LabelId]      INT                NOT NULL,
    [IsActive]     BIT                DEFAULT ((1)) NOT NULL,
    [IsDeleted]    BIT                DEFAULT ((0)) NOT NULL,
    [CreatedDate]  DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [UpdatedDate]  DATETIMEOFFSET (7) NULL,
    [DeletedDate]  DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contact_Label] FOREIGN KEY ([LabelId]) REFERENCES [dbo].[Label] ([Id])
);

