import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms'; 
import { Product } from '../../models/product.model';
import { CartItem } from '../../models/cart-item.model';
import { CartService } from '../../services/cart.service'; 
import { AddToCartStatus } from '../../models/add-to-cart-status.model';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [CommonModule, FormsModule], 
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent implements OnInit {
  products: (Product)[] = [];
  cartItens: CartItem[] = [];
  isLoading: boolean = false;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.products = [
      {
        productId: 'SKOL001',
        productName: 'Cerveja Skol Pilsen 350ml Lata',
        unitPrice: 3.49,
        originalUnitPrice:  3.49,
        imageUrl: 'https://www.ambev.com.br/sites/g/files/wnfebl12286/files/styles/webp/public/paragraphs/product_size/2024-03/3.%20Skol%20Pilsen%20Lata%20Std%20350ml.png.webp?itok=N0EAlrtR',
        quantity: 1
      },
      {
        productId: 'BRA002',
        productName: 'Cerveja Brahma Duplo Malte 350ml Lata',
        unitPrice: 3.99,
        originalUnitPrice:  3.99,
        imageUrl: 'https://www.ambev.com.br/sites/g/files/wnfebl12286/files/styles/webp/public/paragraphs/product_size/2024-03/1.%20Brahma%20Duplo%20Malte%20Tostada%20Lata%20Sleek%20350ml.png.webp?itok=ugScOrC7',
        quantity: 1
      },
      {
        productId: 'ANT003',
        productName: 'Cerveja Antarctica Original 600ml Garrafa',
        unitPrice: 7.20,
        originalUnitPrice: 7.20,
        imageUrl: 'https://www.ambev.com.br/sites/g/files/wnfebl12286/files/styles/webp/public/paragraphs/product_size/2024-03/5.%20Original%20Garrafa%20Vidro%20600ml.png.webp?itok=RRlVqwKI',
        quantity: 1
      },
      {
        productId: 'GUA004',
        productName: 'Refrigerante Guaraná Antarctica 2L PET',
        unitPrice: 8.50,
        originalUnitPrice: 8.50,
        imageUrl: 'https://www.ambev.com.br/sites/g/files/wnfebl12286/files/styles/webp/public/paragraphs/product_size/2024-04/9.%20Guaran%C3%A1%20Antarctica%20Garrafa%20Pet%202L%20%281%29.png.webp?itok=yCuNrdmJ',
        quantity: 1
      }
    ];

    this.cartItens = this.products.map(product => ({ product, addToCart: 1 }));
  }

  addToCart(product: Product): void {
    this.isLoading = true;
    if (product.quantity <= 0) {
      alert('Por favor, insira uma quantidade válida.');
      return;
    }
    
    this.cartService.addToCart(product, true).subscribe({
      next: (status) => {
        this.isLoading = false;
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
      },
      complete: () => {
        this.isLoading = false;
      }
    });
  }
}