import { Component, OnInit } from '@angular/core';
import MaterialModule from '../material.module';
import { CommonModule } from '@angular/common';
import { FormArray, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { DevicesService } from '../../services/devices.service';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { CountryDto } from '../../dto/country.dto';
import { CountriesService } from '../../services/countries.service';
import { CityDeviceDto } from '../../dto/cityDevice.dto';
import { DeviceDto } from '../../dto/device.dto';
import { CitiesManagerService } from '../../services/cities-manager.service';
import CityDto from '../../dto/city.dto';
import { CityDevicesService } from '../../services/city-devices.service';

@Component({
  selector: 'app-add-city',
  standalone: true,
  imports: [ MaterialModule, CommonModule, ReactiveFormsModule ],
  templateUrl: './add-city.component.html',
  styleUrl: './add-city.component.scss'
})
export class AddCityComponent implements OnInit {
   illuminationsForm = this.formBuilder.array([] as CityDeviceDto[])
   form = this.formBuilder.group({
      'id': 0,
      'cityName': '',
      'latitude': 0,
      'longitude': 0,
      'countryId': 0,
      'illuminations': this.illuminationsForm
   })

   countries$!: Observable<CountryDto[]>
   devices$!: Observable<DeviceDto[]>

   constructor(private formBuilder: FormBuilder
      , private cityService: CitiesManagerService
      , private devicesService: DevicesService
      , private countriesService: CountriesService
      , private cityDevicesService: CityDevicesService
      , private router: Router) { }

   get illuminations() {
      return this.form.get('illuminations') as FormArray
   }

   ngOnInit() {
      this.countries$ = this.countriesService.getCountries()
      this.devices$ = this.devicesService.getDevices()

      this.addDevice()
   }

   itemValue(item: any) {
      return item.controls.value.value;
    }

   onSubmit(value: any) {
      const { illuminations, ...city} = value
      this.cityService.addCity(city as CityDto).subscribe(success => {
         console.log("City added...")
         if (success) {
            console.log("...with success")
            this.cityService.getCities().subscribe(cities => {
               console.log("Cities requested: ", cities)
               const foundCity = cities.find((v) => v.cityName == city.cityName)
               if (foundCity) {
                  console.log("City found")
                  for (const illumination of illuminations) {
                     illumination.cityId = foundCity.id
                     console.log("Saving illumination: ", illumination)
                     const illuminationCopy = illumination
                     this.cityDevicesService.addDeviceToCity(illuminationCopy).subscribe()
                  }
               }
            })
         }
      })
   }

   addDevice() {
      const illuminations = this.form.get('illuminations') as FormArray
      illuminations.push(this.formBuilder.group({ illuminationId: 0, quantityOfBulbs: 0 } as CityDeviceDto))
   }
}
