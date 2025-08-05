import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { CommonModule } from '@angular/common';
import { environment } from '../environments/environment';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, CommonModule],
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
