import { Component, ElementRef, HostListener, Renderer2, ViewChild } from '@angular/core';
import { ChatGptService } from '../../services/chat-gpt.service';
import { SharedDataService } from '../../services/shared-data.service';
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
import { trigger, state, style, transition, animate } from '@angular/animations';

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
  animations: [
    trigger('dialogAnimation', [
      state('void', style({
        opacity: 0,
        transform: 'translateX(100%)'
      })),
    ])
  ]
})
export class ChatGptPageComponent {
  @ViewChild('dialog') dialogElement: ElementRef | undefined;
  faArrowUpLong = faArrowUpLong;
  faCalendar = faCalendar;
  faTags = faTags;
  faTimes = faTimes;
  faChevronDown = faChevronDown;

  isResizing: boolean = false;
  lastDownX: number | undefined;
  dialogWidth: number | undefined;

  tagsOptions: { label: string; value: string }[] = [
    { label: 'TC-MAP-MULTI-240-A', value: 'TC-MAP-MULTI-240-A' },
    { label: 'TC-MAP-MULTI-240-B', value: 'TC-MAP-MULTI-240-B' },
    { label: 'TC-MAP-MULTI-240-C', value: 'TC-MAP-MULTI-240-C' },
    { label: 'TC-MAP-MULTI-241-D', value: 'TC-MAP-MULTI-241-D' },
  ];

  // startTime: string = '*-1mo';
  // endTime: string = '*';
  // userMessage: string =
  //   'Analyze the following data of each tagname and provide a summary and tell me how much data you analyze.';
  // chatData: { id: number; message: string }[] = [];
  // isLoading: boolean = false;
  // tokenCountInput: number = 0;
  // tokenCountAnalysisSummary: number = 0;
  // showModelDropdown: boolean = false;
  // selectedModel: string = 'gpt-3.5-turbo';

  constructor(
    private chatGptService: ChatGptService,
    public sharedData: SharedDataService,
    private renderer: Renderer2
  ) {}

  // ngOnInit() {
  //   this.dialogWidth = 500;
  // }

  sendMessage(message: string | null = null, textArea?: HTMLTextAreaElement) {
    const tagsString = this.sharedData.selectedTags.join(',');
    console.log('Selected Tags:', tagsString);
    console.log('User Message:', message);

    if (!message) {
      console.error('The message field is required.');
      return;
    }

    this.sharedData.chatData.push({ id: 0, message });
    this.sharedData.isLoading = true;

    this.chatGptService
      .analyzeStatus(
        message,
        tagsString,
        this.sharedData.startTime,
        this.sharedData.endTime,
        this.sharedData.selectedModel
      )
      .subscribe(
        (response) => {
          console.log('Response:', response.analysisSummary);
          console.log('Token Count Input:', response.tokenCountInput);
          console.log(
            'Token Count Analysis Summary:',
            response.tokenCountAnalysisSummary
          );
          // Check if response and analysisSummary exist
          if (response && response.analysisSummary) {
            // Display the analysis summary with line breaks
            const formattedAnalysisSummary = response.analysisSummary.replace(
              /\n/g,
              '<br>'
            );
            this.sharedData.chatData.push({ id: 1, message: formattedAnalysisSummary });
          } else {
            console.error('Invalid response format:', response);
            // Handle invalid response format gracefully
          }

          // Update token counts if they exist
          this.sharedData.tokenCountInput =
            response && response.tokenCountInput ? response.tokenCountInput : 0;
          this.sharedData.tokenCountAnalysisSummary =
            response && response.tokenCountAnalysisSummary
              ? response.tokenCountAnalysisSummary
              : 0;
          this.sharedData.isLoading = false;

          this.sharedData.userMessage = '';
          if (textArea) {
            textArea.value = '';
            this.autoGrowInput(textArea);
          }
        },
        (error) => {
          console.error('Error:', error);
          this.sharedData.isLoading = false;
        }
      );
  }

  autoGrowInput(textArea: HTMLElement) {
    textArea.style.height = 'auto';
    textArea.style.height = textArea.scrollHeight + 'px';
    // Add Max Height
    textArea.style.maxHeight = '120px';
  }

  updateTokenCount(message: string) {
    this.sharedData.tokenCountInput = this.countTokens(message);
  }

  countTokens(message: string): number {
    return message.split(/\s+/).filter((word) => word.length > 0).length;
  }

  toggleModelDropdown() {
    this.sharedData.showModelDropdown = !this.sharedData.showModelDropdown;
  }

  selectModel(model: string) {
    this.sharedData.selectedModel = model;
    this.sharedData.showModelDropdown = false;
    console.log('Selected Model:', this.sharedData.selectedModel);
  }

  getSelectedTagsCount() {
    return this.sharedData.selectedTags.length;
  }

  clearChat() {
    this.sharedData.chatData = [];
    this.sharedData.tokenCountInput = 0;
    this.sharedData.tokenCountAnalysisSummary = 0;
  }

  onResizeHandleMouseDown(event: MouseEvent) {
    this.isResizing = true;
    this.lastDownX = event.clientX;
    this.dialogWidth = this.dialogElement?.nativeElement?.offsetWidth ?? 0;
  }

  @HostListener('window:mousemove', ['$event'])
  onMouseMove(event: MouseEvent) {
    if (!this.isResizing) return;
    const offsetRight = document.body.offsetWidth - event.clientX;
    this.dialogWidth = Math.max(offsetRight, 200); // Set a minimum width
    this.renderer.setStyle(this.dialogElement?.nativeElement, 'width', `${this.dialogWidth}px`);
  }

  @HostListener('window:mouseup')
  onMouseUp() {
    this.isResizing = false;
  }
}
