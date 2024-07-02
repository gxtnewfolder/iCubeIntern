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
})
export class HomeComponent {
  authService = inject(AuthService);

  constructor(private dialog: MatDialog) { }

  ngOnInit(): void {
  }

  openDialog() {
    
    const viewportWidth = window.innerWidth;
    const dialogWidth = Math.min(viewportWidth * 0.4, 500);

    const dialogRef = this.dialog.open(ChatGptPageComponent, {
      height: '100%',
      width: `${dialogWidth}px`,
      maxWidth: '80vw',
      // maxHeight: '800px',
      disableClose: false,
      position: { right: '-250px', bottom: '0px' },
    });
  
    dialogRef.afterClosed().subscribe((result) => {
      console.log(`Dialog result: ${result}`);
    });
  }
}