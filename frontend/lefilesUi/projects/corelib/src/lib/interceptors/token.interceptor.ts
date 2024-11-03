import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storage/storage.service';

@Injectable()
export class TokenInterceptor implements HttpInterceptor {

  constructor(private storageService:StorageService) {}

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    var token = this.storageService.get("token");
    var expiration = this.storageService.get("expiration");
    if(token != null){
      request.headers.append("Authorization","Bearer "+token);
      return next.handle(request);
    }
    return next.handle(request);
  }
}
