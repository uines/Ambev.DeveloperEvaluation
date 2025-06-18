import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { CartItem } from '../../models/cart-item.model';
import { CartService } from '../../services/cart.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms'; // Importar FormsModule
import { Product } from '../../models/product.model';

@Component({ 
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule], // Adicionar FormsModule aqui
  templateUrl: './cart.component.html',
  styleUrls: ['./cart.component.css']
})
export class CartComponent implements OnInit {
  cartItems$: Observable<CartItem[]>;

  constructor(private cartService: CartService) {
    this.cartItems$ = this.cartService.cartItems$;
  }

  ngOnInit(): void {
  }

  getTotalPrice(items: CartItem[] | null): number {
    if (!items) {
      return 0;
    }
    return items.reduce((total, item) => total + (item.product.unitPrice * item.quantityInCart), 0);
  }

  updateCart(product: Product, newQuantityValue?: any): void {
     const validatedQuantity = Math.max(1, Number(newQuantityValue));
     product.quantity = validatedQuantity;

    this.cartService.addToCart(product).subscribe({
          error: (err) => {
            console.error('Erro inesperado ao se inscrever no addToCart:', err);
            alert('Ocorreu um erro inesperado ao adicionar o produto. Tente novamente.');
          }
        });
  }

}