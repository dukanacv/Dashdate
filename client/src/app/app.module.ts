import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from "@angular/common/http"
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NavComponent } from './nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';


@NgModule({
  declarations: [//all our components go in here
    AppComponent, NavComponent
  ],
  imports: [//importing "foreign" components
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    FormsModule,//imported forms module
    BsDropdownModule.forRoot()//import dropdown module from ngx bootstrap
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
