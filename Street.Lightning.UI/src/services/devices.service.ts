import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DeviceDto } from '../dto/device.dto';
import { ResponseDto } from '../dto/response.dto';
import { catchError, map, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DevicesService {
   private urlBase = "https://street-lights.azurewebsites.net/IlluminationProvider/";

   constructor(private http: HttpClient) { }

   getDevices() {
      const httpOptions = {
         headers: new HttpHeaders({
           'Content-Type': 'application/json',
           'Access-Control-Allow-Origin':'*',
           'Authorization':'authkey',
           'userid':'1'
         })
       };

      return this.http.get<ResponseDto<DeviceDto[]>>(this.urlBase + "getAllIlluminationProviders", httpOptions)
         .pipe(
            map((v, i) => v.data),
            catchError((err, obj) => of([] as DeviceDto[]))
         )
   }

   addDevice(device: Partial<DeviceDto>) {
      const httpOptions = {
         headers: new HttpHeaders({
           'Content-Type': 'application/json',
           'Access-Control-Allow-Origin':'*',
           'Authorization':'authkey',
           'userid':'1'
         })
       };

      return this.http.post<ResponseDto<object>>(this.urlBase + "addIlluminationProvider", device, httpOptions)
         .pipe(
            map((v, i) => true),
            catchError((err, obj) => of(false))
         )
   }
}
