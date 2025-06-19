export interface ProductSale {
  name: string;
  quantity: number;
  unitPrice: number;
}

export interface SaleRequest {
  branch: string;
  products: ProductSale[];
}

export interface SaleResponse {
  success: boolean;
  message: string;
  data?: any;
  errors?: string[];
}