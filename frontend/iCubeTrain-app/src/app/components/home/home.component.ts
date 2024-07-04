import { Component, inject } from '@angular/core';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { ChatGptPageComponent } from '../chat-gpt-page/chat-gpt-page.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterLink, MatDialogModule],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
  host: { class: 'w-full' },
})
export class HomeComponent {
  authService = inject(AuthService);
  isChatVisible = false;

  toggleChat() {
    this.isChatVisible = !this.isChatVisible;
  }

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog() {

    const dialogRef = this.dialog.open(ChatGptPageComponent, {
      width: '30%',
      height: '100vh',
      maxWidth: '80vw',
      position: { right: '-452px', bottom: '0' },
      panelClass: 'custom-dialog',
      disableClose: false,
    });
  
    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
    });
  }
}