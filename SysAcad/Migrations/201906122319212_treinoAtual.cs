namespace SysAcad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class treinoAtual : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TreinosAtuais",
                c => new
                    {
                        TreinoAtualId = c.Int(nullable: false, identity: true),
                        Treino_TreinoId = c.Int(),
                        Usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.TreinoAtualId)
                .ForeignKey("dbo.Treinos", t => t.Treino_TreinoId)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId)
                .Index(t => t.Treino_TreinoId)
                .Index(t => t.Usuario_UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TreinosAtuais", "Usuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.TreinosAtuais", "Treino_TreinoId", "dbo.Treinos");
            DropIndex("dbo.TreinosAtuais", new[] { "Usuario_UsuarioId" });
            DropIndex("dbo.TreinosAtuais", new[] { "Treino_TreinoId" });
            DropTable("dbo.TreinosAtuais");
        }
    }
}
