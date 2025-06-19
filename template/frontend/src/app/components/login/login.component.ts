import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment'; 
import { ModalComponent } from '../modal/modal.component'; 

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule, ModalComponent], 
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';
  loginError: string | null = null;
  loginSuccess: string | null = null;
  isLoading: boolean = false;

  isModalOpen = false;
  modalTitle = '';
  modalMessage = '';

  private apiUrl = `${environment.apiUrlBase}/api/Auth`;

  constructor(private http: HttpClient, private router: Router) {
    console.log('LoginComponent constructor - this.router:', this.router);
  }

  ngOnInit(): void {
    if (localStorage.getItem('authToken')) {
      this.router.navigate(['/']); 
    }
  }

  onLogin(): void {
    this.isLoading = true;
    this.loginError = null;
    this.loginSuccess = null;

    if (!this.username || !this.password) {
      this.loginError = 'Usuário e senha são obrigatórios.';
      return;
    }

    this.http.post<{ data: {token: string} }>(this.apiUrl, { email: this.username, password: this.password })
      .subscribe({
        next: (response) => {
          this.isLoading = false;
          console.log('Login successful', response);
          if (response && response.data.token) {
            localStorage.setItem('authToken', response.data.token); 
            this.loginSuccess = 'Login realizado com sucesso!.';
            this.router.navigate(['/']); 
          } else {
            this.loginError = 'Resposta de login inválida: token não encontrado.';
          }

        },
        error: (error) => {
          this.isLoading = false;
          console.error('Login failed', error);
          this.loginError = `Falha no login: ${error.message || 'Erro desconhecido'}. Verifique se a API está acessível em ${this.apiUrl} e se suas credenciais estão corretas.`;
        }
      });
  }
  closeDialog(): void {
    this.isModalOpen = false;
    if (this.loginSuccess) { // Se o login foi bem-sucedido, redireciona após fechar o modal
        this.router.navigate(['/']);
    }
  }
}