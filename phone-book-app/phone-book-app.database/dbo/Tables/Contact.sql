CREATE TABLE [dbo].[Contact] (
    [Id]           INT                IDENTITY (1, 1) NOT NULL,
    [GivenName]    NVARCHAR (20)      NOT NULL,
    [FamilyName]   NVARCHAR (20)      NOT NULL,
    [MobileNumber] NVARCHAR (20)      NOT NULL,
    [BirthDate]    DATE               NULL,
    [LabelId]      INT                NOT NULL,
    [IsActive]     BIT                DEFAULT (CONVERT([bit],(1))) NOT NULL,
    [IsDeleted]    BIT                NOT NULL,
    [CreatedDate]  DATETIMEOFFSET (7) DEFAULT (getutcdate()) NOT NULL,
    [UpdatedDate]  DATETIMEOFFSET (7) NULL,
    [DeletedDate]  DATETIMEOFFSET (7) NULL,
    CONSTRAINT [PK_Contact] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Contact_Label] FOREIGN KEY ([LabelId]) REFERENCES [dbo].[Label] ([Id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Contact_LabelId]
    ON [dbo].[Contact]([LabelId] ASC);

