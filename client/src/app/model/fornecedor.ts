export interface Fornecedor {
  id: number;
  nome: string;
  descricao: string;
  preco: number;
  imagens: string[];
  fornecerId: number;
}
