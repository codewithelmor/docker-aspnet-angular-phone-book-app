import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ContactComponent } from './contact/contact.component';
import { AddContactComponent } from './shared/components/add-contact/add-contact.component';
import { UpdateContactComponent } from './shared/components/update-contact/update-contact.component';
import { ContactFilterPipe } from './shared/pipes/contact-filter.pipe';
import { WeatherForecastComponent } from './weather-forecast/weather-forecast.component';

@NgModule({
  declarations: [
    AppComponent,
    WeatherForecastComponent,
    ContactComponent,
    AddContactComponent,
    UpdateContactComponent,
    // Pipes
    ContactFilterPipe,
  ],
  imports: [
    BrowserModule,
    CommonModule,
    HttpClientModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
