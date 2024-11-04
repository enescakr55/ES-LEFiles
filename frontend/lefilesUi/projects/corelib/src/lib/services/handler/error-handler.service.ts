import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class ErrorHandlerService {

  constructor(private toastrService:ToastrService) { }
  static HandleError(err:any){
    
  }
}
