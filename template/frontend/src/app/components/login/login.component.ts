import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule], // FormsModule para ngModel e CommonModule para *ngIf
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  username = '';
  password = '';
  loginError: string | null = null;
  loginSuccess: string | null = null;

  private apiUrl = 'https://localhost:7181/api/Auth';

  constructor(private http: HttpClient, private router: Router) {
    console.log('LoginComponent constructor - this.router:', this.router);
  }

  ngOnInit(): void {
    if (localStorage.getItem('authToken')) {
      this.router.navigate(['/']); 
    }
  }

  onLogin(): void {
    this.loginError = null;
    this.loginSuccess = null;

    if (!this.username || !this.password) {
      this.loginError = 'Usuário e senha são obrigatórios.';
      return;
    }

    this.http.post<{ data: {token: string} }>(this.apiUrl, { email: this.username, password: this.password })
      .subscribe({
        next: (response) => {
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
          console.error('Login failed', error);
          this.loginError = `Falha no login: ${error.message || 'Erro desconhecido'}. Verifique se a API está acessível em ${this.apiUrl} e se suas credenciais estão corretas.`;
        }
      });
  }
}