import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private toastService:ToastrService) { }

  success(message:string,title:string|undefined=undefined){
    this.toastService.success(message,title);
  }
  error(message:string,title:string|undefined=undefined){
    this.toastService.error(message,title);
  }
  clear(){
    this.toastService.clear();
  }
}
