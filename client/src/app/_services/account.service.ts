import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({//service are injected in other comp in app; will stay initialized UNTIL APP IS CLOSED
  providedIn: 'root'//provided in app.module.ts
})
//will be used to make req to API
export class AccountService {
  baseUrl = "https://localhost:5001/api/"

  constructor(private htpp: HttpClient) { }

  login(model: any) {
    return this.htpp.post(this.baseUrl + "account/login", model)//model contains username and password
  }
}
