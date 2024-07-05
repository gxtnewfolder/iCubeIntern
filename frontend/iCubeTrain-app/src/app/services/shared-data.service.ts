import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class SharedDataService {
  selectedTags: { label: string; value: string }[] = [];
  chatData: { id: number; message: string }[] = [];
  startTime: string = '*-1mo';
  endTime: string = '*';
  userMessage: string = 'Analyze the following data of each tagname and provide a summary and tell me how much data you analyze.';
  isLoading: boolean = false;
  tokenCountInput: number = 0;
  tokenCountAnalysisSummary: number = 0;
  showModelDropdown: boolean = false;
  selectedModel: string = 'gpt-3.5-turbo';

  private chatVisible = new BehaviorSubject<boolean>(false);
  chatVisible$ = this.chatVisible.asObservable();

  toggleChat() {
    this.chatVisible.next(!this.chatVisible.value);
    // console.log('chatVisible', this.chatVisible.value);
  }
}