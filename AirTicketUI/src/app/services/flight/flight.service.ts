import { Injectable } from '@angular/core';
import { RestService } from '../rest.service';
import { HttpResponse } from '@angular/common/http';
import { ChoosingFlight, Flight } from '../../models/flight';
import { catchError, throwError, map } from 'rxjs';
import { Seat } from '../../models/seta';

@Injectable({
  providedIn: 'root'
})
export class FlightService {

  constructor(private restService: RestService) {}

  public getFlight(flightParameters: ChoosingFlight) {
 
    let formattedDate : string = this.formatDate(flightParameters.scheduledDeparture)

    const requestParams = {
      ...flightParameters,
      scheduledDeparture: formattedDate
    };

    return this.restService.restGET<Flight[]>(`/api/Flight/choosing`,{...requestParams}
    )
      .pipe(
        catchError(err => {
          return throwError(err);
        })
      )        
  }

  public getSeats(flightId : number) {
    console.log(`api/Seat/available/${flightId}`)
    return this.restService.restGET<Seat[]>(`/api/Seat/available/${flightId}`
    )
      .pipe(
        catchError(err => {
          return throwError(err);
        })
      )     
  }

  formatDate(date: Date): string {
    const year = date.getFullYear();
    const month = (date.getMonth() + 1).toString().padStart(2, '0'); // getMonth() возвращает значение от 0 до 11, поэтому добавляем 1
    const day = date.getDate().toString().padStart(2, '0');

    return `${year}.${month}.${day}`;
  }
}
