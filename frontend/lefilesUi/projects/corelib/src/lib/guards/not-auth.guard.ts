import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class NotAuthGuard  {
  constructor(private storageService:StorageService){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      var token = this.storageService.get("token");
      var expiration = this.storageService.get("expiration");
      console.log(new Date().getTime());
      console.log(new Date(expiration).getTime());
      if(token != null && expiration != null && new Date(expiration).getTime() > new Date().getTime()){
        return false;
      }
      return true;
  }
  
}
