import { Component } from '@angular/core';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { ChatGptPageComponent } from '../chat-gpt-page/chat-gpt-page.component';

@Component({
  selector: 'app-main',
  standalone: true,
  imports: [SidebarComponent, ChatGptPageComponent],
  templateUrl: './main.component.html',
  styleUrl: './main.component.css'
})
export class MainComponent {

}
