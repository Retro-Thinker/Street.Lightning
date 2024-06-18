import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { MatTableDataSource } from '@angular/material/table';
import { DevicesService } from '../../services/devices.service';
import { DeviceDto } from '../../dto/device.dto';
import MaterialModule from '../material.module';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-devices',
  standalone: true,
  imports: [MaterialModule, CommonModule],
  templateUrl: './devices.component.html',
  styleUrl: './devices.component.scss'
})
export class DevicesComponent implements OnInit {
   devices$!: Observable<DeviceDto[]>
   devicesDataSource = new MatTableDataSource<DeviceDto>();
   displayedColumns: string[] = ['name', 'illuminationProvider', 'power'];

   constructor(private devicesService: DevicesService) { }

   ngOnInit(): void {
      this.devices$ = this.devicesService.getDevices();

      this.devices$.subscribe(d =>
      {
         this.devicesDataSource.data = d
      })
   }
}
