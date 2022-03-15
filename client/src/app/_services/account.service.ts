import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map, ReplaySubject } from 'rxjs';
import { User } from '../_models/user';

@Injectable({//service are injected in other comp in app; will stay initialized UNTIL APP IS CLOSED
  providedIn: 'root'//provided in app.module.ts
})
//will be used to make req to API
export class AccountService {
  baseUrl = "https://localhost:5001/api/"
  private currentUserSource = new ReplaySubject<User | null>(1)//kinda like buffer object; (1) => size of buffer(user object for current user)
  currentUser$ = this.currentUserSource.asObservable()

  constructor(private http: HttpClient) { }

  login(model: any) {
    return this.http
      .post<User>(this.baseUrl + "account/login", model).pipe(
        map((response: User) => {
          const user = response
          if (user) {
            localStorage.setItem("user", JSON.stringify(user))
            this.currentUserSource.next(user)
          }
        })
      )//model contains username and password
  }

  setCurrentUser(user: User) {
    this.currentUserSource.next(user)
  }

  logout() {
    localStorage.removeItem("user")
    this.currentUserSource.next(null)
  }

  register(model: any) {
    return this.http
      .post(this.baseUrl + "account/register", model)
      .pipe(map((user: any) => {
        if (user) {
          localStorage.setItem("user", JSON.stringify(user))
          this.currentUserSource.next(user)
        }
      })
      )
  }
}
