import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { delay, finalize, Observable } from 'rxjs';
import { LoadSpinnerService } from '../_services/load-spinner.service';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private loadSpinnerService: LoadSpinnerService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    this.loadSpinnerService.busy()
    return next.handle(request).pipe(
      delay(800),
      finalize(() => {
        this.loadSpinnerService.idle()
      })
    );
  }
}
