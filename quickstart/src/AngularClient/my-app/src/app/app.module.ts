import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { FreeDataComponent } from './free-data/free-data.component';
import { PremiumDataComponent } from './premium-data/premium-data.component';

@NgModule({
  declarations: [
    AppComponent,
    FreeDataComponent,
    PremiumDataComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
