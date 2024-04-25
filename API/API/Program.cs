using API.Models;
using Microsoft.AspNetCore.Mvc;


    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();


        //List<Produto> produtos = new Produto();
        List<Produto> produtos =
        [
            new Produto("Celular", "IOS", 5000),
    new Produto("Celular", "Android", 4000),
    new Produto("Televisão", "LG", 2300.5),
    new Produto("Placa de Vídeo", "NVIDIA", 3500),
    new Produto("Placa de Vídeo", "AMD", 1900),
];

        //Funcionalidades da aplicação - EndPoints

        // GET: http://localhost:5090/
        app.MapGet("/", () => "API de Produtos");

        // GET: http://localhost:5090/produto/listar
        app.MapGet("/produto/listar", () =>
            produtos);

        // GET: http://localhost:5090/produto/buscar/iddoproduto
        app.MapGet("/produto/buscar/{nome}", ([FromRoute] string id) =>
            {
                produtos produto = ctx.Produtos.Find(id);
                if(produto is null){
                    return Results.NotFound("Produto não encontrado.")
                }
                return Results.Ok(produto);
            }
        );

        //EXERCÍCIOS
        //1) Cadastrar um produto 
        // POST: http://localhost:5090/produto/cadastrar
        // a) Pela URL - app.MapPost("/produto/cadastrar/{nome}/{descricao}/{valor}", ([FromRoute] string nome, [FromRoute] string descricao, [FromRoute] double valor) => 
        // {
        //     //Preencher o objeto pelo construtor
        //     Produto produto = new Produto(nome, descricao, valor);  

        //     //Preencher o objeto pelos atributos
        //     produto.Nome = nome;
        //     produto.Descricao = descricao;
        //     produto.Valor = valor;

        //     Adicionar o objeto dentro da lista
        //     produtos.Add(produto);
        //     return Results.Created("", produto);
        // });

        //b) Pelo corpo
        app.MapPost("/produto/cadastrar", ([FromBody] Produto produto) =>
            {
                // //Preencher o objeto pelo construtor
                // Produto produto = new Produto(nome, descricao, valor);  

                // //Preencher o objeto pelos atributos
                // produto.Nome = nome;
                // produto.Descricao = descricao;
                // produto.Valor = valor;

                //Adicionar o objeto dentro da lista
                produtos.Add(produto);
                return Results.Created("", produto);
            });

        //2) Remoção do produto
        app.MapDelete("/produto/deletar/{nome}", ([FromRoute] string nome) =>
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                if (produtos[i].Nome == nome)
                {
                    produtos.RemoveAt(i);
                    return Results.Ok("Produto removido!");
                }
            }
            return Results.NotFound("Produto não encontrado");
        });
        //3) Alteração do produto

        app.MapPatch("/produto/alterar/{nome}/{descricao}/{valor}", ([FromRoute] string nome, [FromRoute] string descricao, [FromRoute] double valor) =>
        {
            for (int i = 0; i < produtos.Count; i++)
            {
                if (produtos[i].Nome == nome)
                {
                    produtos[i].Descricao = descricao;
                    produtos[i].Valor = valor;
                    return Results.Ok("Produto alterado!");
                }
            }
            return Results.NotFound("Produto não encontrado");
        });

        app.Run();
}