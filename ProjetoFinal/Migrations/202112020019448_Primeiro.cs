namespace ProjetoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Primeiro : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Aluno",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Cpf = c.String(),
                        Email = c.String(),
                        Telefone = c.String(),
                        DataInscricao = c.DateTime(nullable: false),
                        CursoId = c.Int(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Curso", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.CursoId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Curso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                        QuantidadeVagas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Usuario",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Senha = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Aluno", "UsuarioId", "dbo.Usuario");
            DropForeignKey("dbo.Aluno", "CursoId", "dbo.Curso");
            DropIndex("dbo.Aluno", new[] { "UsuarioId" });
            DropIndex("dbo.Aluno", new[] { "CursoId" });
            DropTable("dbo.Usuario");
            DropTable("dbo.Curso");
            DropTable("dbo.Aluno");
        }
    }
}
