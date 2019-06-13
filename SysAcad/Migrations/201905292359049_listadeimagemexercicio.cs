namespace SysAcad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class listadeimagemexercicio : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Imagens", "Exercicio_ExercicioId", c => c.Int());
            CreateIndex("dbo.Imagens", "Exercicio_ExercicioId");
            AddForeignKey("dbo.Imagens", "Exercicio_ExercicioId", "dbo.Exercicios", "ExercicioId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Imagens", "Exercicio_ExercicioId", "dbo.Exercicios");
            DropIndex("dbo.Imagens", new[] { "Exercicio_ExercicioId" });
            DropColumn("dbo.Imagens", "Exercicio_ExercicioId");
        }
    }
}
