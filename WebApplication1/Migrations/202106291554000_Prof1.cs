namespace WebProfessor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Prof1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alunoes",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                        Mensalidade = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DataVencimento = c.DateTime(nullable: false),
                        CodigoProfessor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Codigo)
                .ForeignKey("dbo.Professors", t => t.CodigoProfessor, cascadeDelete: true)
                .Index(t => t.CodigoProfessor);
            
            CreateTable(
                "dbo.Professors",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Nome = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Codigo);
            
            CreateTable(
                "dbo.Pessoas",
                c => new
                    {
                        Codigo = c.Int(nullable: false, identity: true),
                        Cpf = c.String(nullable: false, maxLength: 11),
                        Nome = c.String(nullable: false, maxLength: 20),
                        Sobrenome = c.String(maxLength: 20),
                        DataNascimento = c.DateTime(nullable: false),
                        Email = c.String(nullable: false),
                        Telefone = c.String(),
                        NomeMae = c.String(nullable: false, maxLength: 100),
                    })
                .PrimaryKey(t => t.Codigo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Alunoes", "CodigoProfessor", "dbo.Professors");
            DropIndex("dbo.Alunoes", new[] { "CodigoProfessor" });
            DropTable("dbo.Pessoas");
            DropTable("dbo.Professors");
            DropTable("dbo.Alunoes");
        }
    }
}
