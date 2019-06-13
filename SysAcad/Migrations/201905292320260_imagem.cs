namespace SysAcad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagem : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Imagens",
                c => new
                    {
                        ImagemId = c.Int(nullable: false, identity: true),
                        Caminho = c.String(),
                    })
                .PrimaryKey(t => t.ImagemId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Imagens");
        }
    }
}
