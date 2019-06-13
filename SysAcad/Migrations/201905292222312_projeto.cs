namespace SysAcad.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class projeto : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Exercicios",
                c => new
                    {
                        ExercicioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Link = c.String(),
                        UsuarioDono_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.ExercicioId)
                .ForeignKey("dbo.Usuarios", t => t.UsuarioDono_UsuarioId)
                .Index(t => t.UsuarioDono_UsuarioId);
            
            CreateTable(
                "dbo.Usuarios",
                c => new
                    {
                        UsuarioId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        CPF = c.String(),
                        Idade = c.Int(nullable: false),
                        Genero = c.String(),
                        Peso = c.Int(nullable: false),
                        Altura = c.Double(nullable: false),
                        Senha = c.String(),
                        IsAdmin = c.Boolean(nullable: false),
                        PeriodoPreferido_PeriodoId = c.Int(),
                    })
                .PrimaryKey(t => t.UsuarioId)
                .ForeignKey("dbo.Periodos", t => t.PeriodoPreferido_PeriodoId)
                .Index(t => t.PeriodoPreferido_PeriodoId);
            
            CreateTable(
                "dbo.Periodos",
                c => new
                    {
                        PeriodoId = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        HoraInicio = c.DateTime(nullable: false),
                        HoraFim = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PeriodoId);
            
            CreateTable(
                "dbo.Presencas",
                c => new
                    {
                        PresencaId = c.Int(nullable: false, identity: true),
                        Chegada = c.DateTime(nullable: false),
                        Saida = c.DateTime(nullable: false),
                        Usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.PresencaId)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId)
                .Index(t => t.Usuario_UsuarioId);
            
            CreateTable(
                "dbo.Finalizacoes",
                c => new
                    {
                        FinalizacaoId = c.Int(nullable: false, identity: true),
                        Data = c.DateTime(nullable: false),
                        QuantidadeRealizada = c.Int(nullable: false),
                        Treino_TreinoId = c.Int(),
                    })
                .PrimaryKey(t => t.FinalizacaoId)
                .ForeignKey("dbo.Treinos", t => t.Treino_TreinoId)
                .Index(t => t.Treino_TreinoId);
            
            CreateTable(
                "dbo.ItensTreino",
                c => new
                    {
                        ItemTreinoId = c.Int(nullable: false, identity: true),
                        Repeticoes = c.Int(nullable: false),
                        Series = c.Int(nullable: false),
                        Exercicio_ExercicioId = c.Int(),
                        Treino_TreinoId = c.Int(),
                    })
                .PrimaryKey(t => t.ItemTreinoId)
                .ForeignKey("dbo.Exercicios", t => t.Exercicio_ExercicioId)
                .ForeignKey("dbo.Treinos", t => t.Treino_TreinoId)
                .Index(t => t.Exercicio_ExercicioId)
                .Index(t => t.Treino_TreinoId);
            
            CreateTable(
                "dbo.Pesos",
                c => new
                    {
                        PesoId = c.Int(nullable: false, identity: true),
                        Quantidade = c.Double(nullable: false),
                        Data = c.DateTime(nullable: false),
                        Aluno_UsuarioId = c.Int(),
                        Exercicio_ExercicioId = c.Int(),
                    })
                .PrimaryKey(t => t.PesoId)
                .ForeignKey("dbo.Usuarios", t => t.Aluno_UsuarioId)
                .ForeignKey("dbo.Exercicios", t => t.Exercicio_ExercicioId)
                .Index(t => t.Aluno_UsuarioId)
                .Index(t => t.Exercicio_ExercicioId);
            
            CreateTable(
                "dbo.Treinos",
                c => new
                    {
                        TreinoId = c.Int(nullable: false, identity: true),
                        QuantidadeTreinos = c.Int(nullable: false),
                        DataExpiracao = c.DateTime(nullable: false),
                        Nome = c.String(),
                        Usuario_UsuarioId = c.Int(),
                    })
                .PrimaryKey(t => t.TreinoId)
                .ForeignKey("dbo.Usuarios", t => t.Usuario_UsuarioId)
                .Index(t => t.Usuario_UsuarioId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Treinos", "Usuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Finalizacoes", "Treino_TreinoId", "dbo.Treinos");
            DropForeignKey("dbo.ItensTreino", "Treino_TreinoId", "dbo.Treinos");
            DropForeignKey("dbo.Pesos", "Exercicio_ExercicioId", "dbo.Exercicios");
            DropForeignKey("dbo.Pesos", "Aluno_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.ItensTreino", "Exercicio_ExercicioId", "dbo.Exercicios");
            DropForeignKey("dbo.Exercicios", "UsuarioDono_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Presencas", "Usuario_UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.Usuarios", "PeriodoPreferido_PeriodoId", "dbo.Periodos");
            DropIndex("dbo.Treinos", new[] { "Usuario_UsuarioId" });
            DropIndex("dbo.Pesos", new[] { "Exercicio_ExercicioId" });
            DropIndex("dbo.Pesos", new[] { "Aluno_UsuarioId" });
            DropIndex("dbo.ItensTreino", new[] { "Treino_TreinoId" });
            DropIndex("dbo.ItensTreino", new[] { "Exercicio_ExercicioId" });
            DropIndex("dbo.Finalizacoes", new[] { "Treino_TreinoId" });
            DropIndex("dbo.Presencas", new[] { "Usuario_UsuarioId" });
            DropIndex("dbo.Usuarios", new[] { "PeriodoPreferido_PeriodoId" });
            DropIndex("dbo.Exercicios", new[] { "UsuarioDono_UsuarioId" });
            DropTable("dbo.Treinos");
            DropTable("dbo.Pesos");
            DropTable("dbo.ItensTreino");
            DropTable("dbo.Finalizacoes");
            DropTable("dbo.Presencas");
            DropTable("dbo.Periodos");
            DropTable("dbo.Usuarios");
            DropTable("dbo.Exercicios");
        }
    }
}
