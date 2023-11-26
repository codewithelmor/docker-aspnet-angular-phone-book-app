import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ContactComponent } from './contact/contact.component';
import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';

const routes: Routes = [
  { path: 'weather-forecast', component: WeatherForecastComponent },
  { path: 'contact', component: ContactComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
