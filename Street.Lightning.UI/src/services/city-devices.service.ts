import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { CityDeviceDto } from '../dto/cityDevice.dto';
import { ResponseDto } from '../dto/response.dto';
import { catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CityDevicesService {
   private urlBase = "https://street-lights.azurewebsites.net/CityIllumination/";

   constructor(private http: HttpClient) { }

   addDeviceToCity(city: CityDeviceDto) {
      const httpOptions = {
         headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin':'*',
            'Authorization':'authkey',
            'userid':'1'
          })
      }

      return this.http.post<ResponseDto<object>>(this.urlBase + "addCityIllumination", city, httpOptions)
         .pipe(
            map((v, i) => true),
            catchError((err, obj) => of(false))
         )
   }
}
