namespace WebProfessor.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update2 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Pessoas");
        }
        
        public override void Down()
        {
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
    }
}
