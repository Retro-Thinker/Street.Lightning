import { Component, OnDestroy, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CitiesManagerService } from '../../services/cities-manager.service';
import { Subscription, Observable, map, of } from 'rxjs';
import { CommonModule } from '@angular/common';
import CityDto from '../../dto/city.dto';
import { BaseChartDirective } from 'ng2-charts';
import { ChartData, ChartOptions } from 'chart.js';
import { PowerUsageDayDto } from '../../dto/power-usage-day.dto';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-city',
  standalone: true,
  imports: [ CommonModule, BaseChartDirective, MatButtonModule ],
  templateUrl: './city.component.html',
  styleUrl: './city.component.scss'
})
export class CityComponent implements OnInit, OnDestroy {
   id: number | undefined;
   sub: Subscription | undefined;

   isGrouped: boolean = true

   data!: PowerUsageDayDto[]
   data$!: Observable<PowerUsageDayDto[]>
   city$!: Observable<CityDto>

   yearlyPowerUsage!: number

   chartData: any
   chartGroupedLabels = [
      'January',
      'February',
      'March',
      'April',
      'May',
      'June',
      'July',
      'August',
      'September',
      'October',
      'November',
      'December'
   ]

   constructor(private cityService: CitiesManagerService, private route: ActivatedRoute) {}
   
   ngOnInit(): void {
      this.sub = this.route.params.subscribe((params => {
         this.id = +params['id']
         
         this.city$ = this.cityService.getCityById(this.id)
         this.data$ = this.cityService.getCityDailyUsage(this.id)

         this.data$.subscribe(d => {
            this.data = d
            this.yearlyPowerUsage = d.reduce((partialSum, a) => partialSum + a.powerUsage, 0);
         })
      }))
   }
   
   getPowerData() {
      if (this.data !== undefined) {
         if (this.isGrouped) {
            return [{
               label: "Power usage",
               data: this.data.reduce((sum, v) => {
                  const month = this.chartGroupedLabels[new Date(v.trackingDate).getMonth()]
                  if (sum[month] !== undefined) {
                     sum[month] += v.powerUsage;
                  }
                  else {
                     sum[month] = v.powerUsage;
                  }
                  return sum;
               }, {} as any)
            }]
         }
         return [{
            label: "Power usage",
            data: this.data.reduce((r, v, i) => {
               r[i.toString()] = v.powerUsage
               return r
            }, {} as any)
         }]
      }
      return [{ label: "Power usage", data: []}]
   }

   getLengthData() {
       if (this.data !== undefined) {
          if (this.isGrouped) {
            const transformedData = this.data.reduce((sum, v) => {
               const month = this.chartGroupedLabels[new Date(v.trackingDate).getMonth()]
               const days = new Date(new Date(v.trackingDate).getFullYear(), new Date(v.trackingDate).getMonth() + 1, 0).getDate()
               if (sum[month] !== undefined) {
                  sum[month] += v.streetLightsOnDuration;
               }
               else {
                  sum[month] = v.streetLightsOnDuration;
               }
               if (new Date(v.trackingDate).getDate() === days || (new Date(v.trackingDate).getMonth() == 11 && new Date(v.trackingDate).getDate() == 21)) {
                  sum[month] = Number.parseFloat(sum[month])/days
               }
               return sum;
            }, {} as any)
            return [{
               label: "Average light time",
               data: transformedData
            }]
         }
         return [{
            label: "Average light time",
            data: this.data.reduce((r, v, i) => {
               r[i.toString()] = v.streetLightsOnDuration
               return r
            }, {} as any)
         }]
      }
      return [{ label: "Average light time", data: []}]
   }

   getChartOptions() {
      if (this.isGrouped) {
         return {
            responsive: true, scales: {
               y: { title: { text: 'Power usage (KWh)', display: true } },
               x: { title: { text: 'Month', display: true } },
            }
         }
      }
      return {
         responsive: true, scales: {
            y: { title: { text: 'Power usage (KWh)', display: true } },
            x: { title: { text: 'Day', display: true } },
         }
      }
   }

   getLengthChartOptions() {
      if (this.isGrouped) {
         return {
            responsive: true, scales: {
               y: { title: { text: 'Time (h)', display: true } },
               x: { title: { text: 'Month', display: true } },
            }
         }
      }
      return {
         responsive: true, scales: {
            y: { title: { text: 'Time (h)', display: true } },
            x: { title: { text: 'Day', display: true } },
         }
      }
   }

   getLabels(): string[] {
      if (this.data !== undefined) {
         if (this.isGrouped) {
            return this.chartGroupedLabels
         }
         return this.data.map((x, i) => i.toString())
      }
      return []
   }

   ngOnDestroy(): void {
      this.sub?.unsubscribe();
   }

   changeGrouping(): void {
      this.isGrouped = !this.isGrouped
   }
}
