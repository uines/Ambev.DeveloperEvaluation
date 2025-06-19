// import { Component } from '@angular/core';
// import { RouterOutlet, RouterLink, RouterLinkActive } from '@angular/router';
import { Component, Inject, PLATFORM_ID, ChangeDetectorRef, OnInit, OnDestroy } from '@angular/core';
import { Router, RouterLink, RouterOutlet, RouterModule, RouterLinkActive } from '@angular/router'; 
import { CommonModule, isPlatformBrowser } from '@angular/common'; 
import { Subscription, fromEvent } from 'rxjs';

@Component({
  selector: 'app-root', 
  standalone: true,
  imports: [
    RouterOutlet,    
    RouterLink,      
    RouterLinkActive,
    RouterModule,
    CommonModule
  ],
  templateUrl: './app.html', 
  styleUrls: ['./app.css']   
})
export class App {
  private storageSub!: Subscription;

  constructor(
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object,
    private cdr: ChangeDetectorRef
  ) {}

  isLoggedIn(): boolean {
    if (isPlatformBrowser(this.platformId)) {
      return !!localStorage.getItem('authToken');
    }
    return false;
  }

  logout(): void {
    if (isPlatformBrowser(this.platformId)) {
      localStorage.removeItem('authToken');
      this.cdr.detectChanges();
      this.router.navigate(['/login']);
    }
  }
}