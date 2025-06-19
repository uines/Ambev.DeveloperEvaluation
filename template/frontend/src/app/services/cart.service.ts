import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AddToCartStatus } from '../models/add-to-cart-status.model';
import { ApiCartResponse, ApiCartItem } from '../models/api-cart.model';
import { SaleRequest, SaleResponse } from '../models/sale.model';
import { environment } from '../../environments/environment';
import { OrderListResponse, Order } from '../models/order.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private items: CartItem[] = [];
  private cartItemsSource = new BehaviorSubject<CartItem[]>([]);
  cartItems$ = this.cartItemsSource.asObservable();
  private apiUrl = `${environment.apiUrlBase}/api/Cart`;
  private saleApiUrl = `${environment.apiUrlBase}/api/Sale`;
  private myOrdersApiUrl = `${environment.apiUrlBase}/api/Sale/Me`;

  constructor(
    @Inject(PLATFORM_ID) private platformId: Object,
    private http: HttpClient
  ) {
    if (isPlatformBrowser(this.platformId)) {
      const storedCart = localStorage.getItem('cart');
      if (storedCart) {
        this.items = JSON.parse(storedCart);
        this.cartItemsSource.next([...this.items]);
      }
    }
  }

  addToCart(product: Product, sumQuatity: boolean = false): Observable<AddToCartStatus> {
    const quantitySelectedByUser = product.quantity;

    if (quantitySelectedByUser <= 0) {
      console.warn(`Tentativa de adicionar produto ${product.productId} com quantidade inválida: ${quantitySelectedByUser}`);
      return of(AddToCartStatus.INVALID_QUANTITY);
    }

    const existingItemIndex = this.items.findIndex(item => item.product.productId === product.productId);
    
    if(sumQuatity && existingItemIndex > -1){
      product.quantity += this.items[existingItemIndex].addToCart;
    }

    const requestBody = {
      name: product.productName,
      quantity: product.quantity,
      unitPrice: product.unitPrice
    };

    let headers = new HttpHeaders();
    const token = localStorage.getItem('authToken')

    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    } else {
      console.warn('Nenhum token de autenticação encontrado. A requisição para /api/Cart será feita sem autenticação.');
    }

    return this.http.post<ApiCartResponse>(this.apiUrl, requestBody, { headers }).pipe(
      map(response => {
        if (response.success && response.data) {
          const apiData = response.data;

          const productDataForCartItem: Product = {
            productId: product.productId,
            productName: product.productName,
            imageUrl: product.imageUrl,
            unitPrice: apiData.unitPrice,
            originalUnitPrice: apiData.originalUnitPrice,
            discountPercentage: apiData.discountPercentage,
            quantity: product.quantity
          };

          if (existingItemIndex > -1) {
            this.items[existingItemIndex].addToCart = apiData.quantity;
            this.items[existingItemIndex].product = productDataForCartItem;
          } else {
            this.items.push({ product: productDataForCartItem, addToCart: apiData.quantity });
          }

          this.cartItemsSource.next([...this.items]);
          if (isPlatformBrowser(this.platformId)) {
            this.saveCartToLocalStorage();
          }
          return AddToCartStatus.SUCCESS;
        } else {
          console.error('Erro ao adicionar ao carrinho (API):', response.message, response.errors);
          return AddToCartStatus.API_ERROR;
        }
      }),
      catchError(error => {
        console.error('Erro HTTP ao adicionar ao carrinho:', error);
        return of(AddToCartStatus.API_ERROR);
      })
    );
  }

  private saveCartToLocalStorage(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.setItem('cart', JSON.stringify(this.items));
    }
  }

  checkout(saleData: SaleRequest): Observable<SaleResponse> {
    let headers = new HttpHeaders();
    const token = localStorage.getItem('authToken');

    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    } else {
      console.warn('Nenhum token de autenticação encontrado. A requisição para /api/Sale será feita sem autenticação.');
    }
    headers = headers.set('Content-Type', 'application/json');

    return this.http.post<SaleResponse>(this.saleApiUrl, saleData, { headers }).pipe(
      map(response => {
        // Não vamos limpar o carrinho aqui diretamente,
        // o componente decidirá isso com base na resposta.
        return response;
      }),
      catchError(error => {
        console.error('Erro HTTP ao finalizar a compra:', error);
        const errorResponse: SaleResponse = {
          success: false,
          message: 'Erro ao conectar com o servidor para finalizar a compra.',
          errors: [error.message || 'Erro desconhecido']
        };
        return of(errorResponse);
      })
    );
  }

  public clearCartLocal(): void {
    this.items = [];
    this.cartItemsSource.next([...this.items]);
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('cart');
    }
    console.log('Carrinho local e localStorage limpos.');
  }

  getMyOrders(): Observable<OrderListResponse> {
    let headers = new HttpHeaders();
    const token = localStorage.getItem('authToken');

    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    } else {
      console.warn('Nenhum token de autenticação encontrado. A requisição para /api/Sale/Me será feita sem autenticação.');
      // Considerar retornar um erro ou um Observable vazio se o token for obrigatório
      // return of({ success: false, message: 'Token não encontrado', errors: [], data: [] });
    }

    return this.http.get<OrderListResponse>(this.myOrdersApiUrl, { headers }).pipe(
      catchError(error => {
        console.error('Erro HTTP ao buscar os pedidos:', error);
        // Retorna uma estrutura de erro consistente com OrderListResponse
        return of({ success: false, message: 'Erro ao buscar pedidos.', errors: [{error: error.name, detail: error.message}], data: [] });
      })
    );
  }


  getCartItems(): CartItem[] {
    return [...this.items];
  }
}