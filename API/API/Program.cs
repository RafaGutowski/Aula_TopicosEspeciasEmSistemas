using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDataContext>();

//configurar a policy de CORS para liberar o acesso total
builder.Services.AddCors(
    options => options.AddPolicy("Acesso Total", builder => builder
    .AllowAnyOrigin() 
    .AllowAnyHeader() 
    .AllowAnyMethod())
);

var app = builder.Build();

List<Produto> produtos =
[
    new Produto("Celular", "IOS", 5000),
    new Produto("Celular", "Android", 4000),
    new Produto("Televisão", "LG", 2300.5),
    new Produto("Placa de Vídeo", "NVIDIA", 2500),
];

//Funcionalidades da aplicação - EndPoints

// GET: http://localhost:5008/
app.MapGet("/", () => "API de Produtos");

// GET: http://localhost:5008/produto/listar
app.MapGet("/produto/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.TabelaProdutos.Any())
    {
        return Results.Ok(ctx.TabelaProdutos.ToList());
    }
    return Results.NotFound("Não existe produtos na tabela");
});

// GET: http://localhost:5008/produto/buscar/nomedoproduto
app.MapGet("/produto/buscar/{nome}", ([FromRoute] string nome) =>
    {
        for (int i = 0; i < produtos.Count; i++)
        {
            if (produtos[i].Nome == nome)
            {
                //retornar o produto encontrado
                return Results.Ok(produtos[i]);
            }
        }
        return Results.NotFound("Produto não encontrado!");
    }
);




//EXERCÍCIOS
//1) Cadastrar um produto 
//a) Pela URL
// POST: http://localhost:5008/produto/cadastrar
app.MapPost("/produto/cadastrar/{nome}/{descricao}/{valor}", ([FromRoute] string nome, [FromRoute] string descricao, [FromRoute] double valor) =>
    {
        //Preencher o objeto pelo construtor
        Produto produto = new Produto(nome, descricao, valor);

        //Preencher o objeto pelos atributos
        produto.Nome = nome;
        produto.Descricao = descricao;
        produto.Valor = valor;

        //Adicionar o objeto dentro da lista
        produtos.Add(produto);
        return Results.Created("", produto);
    }
);

//b) Pelo corpo
app.MapPost("/produto/cadastrar", ([FromBody] Produto produto, [FromServices] AppDataContext ctx) =>
    {
        //Adicionar o objeto dentro da tabela no banco de dados
        ctx.TabelaProdutos.Add(produto);
        ctx.SaveChanges();
        return Results.Created("", produto);
    }
);

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
    return Results.NotFound("Produto não encontrado!");
});

//3) Alteração do produto
app.MapPatch("/produto/alterar/{nome}/{descricao}", ([FromRoute] string nome, [FromRoute] string descricao) =>
{
    for (int i = 0; i < produtos.Count; i++)
    {
        if (produtos[i].Nome == nome)
        {
            produtos[i].Descricao = descricao;
            return Results.Ok("Descrição alterada");
        }
    }
    return Results.NotFound("Produto não encontrado!");
});




app.UseCors("Acesso Total");
app.Run();