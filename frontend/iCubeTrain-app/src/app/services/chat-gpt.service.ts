import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatGptService {
  private apiUrl = 'http://localhost:5032/api/multitag/analyze';
  

  constructor(private http: HttpClient) { }

  analyzeStatus(prompt: string, tags: string, startTime: string, endTime: string, model: string): Observable<any> {
    if (!prompt) {
      throw new Error('The prompt field is required.');
    }

    const url = `${this.apiUrl}?tagName=${tags}&startTime=${startTime}&endTime=${endTime}&prompt=${prompt}`;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });

    return this.http.post<any>(url, { prompt }, { headers, responseType: 'json' });
  }
}