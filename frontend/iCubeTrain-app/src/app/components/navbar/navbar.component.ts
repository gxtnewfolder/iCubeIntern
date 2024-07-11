import { Component, inject } from '@angular/core';
import { Router, RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../services/auth.service';
import { MatMenuModule } from '@angular/material/menu';
import { CommonModule } from '@angular/common';
import { MatSnackBarModule, MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { SharedDataService } from '../../services/shared-data.service';
import { ChatGptPageComponent } from '../chat-gpt-page/chat-gpt-page.component'

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [
    RouterLink,
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatMenuModule,
    CommonModule,
    MatSnackBarModule,
    MatDialogModule,
  ],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  authService = inject(AuthService);
  matSnackBar = inject(MatSnackBar);
  router = inject(Router);

  isLoggedIn() {
    return this.authService.isLogedIn();
  }

  logout() {
    this.authService.logout();
    this.matSnackBar.open('You have been logged out', 'Close', {
      duration: 3000,
    });
    this.router.navigate(['/login']);
  }

  readonly dialog = inject(MatDialog);

  openDialog() {
    const dialogRef = this.dialog.open(ChatGptPageComponent, {
      width: '25%',
      height: '100vh',
      maxWidth: '80vw',
      position: { right: '0' },
      disableClose: false,
    });

    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
    });
  }
  constructor(private sharedData: SharedDataService ) {}

  toggleChat() {
    this.sharedData.toggleChat();
  }
}
