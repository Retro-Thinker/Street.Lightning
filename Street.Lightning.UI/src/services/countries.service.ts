import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ResponseDto } from '../dto/response.dto';
import { CountryDto } from '../dto/country.dto';
import { catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CountriesService {
   private urlBase = "https://street-lights.azurewebsites.net/Country/";

   constructor(private http: HttpClient) { }
   
   getCountries() {
      const httpOptions = {
         headers: new HttpHeaders({
           'Content-Type': 'application/json',
           'Access-Control-Allow-Origin':'*',
           'Authorization':'authkey',
           'userid':'1'
         })
       };

      return this.http.get<ResponseDto<CountryDto[]>>(this.urlBase + "countries", httpOptions)
         .pipe(
            map((v, i) => v.data),
            catchError((err, obj) => of([] as CountryDto[]))
         )
   }
}
