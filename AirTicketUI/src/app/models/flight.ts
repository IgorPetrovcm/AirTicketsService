export interface Flight { 
    flightId: number,
    flightNo: string
    scheduledDeparture: string,
    scheduledArrival: string,
    departureAirport: string,
    arrivalAirport: string,
    status: string,
    aircraftCode: number,
    actualDeparture: string,
    actualArrival: string
}

export interface ChoosingFlight {
  scheduledDeparture: Date
  departureAirport: string,
  arrivalAirport: string
}