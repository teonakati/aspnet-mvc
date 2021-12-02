namespace ProjetoFinal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandoLog : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LogAcesso",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DataAcesso = c.DateTime(nullable: false),
                        UsuarioId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Usuario", t => t.UsuarioId, cascadeDelete: true)
                .Index(t => t.UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LogAcesso", "UsuarioId", "dbo.Usuario");
            DropIndex("dbo.LogAcesso", new[] { "UsuarioId" });
            DropTable("dbo.LogAcesso");
        }
    }
}
