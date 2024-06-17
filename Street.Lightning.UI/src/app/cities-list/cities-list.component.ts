import { Component, OnInit } from '@angular/core';
import { CitiesManagerService } from '../../services/cities-manager.service';
import { Observable } from 'rxjs';
import CityDto from '../../dto/city.dto';
import { MatTableDataSource } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router'
import MaterialModule from '../material.module';

@Component({
   selector: 'app-cities-list',
   standalone: true,
   imports: [ MaterialModule, CommonModule ],
   templateUrl: './cities-list.component.html',
   styleUrl: './cities-list.component.scss'
})
export class CitiesListComponent implements OnInit {
   cities$!: Observable<CityDto[]>
   cityDataSource = new MatTableDataSource<CityDto>();
   displayedColumns: string[] = ['cityName', 'latitude', 'longitude', 'countryName'];
   constructor(private citiesManagerService: CitiesManagerService, private router: Router) { }

   ngOnInit(): void {
      this.cities$ = this.citiesManagerService.getCities();

      this.cities$.subscribe(d =>
      {
         this.cityDataSource.data = d
      })
   }

   onCitySelected(row: CityDto): void {
      this.router.navigate(["cities", row.id])
   }
}
