export interface Product {
  productId: string;
  productName: string;
  quantity: number; // Estoque disponível do produto
  unitPrice: number; // Preço unitário ORIGINAL do produto
  imageUrl: string;

  // Campos populados no objeto Product DENTRO do CartItem após resposta da API.
  // O unitPrice do Product DENTRO do CartItem será o preço COM DESCONTO.
  originalUnitPrice?: number; 
  discountPercentage?: number;
}