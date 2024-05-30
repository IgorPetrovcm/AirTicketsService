import { Routes } from '@angular/router';
import { MainPageComponent } from './pages/main-page/main-page.component';
import { FlightsPageComponent } from './pages/flights-page/flights-page.component';
import { SeatsPageComponent } from './pages/seats-page/seats-page.component';
import { CreateTicketsPageComponent } from './pages/create-tickets-page/create-tickets-page.component';

export const routes: Routes = [
    {path : "", component: MainPageComponent},
    {path : "home", component: MainPageComponent},
    {path : "flights", component: FlightsPageComponent},
    {path : "seats/:flightId", component: SeatsPageComponent},
    {path : "create-tickets/:flightId", component: CreateTicketsPageComponent},
];
