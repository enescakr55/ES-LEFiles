import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DeviceManagementService } from '../../../services/device-management/device-management.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-register-new-device',
  templateUrl: './register-new-device.component.html',
  styleUrls: ['./register-new-device.component.css']
})
export class RegisterNewDeviceComponent implements OnInit {
  registerDeviceForm:FormGroup
  @Output() refresh = new EventEmitter<any>(); 
  constructor(private deviceManagement:DeviceManagementService,private formBuilder:FormBuilder,private translateService:TranslateService,private toastService:ToastService) { }

  ngOnInit(): void {
    this.createForm();
  }
  createForm(){
    this.registerDeviceForm = this.formBuilder.group({
      clientName:["",Validators.required]
    })
  }
  registerDeviceRequest(){
    if(this.registerDeviceForm.valid){
      var req = this.registerDeviceForm.value;
      this.deviceManagement.registerNewDeviceRequest(req).subscribe({
        next:(response)=>{
          this.toastService.success(this.translateService.instant("devices.registerRequestCreated"));
          this.refresh.emit(Math.random());
        },error:(err)=>{
          this.toastService.error(this.translateService.instant("common.errorOccurred"));
        }
      })
    }else{
      this.toastService.error(this.translateService.instant("common.formValidationErrorMessage"));
    }
  }

}
