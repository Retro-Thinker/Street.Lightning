import { Routes } from '@angular/router';
import { AddCityComponent } from './add-city/add-city.component';
import { MainComponent } from './main/main.component';
import { CreditsComponent } from './credits/credits.component';
import { CitiesListComponent } from './cities-list/cities-list.component';

export const routes: Routes = [
   { path: 'add-city', component: AddCityComponent },
   { path: 'credits', component: CreditsComponent },
   { path: 'cities', component: CitiesListComponent },
   { path: '**', component: MainComponent }
];
