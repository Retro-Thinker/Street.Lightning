import { Component } from '@angular/core';
import { CitiesManagerService } from '../services/cities-manager.service';
import { DevicesService } from '../services/devices.service';
import MaterialModule from './material.module';
import { CountriesService } from '../services/countries.service';
import { CityDevicesService } from '../services/city-devices.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [ MaterialModule ],
  providers: [ CitiesManagerService, DevicesService, CountriesService, CityDevicesService ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Street.Lightning.UI';
}
