import React, { useState } from 'react'

function Soma(){

    //1 - Exibir o valor do contador no HTML
    //2 - Somar os dois números da caixa de texto e exibir no navegador

    const [contador, setContador] = useState(0)

    function clicar(){
        setContador(contador + 1);
        console.log(contador);
    }

    return(
        <div>
         <h1>SOMA</h1>
         <label>Primeiro valor:</label>
         <input type="text"></input> <br />
         <label>Segundo valor:</label>
         <input type="text" /> <br />
         <button onClick={clicar}>Calcular</button>
        </div>
    );
}

export default Soma;