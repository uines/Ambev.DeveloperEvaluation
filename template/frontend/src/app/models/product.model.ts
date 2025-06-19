export interface Product {
  productId: string;
  productName: string;
  quantity: number;
  unitPrice: number; 
  imageUrl: string;

  originalUnitPrice: number; 
  discountPercentage?: number;
}