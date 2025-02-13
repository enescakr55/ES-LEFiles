import { inject, Injectable } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { Toast, ToastrComponentlessModule, ToastrService } from 'ngx-toastr';
import { ToastService } from '../toasts/toast-service.service';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor(private toastrService:ToastService,private translateService:TranslateService) { }
  static translateService:TranslateService;
  static toastrService:ToastrService;
  static HandleError(err:any){
    var toastrService = this.toastrService
    var translateService = this.translateService;
    if(err.error.message != null && err.error.message != ''){
      toastrService.error(err.error.message);
    }else{
      toastrService.error(translateService.instant("common.errorOccurred"));
    }
  }
}
