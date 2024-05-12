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
      return this.http.get<ResponseDto<CityDto[]>>(this.urlBase + "/City/cities")
         .pipe(
            map((v, i) => v.data),
            catchError((err, obj) => of([] as CityDto[]))
         )
   }
}
