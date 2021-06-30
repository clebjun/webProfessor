CREATE TABLE [dbo].[Professors] (
    [Codigo] INT           IDENTITY (1, 1) NOT NULL,
    [Nome]   NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_dbo.Professors] PRIMARY KEY CLUSTERED ([Codigo] ASC)
);

