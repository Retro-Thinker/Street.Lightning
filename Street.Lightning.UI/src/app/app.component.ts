import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatToolbarModule } from '@angular/material/toolbar';
import { HttpClientModule } from '@angular/common/http';
import { CitiesManagerService } from '../services/cities-manager.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, MatGridListModule, MatToolbarModule, HttpClientModule],
  providers: [CitiesManagerService],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Street.Lightning.UI';
}
