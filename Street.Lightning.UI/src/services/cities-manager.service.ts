import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import CityDto from '../dto/city.dto';
import { ResponseDto } from '../dto/response.dto';
import { catchError, map, of } from 'rxjs';

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
}
