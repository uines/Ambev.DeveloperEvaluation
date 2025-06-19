import { Component, Input, Output, EventEmitter } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './modal.component.html',
  styleUrls: ['./modal.component.css']
})
export class ModalComponent {
  @Input() isOpen: boolean = true;
  @Input() title: string = '';
  @Input() message: string = '';
  @Input() confirmButtonText: string = 'OK';

  @Output() closed = new EventEmitter<void>();

  closeModal(): void {
    this.isOpen = false;
    this.closed.emit();
  }
}
