export interface OrderProduct {
  id: string;
  name: string;
  quantity: number;
  unitPrice: number;
  originalUnitPrice: number;
  discountPercentage: number;
}

export interface Order {
  id: string;
  date: string;
  customerId: string;
  totalSaleAmount: number;
  branch: string;
  isCanceled: boolean;
  products: OrderProduct[];
  originalTotalPrice: number;
}

export interface ApiErrorDetail {
  error: string;
  detail: string;
}
export interface OrderListResponse {
  success: boolean;
  message: string;
  errors: ApiErrorDetail[] | null;
  data: Order[];
}