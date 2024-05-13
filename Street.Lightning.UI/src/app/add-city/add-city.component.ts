import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card'
import { MatFormFieldModule } from '@angular/material/form-field'
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-add-city',
  standalone: true,
  imports: [MatCardModule, MatFormFieldModule, MatInputModule],
  templateUrl: './add-city.component.html',
  styleUrl: './add-city.component.scss'
})
export class AddCityComponent {

}
