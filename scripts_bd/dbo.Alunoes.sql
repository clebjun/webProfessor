CREATE TABLE [dbo].[Alunoes] (
    [Codigo]          INT             IDENTITY (1, 1) NOT NULL,
    [Nome]            NVARCHAR (50)   NOT NULL,
    [Mensalidade]     DECIMAL (18, 2) NOT NULL,
    [DataVencimento]  DATETIME        NOT NULL,
    [CodigoProfessor] INT             NOT NULL,
    CONSTRAINT [PK_dbo.Alunoes] PRIMARY KEY CLUSTERED ([Codigo] ASC),
    CONSTRAINT [FK_dbo.Alunoes_dbo.Professors_CodigoProfessor] FOREIGN KEY ([CodigoProfessor]) REFERENCES [dbo].[Professors] ([Codigo]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_CodigoProfessor]
    ON [dbo].[Alunoes]([CodigoProfessor] ASC);

