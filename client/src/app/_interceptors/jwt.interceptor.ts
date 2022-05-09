import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable, take } from 'rxjs';
import { AccountService } from '../_services/account.service';
import { User } from '../_models/user';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {

  constructor(private accountService: AccountService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    let currentUser!: User | null

    this.accountService.currentUser$.pipe(take(1)).subscribe(user => currentUser = user)
    //with take -> dont have to unsubscribe from observable, use it when not sure if want to unsubscribe afterwards

    if (currentUser) {
      request = request.clone({
        setHeaders: {
          Authorization: `Bearer ${currentUser.token}`//attach token to every req when logged in and send it with req
        }
      })
    }

    return next.handle(request);
  }
}
