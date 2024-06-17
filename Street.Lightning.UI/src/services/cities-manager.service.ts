import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import CityDto from '../dto/city.dto';
import { ResponseDto } from '../dto/response.dto';
import { catchError, map, of } from 'rxjs';
import PowerUsageDto from '../dto/power-usage.dto';
import { PowerUsageDayDto } from '../dto/power-usage-day.dto';

@Injectable({
  providedIn: 'root',
})
export class CitiesManagerService {
   private urlBase = "https://street-lights.azurewebsites.net";

   constructor(private http: HttpClient) { }
   
   getCities() {

      const httpOptions = {
         headers: new HttpHeaders({
           'Content-Type': 'application/json',
           'Access-Control-Allow-Origin':'*',
           'Authorization':'authkey',
           'userid':'1'
         })
       };

      return this.http.get<ResponseDto<CityDto[]>>(this.urlBase + "/City/cities", httpOptions)
         .pipe(
            map((v, i) => v.data),
            catchError((err, obj) => of([] as CityDto[]))
         )
   }

   getCityYearlyUsage(cityId: number) {
      const httpOptions = {
         headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin':'*',
            'Authorization':'authkey',
            'userid':'1'
          })
      }

      return this.http.get<ResponseDto<PowerUsageDto>>(this.urlBase + "/City/getCityYearlyPowerUsage/" + cityId, httpOptions)
         .pipe(
            map((v, i) => v.data),
            catchError((err, obj) => of({} as PowerUsageDto))
         )
   }

   getCityDailyUsage(cityId: number) {
      const httpOptions = {
         headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin':'*',
            'Authorization':'authkey',
            'userid':'1'
          })
      }

      return this.http.get<ResponseDto<PowerUsageDayDto[]>>(this.urlBase + "/City/getYearlyCityPowerUsageDays/" + cityId, httpOptions)
         .pipe(
            map((v, i) => v.data),
            catchError((err, obj) => of([] as PowerUsageDayDto[]))
         )
   }

   getCityById(cityId: number) {
      const httpOptions = {
         headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin':'*',
            'Authorization':'authkey',
            'userid':'1'
          })
      }

      return this.http.get<ResponseDto<CityDto>>(this.urlBase + "/City/getCityById/" + cityId, httpOptions)
         .pipe(
            map((v, i) => v.data),
            catchError((err, obj) => of({} as CityDto))
         )
   }

   addCity(city: CityDto) {
      const httpOptions = {
         headers: new HttpHeaders({
            'Content-Type': 'application/json',
            'Access-Control-Allow-Origin':'*',
            'Authorization':'authkey',
            'userid':'1'
          })
      }

      return this.http.post<ResponseDto<object>>(this.urlBase + "/City/addCity/", city, httpOptions)
         .pipe(
            map((v, i) => true),
            catchError((err, obj) => of(false))
         )
   }
}
