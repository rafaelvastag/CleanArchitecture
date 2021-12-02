using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Data.Migrations
{
    public partial class SeedProduct : Migration
    {
        protected override void Up(MigrationBuilder mb)
        {
            mb.Sql("INSERT INTO Products(NAME,DESCRIPTION,PRICE,Stock,Image,CATEGORY_ID) " +
                "VALUES('Caderno espiral','Caderno espiral 100 fôlhas',7.45,50,'caderno1.jpg',1)");

            mb.Sql("INSERT INTO Products(NAME,DESCRIPTION,PRICE,Stock,Image,CATEGORY_ID) " +
            "VALUES('Estojo escolar','Estojo escolar cinza',5.65,70,'estojo1.jpg',1)");

            mb.Sql("INSERT INTO Products(NAME,DESCRIPTION,PRICE,Stock,Image,CATEGORY_ID) " +
            "VALUES('Borracha escolar','Borracha branca pequena',3.25,80,'borracha1.jpg',1)");

            mb.Sql("INSERT INTO Products(NAME,DESCRIPTION,PRICE,Stock,Image,CATEGORY_ID) " +
            "VALUES('Calculadora escolar','Calculadora simples',15.39,20,'calculadora1.jpg',2)");
        }

        protected override void Down(MigrationBuilder mb)
        {
            mb.Sql("DELETE FROM Products");
        }
    }
}
