import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { StorageService } from '../services/storage/storage.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard  {
  constructor(private storageService:StorageService,private router:Router){}
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      var token = this.storageService.get("token");
      var expiration = this.storageService.get("expiration");
      if(token != null && expiration != null && new Date(expiration).getTime() > new Date().getTime()){
        return true;
      }
      this.router.navigate(["/login"])
      return false;
  }
  
}
