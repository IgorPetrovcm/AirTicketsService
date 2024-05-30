import { Component, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Seat } from '../../models/seta';
import { FlightService } from '../../services/flight/flight.service';
import { MatButtonModule } from '@angular/material/button';
import { MatTableModule } from '@angular/material/table';

@Component({
  selector: 'app-seats-page',
  standalone: true,
  imports: [  MatTableModule, MatButtonModule,],
  templateUrl: './seats-page.component.html',
  styleUrl: './seats-page.component.scss'
})
export class SeatsPageComponent {
  flightId!: number;
  seats : Seat[] = [];
  displayedColumns: string[] = [
    'aircraftCode',
    'seatNo',
    'fareConditions',    
  ];

  constructor(private route: ActivatedRoute, 
    private flightService : FlightService
  ) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.flightId = +params['flightId']; 
    });

    if (this.flightId != null) {
      this.flightService.getSeats(this.flightId)
      .subscribe((data : Seat[]) => {
        this.seats = [...data]
        console.log(this.seats)
      })
    }
  }
}
