import { CommonModule } from '@angular/common';
import { Component, OnInit, ViewChild } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { NavbarComponent } from './components/navbar/navbar.component';
import { ChatGptPageComponent } from './components/chat-gpt-page/chat-gpt-page.component';
import { MainComponent } from './components/main/main.component';
import { SidebarComponent } from './components/sidebar/sidebar.component';
import { HomeComponent } from './components/home/home.component';
import { SharedDataService } from './services/shared-data.service';
// PrimeNG modules
import { DropdownModule } from 'primeng/dropdown';
import { ButtonModule } from 'primeng/button';
import { PrimeNGConfig } from 'primeng/api';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    MatToolbarModule,
    CommonModule,
    NavbarComponent,
    DropdownModule,
    ButtonModule,
    ChatGptPageComponent,
    FontAwesomeModule,
    MainComponent,
    SidebarComponent,
    HomeComponent,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css',
})
export class AppComponent implements OnInit {
  isChatVisible: boolean = false;

  constructor(private primengConfig: PrimeNGConfig, private sharedData: SharedDataService) {}

  ngOnInit() {
    this.primengConfig.ripple = true;
    this.sharedData.chatVisible$.subscribe(visible => {
      this.isChatVisible = visible;
    });
  }
  title = 'client';
}
