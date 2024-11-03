import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {

  constructor() { }
  set(key:string,val:string) : void{
    localStorage.setItem(key,val);
  }
  get(key:string) : string|null{
    return localStorage.getItem(key);
  }
  remove(key:string){
    localStorage.removeItem(key);
  }
  
}
