using Microsoft.EntityFrameworkCore;

namespace API.Models;

//Classe que representa o Entity Framework Core dentro da aplicação : Code First
public class AppDataContext : DbContext
{
    //Representação classes que vaõ virar tabelas do banco de dados
    public DbSet<Produto> TabelaProdutos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //Configuração da conexão com o banco de dados
        optionsBuilder.UseSqlite("Data Source=app.db");
    }
}
