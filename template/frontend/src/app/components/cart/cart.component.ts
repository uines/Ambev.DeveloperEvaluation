import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { CartItem } from '../../models/cart-item.model';
import { CartService } from '../../services/cart.service';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Product } from '../../models/product.model';
import { SaleRequest, ProductSale, SaleResponse } from '../../models/sale.model';
import { AddToCartStatus } from '../../models/add-to-cart-status.model';

@Component({
  selector: 'app-cart',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
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
    return items.reduce((total, item) => total + (item.product.unitPrice * item.addToCart), 0);
  }

  updateCart(product: Product, newQuantityValue?: any): void {

    const inputElement = newQuantityValue.target as HTMLInputElement;
    const newQuantity = Number(inputElement.value);

    const validatedQuantity = Math.max(1, Number(newQuantity));

    if (product.quantity < validatedQuantity) {
      product.quantity += validatedQuantity - product.quantity;
    } else if (product.quantity > validatedQuantity) {
      product.quantity -= product.quantity - validatedQuantity;
    }

    product.unitPrice = product.originalUnitPrice;
    this.cartService.addToCart(product).subscribe({
      next: (status) => {
        switch (status) {
          case AddToCartStatus.SUCCESS:
            alert(`${product.productName} foi adicionado ao carrinho e processado pela API!`);
            break;
          case AddToCartStatus.QUANTITY_LIMIT_REACHED:
            alert(`Não é possível adicionar mais unidades de ${product.productName}. Limite de estoque no carrinho atingido.`);
            break;
          case AddToCartStatus.API_ERROR:
            alert(`Erro ao processar o item ${product.productName} com a API. Verifique o console para mais detalhes ou tente novamente.`);
            break;
          case AddToCartStatus.INVALID_QUANTITY:
            alert(`A quantidade selecionada para ${product.productName} é inválida.`);
            break;
        }
      },
      error: (err) => {
        console.error('Erro inesperado ao se inscrever no addToCart:', err);
        alert('Ocorreu um erro inesperado ao adicionar o produto. Tente novamente.');
      }
    });
  }

  checkout(items: CartItem[] | null): void {
    if (!items || items.length === 0) {
      alert('Seu carrinho está vazio!');
      return;
    }

    const productsForSale: ProductSale[] = items.map(item => ({
      name: item.product.productName,
      quantity: item.product.quantity,
      unitPrice: item.product.originalUnitPrice
    }));

    const saleRequest: SaleRequest = {
      branch: "DefaultBranch",
      products: productsForSale
    };

    this.cartService.checkout(saleRequest).subscribe({
      next: (response: SaleResponse) => {
        if (response.success) {
          alert(`Compra finalizada com sucesso! ${response.message || ''}`);
          this.cartService.clearCartLocal();
        } else {
          const errorMsg = response.errors && response.errors.length > 0 ? response.errors.join(', ') : response.message;
          alert(`Erro ao finalizar a compra: ${errorMsg || 'Erro desconhecido.'}`);
        }
      },
      error: (err) => {
        console.error('Erro inesperado ao finalizar a compra:', err);
        alert('Ocorreu um erro inesperado ao tentar finalizar a compra. Verifique sua conexão ou tente mais tarde.');
      }
    });
  }
}