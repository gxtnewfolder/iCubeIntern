import { Component } from '@angular/core';
import { ChatGptService } from '../../services/chat-gpt.service';
import { MatButtonModule } from '@angular/material/button';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatFormFieldModule } from '@angular/material/form-field';
import { NgSelectModule } from '@ng-select/ng-select';
import { MatDialogModule } from '@angular/material/dialog';
import { MatIconModule } from '@angular/material/icon';
import {
  faArrowUpLong,
  faCalendar,
  faTags,
  faTimes,
  faChevronDown,
} from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-chat-gpt-page',
  standalone: true,
  imports: [
    MatButtonModule,
    FormsModule,
    CommonModule,
    RouterModule,
    MatFormFieldModule,
    NgSelectModule,
    MatDialogModule,
    MatIconModule,
    FontAwesomeModule,
  ],
  templateUrl: './chat-gpt-page.component.html',
  styleUrls: ['./chat-gpt-page.component.css'],
})
export class ChatGptPageComponent {
  faArrowUpLong = faArrowUpLong;
  faCalendar = faCalendar;
  faTags = faTags;
  faTimes = faTimes;
  faChevronDown = faChevronDown;

  selectedTags: string = 'B15-Status';
  startTime: string = '*-1m';
  endTime: string = '*';
  userMessage: string = '"Hello"';
  chatData: any[] = [];

  constructor(private chatGptService: ChatGptService) {}

  sendMessage(prompt: string | null = null, textArea?: { value: string }) {
    // Log the inputs to the console
    console.log('Selected Tags:', this.selectedTags);
    console.log('Start Time:', this.startTime);
    console.log('End Time:', this.endTime);
    console.log('User Message:', prompt);

    if (!prompt) {
      console.error('The prompt field is required.');
      return;
    }

    this.chatGptService
      .analyzeStatus(prompt, this.selectedTags, this.startTime, this.endTime)
      .subscribe(
        (response) => {
          console.log('Response:', response); // Check response here
          this.chatData.push({ id: 1, message: response });
          if (textArea) {
            textArea.value = '';
          }
        },
        (error) => {
          console.error('Error:', error);
        }
      );
  }

  autoGrowInput(textArea: HTMLElement) {
    textArea.style.height = 'auto';
    textArea.style.height = textArea.scrollHeight + 'px';
  }
}
