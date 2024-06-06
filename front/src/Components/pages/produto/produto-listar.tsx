import { useEffect, useState } from "react";
import { Produto } from "../../../models/Produto";

//Consultar os produtos da API e exibir na tela
// - Resolver o problema de CORS (API)
// - Como exibir uma array na tela utilizando React

function ProdutoListar() {
    const[produtos, setProdutos] = useState<Produto[]>([]);
  //Executar algum código no carregamento do componente
  useEffect(() => {
    console.log("O componente foi carregado...");
    fetch("http://localhost:5090/produto/listar/")
      .then((resposta) => resposta.json())
      .then((produto : Produto) => {
        console.log(produto);
      });
  }, []);

  function cadastrarProduto(){
    const produto : Produto = {nome: "Macarrão", descricao: "Comida", quantidade: 150, valor: 15};

    fetch("http://localhost:5090/produto/cadastrar/",
    {method: "POST",
     headers: {"Content-Type": "application/json",},
     body:JSON.stringify(produto)
    })
      .then((resposta) => resposta.json())
      .then((produtos : Produto[]) => {
        setProdutos(produtos);
      });
  }

  return (
    <div>
        <button onClick={cadastrarProduto}>Cadastrar Produto</button>
      <h1>Listar Produtos</h1>
      <table>
        <thead>
            <tr>
                <th>#</th>
                <th>Nome</th>
                <th>Descrição</th>
                <th>Valor</th>
                <th>Quantidade</th>
                <th>Criado Em</th>
            </tr>
        </thead>
        <tbody>
            {produtos.map((produto) => (
             <tr>
                <td>{produto.id}</td>
                <td>{produto.nome}</td>
                <td>{produto.valor}</td>
                <td>{produto.descricao}</td>
                <td>{produto.quantidade}</td>
                <td>{produto.criadoEm}</td>
            </tr>   
            ))}
        </tbody>    
      </table>     
    </div>
  );
}

export default ProdutoListar;