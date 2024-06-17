import { Component } from '@angular/core';
import MaterialModule from '../material.module';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { DeviceDto } from '../../dto/device.dto';
import { DevicesService } from '../../services/devices.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-device',
  standalone: true,
  imports: [ MaterialModule, ReactiveFormsModule ],
  templateUrl: './add-device.component.html',
  styleUrl: './add-device.component.scss'
})
export class AddDeviceComponent {
   form = this.formBuilder.group<DeviceDto>({
      'id': 0,
      'name': '',
      'illuminationProvider': '',
      'power': 0
   })

   constructor(private formBuilder: FormBuilder, private devicesService: DevicesService, private router: Router) { }

   onSubmit(value: any) {
      this.devicesService
         .addDevice(value)
         .subscribe(v => { if (v) { this.router.navigateByUrl('/devices'); }})
   }
}
