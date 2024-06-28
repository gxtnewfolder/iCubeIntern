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
import { MatChipsModule } from '@angular/material/chips';

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
    MatChipsModule,
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

  selectedTags: string[] = ['TC-MAP-MULTI-240-A', 'TC-MAP-MULTI-240-B']; // Array for multiple selections
  tagsOptions: { label: string; value: string }[] = [
    { label: 'TC-MAP-MULTI-240-A', value: 'TC-MAP-MULTI-240-A' },
    { label: 'TC-MAP-MULTI-240-B', value: 'TC-MAP-MULTI-240-B' },
    { label: 'TC-MAP-MULTI-240-C', value: 'TC-MAP-MULTI-240-C' },
    { label: 'TC-MAP-MULTI-241-D', value: 'TC-MAP-MULTI-241-D' },
  ];

  startTime: string = '*-1mo';
  endTime: string = '*';
  userMessage: string =
    'Analyze the following data of each tagId and provide a summary and tell me how much data you analyze.';
  chatData: { id: number; message: string }[] = [];
  isLoading: boolean = false;
  tokenCountInput: number = 0;
  tokenCountAnalysisSummary: number = 0;
  showModelDropdown: boolean = false;
  selectedModel: string = 'gpt-3.5-turbo';

  constructor(private chatGptService: ChatGptService) {}

  sendMessage(message: string | null = null, textArea?: HTMLTextAreaElement) {
    const tagsString = this.selectedTags.join(',');
    console.log('User Message:', message);

    if (!message) {
      console.error('The message field is required.');
      return;
    }

    this.chatData.push({ id: 0, message });
    this.isLoading = true;

    this.chatGptService
      .analyzeStatus(
        message,
        tagsString,
        this.startTime,
        this.endTime,
        this.selectedModel
      )
      .subscribe(
        (response) => {
          console.log('Response:', response.analysisSummary);
          console.log('Token Count Input:', response.tokenCountInput);
          console.log(
            'Token Count Analysis Summary:',
            response.tokenCountAnalysisSummary
          )
          // Check if response and analysisSummary exist
          if (response && response.analysisSummary) {
            // Display the analysis summary with line breaks
            const formattedAnalysisSummary = response.analysisSummary.replace(
              /\n/g,
              '<br>'
            );
            this.chatData.push({ id: 1, message: formattedAnalysisSummary });
          } else {
            console.error('Invalid response format:', response);
            // Handle invalid response format gracefully
          }

          // Update token counts if they exist
          this.tokenCountInput =
            response && response.tokenCountInput ? response.tokenCountInput : 0;
          this.tokenCountAnalysisSummary =
            response && response.tokenCountAnalysisSummary
              ? response.tokenCountAnalysisSummary
              : 0;
          this.isLoading = false;

          this.userMessage = '';
          if (textArea) {
            textArea.value = '';
            this.autoGrowInput(textArea);
          }
        },
        (error) => {
          console.error('Error:', error);
          this.isLoading = false;
        }
      );
  }

  autoGrowInput(textArea: HTMLElement) {
    textArea.style.height = 'auto';
    textArea.style.height = textArea.scrollHeight + 'px';
    // Add Max Height
    textArea.style.maxHeight = '84px';
  }

  updateTokenCount(message: string) {
    this.tokenCountInput = this.countTokens(message);
  }

  countTokens(message: string): number {
    return message.split(/\s+/).filter((word) => word.length > 0).length;
  }

  toggleModelDropdown() {
    this.showModelDropdown = !this.showModelDropdown;
  }

  selectModel(model: string) {
    this.selectedModel = model;
    this.showModelDropdown = false;
    console.log('Selected Model:', this.selectedModel);
  }
}
