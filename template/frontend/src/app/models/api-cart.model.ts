export interface ApiCartItem {
  name: string; // Corresponde a productName
  quantity: number; // Corresponde a quantityInCart
  unitPrice: number; // Preço unitário COM DESCONTO
  originalUnitPrice: number; // Preço unitário original
  discountPercentage: number;
}

export interface ApiCartResponse {
  data: ApiCartItem | null;
  success: boolean;
  message: string;
  errors: string[];
}