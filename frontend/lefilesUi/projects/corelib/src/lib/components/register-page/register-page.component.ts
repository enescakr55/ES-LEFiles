import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthBaseService } from '../../services/auth/auth-base.service';
import { StorageService } from '../../services/storage/storage.service';
import { ToastService } from '../../services/toasts/toast-service.service';
import { RegisterRequest } from '../../models/auth/registerRequest';
import { TranslateService } from '@ngx-translate/core';
import { ErrorHandlerService } from '../../services/handler/error-handler.service';

@Component({
    selector: 'lib-register-page',
    templateUrl: './register-page.component.html',
    styleUrls: ['./register-page.component.css'],
    standalone: false
})
export class RegisterPageComponent implements OnInit {
  registerForm:FormGroup
  constructor(private authBaseService:AuthBaseService,private translateService:TranslateService,private storageService:StorageService,private formBuilder:FormBuilder,private toastService:ToastService) { }

  ngOnInit(): void {
    this.registerForm = this.formBuilder.group({
      username:["",Validators.required],
      firstname:["",Validators.required],
      lastname:["",Validators.required],
      email:["",Validators.required],
      password:["",Validators.required]
    })
  }
  register(){
    if(this.registerForm.valid){
      var registerObj:RegisterRequest = Object.assign({},this.registerForm.value);
      this.authBaseService.register(registerObj).subscribe({
        next:(response)=>{
          this.toastService.success(this.translateService.instant("authentication.successfullyRegistered"));
        },error:(err)=>{
          console.log(err);
          ErrorHandlerService.HandleError(err);
        }
      })
    }
  }

}
