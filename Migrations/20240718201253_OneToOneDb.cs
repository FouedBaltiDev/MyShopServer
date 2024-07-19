using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyShop.Migrations
{
    public partial class OneToOneDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Modifier la colonne UserId sans essayer de supprimer un index
            migrationBuilder.Sql(@"
            -- Vérifier et supprimer l'index s'il existe (optionnel, mais non nécessaire ici)
            IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Carts]') AND name = N'IX_Carts_UserId')
            BEGIN
                DROP INDEX [IX_Carts_UserId] ON [dbo].[Carts];
            END

            -- Modifier la colonne UserId
            ALTER TABLE [dbo].[Carts] ALTER COLUMN [UserId] NVARCHAR(450) NOT NULL;

            -- Recréer l'index après modification de la colonne (si nécessaire)
            CREATE INDEX [IX_Carts_UserId] ON [dbo].[Carts] ([UserId]);
        ");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            -- Revenir la colonne UserId à int (recréer l'index, si nécessaire)
            IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[Carts]') AND name = N'IX_Carts_UserId')
            BEGIN
                DROP INDEX [IX_Carts_UserId] ON [dbo].[Carts];
            END

            -- Modifier la colonne UserId
            ALTER TABLE [dbo].[Carts] ALTER COLUMN [UserId] INT NOT NULL;
        ");

        }
    }
}
