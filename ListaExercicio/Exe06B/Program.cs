Console.Clear();

//criar um vetor
int[] vetor = new int[10];

//Criar o objeto que vai gerar um numero random
Random aleatorio = new Random();

//Preencher vetor com valores aleatorios
for(int i = 0; i < vetor.Length; i++){
    vetor[i] = aleatorio.Next(100);
}

//Imprimir vetor
for(int i = 0; i < vetor.Length; i++){
    Console.Write(vetor[i] + " ");
}
    Console.WriteLine("\n");

  Array.Sort(vetor);

//Imprimir vetor
for(int i = 0; i < vetor.Length; i++){
    Console.Write(vetor[i] + " ");
};



