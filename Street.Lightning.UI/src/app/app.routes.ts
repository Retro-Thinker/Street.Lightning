import { Routes } from '@angular/router';
import { AddCityComponent } from './add-city/add-city.component';
import { MainComponent } from './main/main.component';
import { CreditsComponent } from './credits/credits.component';
import { CitiesListComponent } from './cities-list/cities-list.component';
import { CityComponent } from './city/city.component';
import { DevicesComponent } from './devices/devices.component';
import { AddDeviceComponent } from './add-device/add-device.component';

export const routes: Routes = [
   { path: 'add-city', component: AddCityComponent },
   { path: 'credits', component: CreditsComponent },
   { path: 'cities', component: CitiesListComponent },
   { path: 'cities/:id', component: CityComponent },
   { path: 'devices', component: DevicesComponent },
   { path: 'add-device', component: AddDeviceComponent },
   { path: '**', component: MainComponent }
];
