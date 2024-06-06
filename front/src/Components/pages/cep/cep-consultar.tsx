import { useEffect, useState } from "react";
import { Endereco } from "../../../models/Endereco";

function CepConsultar() {

    const[cep, setCep] = useState("");
    const[logradouro, setLogradouro] = useState("");
    const[bairro, setBairro] = useState("");
    const[uf, setUf] = useState("");
    const[localidade, setLocalidade] = useState("");

  //Executar algum código no carregamento do componente
  useEffect(() => {
    console.log("O componente foi carregado...");
    //carregarCep();
  }, []);

  function carregarCep(){
        //FETCH ou AXIOS
    fetch("https://viacep.com.br/ws/"+ cep +"/json/")
      .then((resposta) => resposta.json())
      .then((endereco: Endereco) => {
        setBairro(endereco.bairro);
        setLogradouro(endereco.logradouro);
        setLocalidade(endereco.localidade);
        setUf(endereco.uf);
      });
  }

  return (
    <div>
      <h1>Consultar CEP</h1>
      <input type="text" placeholder="Digite o CEP" onBlur={carregarCep} onChange={(e : any) => setCep(e.target.value)}/>


      <input type="text" value={bairro} disabled/> <br/>
      <input type="text" value={localidade} disabled/> <br/>
      <input type="text" value={logradouro} disabled/> <br/>
      <input type="text" value={uf} disabled/> <br/>
    </div>
  );
}

export default CepConsultar;