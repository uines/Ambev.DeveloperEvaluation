import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { CartService } from '../../services/cart.service';
import { Order, OrderListResponse } from '../../models/order.model';

@Component({
  selector: 'app-order-list',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './order-list.component.html',
  styleUrls: ['./order-list.component.css']
})
export class OrderListComponent implements OnInit {
  orders$: Observable<Order[] | null> = of(null);
  isLoading: boolean = true;
  error: string | null = null;

  constructor(private cartService: CartService) {}

  ngOnInit(): void {
    this.orders$ = this.cartService.getMyOrders().pipe(
      map((response: OrderListResponse) => {
        this.isLoading = false;
        if (response.success) {
          return response.data;
        } else {
          this.error = response.message || 'Erro ao carregar pedidos.';
          if (response.errors && response.errors.length > 0) {
            this.error += ' Detalhes: ' + response.errors.map(e => e.detail || e.error).join(', ');
          }
          return []; // Retorna array vazio em caso de erro de API, mas sucesso = false
        }
      }),
      catchError(err => {
        this.isLoading = false;
        this.error = 'Ocorreu um erro inesperado ao buscar os pedidos. Tente novamente mais tarde.';
        console.error('Erro crítico ao buscar pedidos:', err);
        return of([]); // Retorna array vazio em caso de erro crítico na subscrição
      })
    );
  }

  toggleProducts(order: Order): void {
    // Simples toggle para expandir/recolher produtos, pode ser melhorado
    const productsDiv = document.getElementById(`products-${order.id}`);
    if (productsDiv) {
      productsDiv.style.display = productsDiv.style.display === 'none' ? 'block' : 'none';
    }
  }
}
