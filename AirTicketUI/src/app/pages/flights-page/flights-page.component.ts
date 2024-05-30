import { Component } from '@angular/core';
import { FlightService } from '../../services/flight/flight.service';
import { MatButtonModule } from '@angular/material/button';
import { ChoosingFlight, Flight } from '../../models/flight';
import { MatTableModule } from '@angular/material/table';
import { CommonModule, NgFor, NgIf } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import {MatDatepickerModule} from '@angular/material/datepicker';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { Router } from '@angular/router';



@Component({
  selector: 'app-flights-page',
  standalone: true,
  imports: [MatButtonModule, MatTableModule, MatFormFieldModule, MatDatepickerModule, MatInputModule, ReactiveFormsModule, CommonModule, NgFor],
  templateUrl: './flights-page.component.html',
  styleUrl: './flights-page.component.scss'
})
export class FlightsPageComponent {

  //TODO: тут нужны реактивные формы
  flights : Flight[] = []
  displayedColumns: string[] = [
    'flightId', 'flightNo', 'scheduledDeparture', 'scheduledArrival',
    'departureAirport', 'arrivalAirport', 'status', 'aircraftCode',
    'actualDeparture', 'actualArrival', 'act'
  ];

  flightForm: FormGroup;

  constructor(private flightService: FlightService, private fb: FormBuilder,  private router: Router) {
    this.flightForm = this.fb.group({
      departureAirport: [''],
      arrivalAirport: [''],
      scheduledDeparture: ['']
    });
  }

  public getFlight() {
    const formValues = this.flightForm.value;
    const flightParams: ChoosingFlight = {
      ...formValues,
    };

    console.log(flightParams)

    this.flightService.getFlight(flightParams)
    .subscribe((data : Flight[]) => {
       this.flights = [...data];
    })
  }

  showSeats(flightId: number) {
    if (flightId) {
      this.router.navigate(['/seats', flightId]);
    }
  }
}
