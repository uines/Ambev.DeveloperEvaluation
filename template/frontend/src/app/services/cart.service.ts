import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { isPlatformBrowser } from '@angular/common';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Product } from '../models/product.model';
import { CartItem } from '../models/cart-item.model';
import { BehaviorSubject, Observable, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { AddToCartStatus } from '../models/add-to-cart-status.model';
import { ApiCartResponse, ApiCartItem } from '../models/api-cart.model';

@Injectable({
  providedIn: 'root'
})
export class CartService {
  private items: CartItem[] = [];
  private cartItemsSource = new BehaviorSubject<CartItem[]>([]);
  cartItems$ = this.cartItemsSource.asObservable();
  private apiUrl = 'https://localhost:7181/api/Cart';

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

  addToCart(product: Product): Observable<AddToCartStatus> {
    const quantitySelectedByUser = product.quantity;

    if (quantitySelectedByUser <= 0) {
      console.warn(`Tentativa de adicionar produto ${product.productId} com quantidade inválida: ${quantitySelectedByUser}`);
      return of(AddToCartStatus.INVALID_QUANTITY);
    }

    const existingItemIndex = this.items.findIndex(item => item.product.productId === product.productId);
    let quantityForApi: number;

    quantityForApi = quantitySelectedByUser;
    
    const requestBody = {
      name: product.productName,
      quantity: quantitySelectedByUser,
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
            this.items[existingItemIndex].quantityInCart = apiData.quantity;
            this.items[existingItemIndex].product = productDataForCartItem;
          } else {
            this.items.push({ product: productDataForCartItem, quantityInCart: apiData.quantity });
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

  getCartItems(): CartItem[] {
    return [...this.items];
  }
}