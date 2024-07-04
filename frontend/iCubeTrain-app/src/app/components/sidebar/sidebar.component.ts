import { Component, HostListener, ElementRef, Renderer2 } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  private resizing: boolean = false;

  constructor(private el: ElementRef, private renderer: Renderer2) {}

  startResizing(event: MouseEvent): void {
    this.resizing = true;
    event.preventDefault();
  }

  @HostListener('window:mousemove', ['$event'])
  onMouseMove(event: MouseEvent): void {
    if (this.resizing) {
      const newWidth = event.clientX;
      this.renderer.setStyle(this.el.nativeElement.querySelector('.sidebar'), 'width', `${newWidth}px`);
    }
  }

  @HostListener('window:mouseup')
  onMouseUp(): void {
    this.resizing = false;
  }
}
