import { Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ToastService {

  constructor(private toastService:ToastrService,private translateService:TranslateService) { }

  success(messagekey:string,titlekey:string|undefined=undefined){
    this.toastService.success(this.translateService.instant(messagekey),titlekey == null ? undefined : this.translateService.instant(titlekey));
  }
  error(messagekey:string,titlekey:string|undefined=undefined){
    this.toastService.error(this.translateService.instant(messagekey),titlekey == null ? undefined :this.translateService.instant(titlekey));
  }
  clear(){
    this.toastService.clear();
  }
}
