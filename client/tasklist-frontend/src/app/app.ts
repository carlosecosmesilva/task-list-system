import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { environment } from '../environments/environment';

// Material Imports
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatChipsModule } from '@angular/material/chips';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    CommonModule,
    MatToolbarModule,
    MatButtonModule,
    MatIconModule,
    MatChipsModule
  ],
  templateUrl: './app.html',
})
export class App {
  title = environment.appName;
  version = environment.version;
  isProduction = environment.production;

  getApiUrl(): string {
    return environment.apiUrl;
  }
}
