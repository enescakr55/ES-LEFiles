import { Component, OnInit } from '@angular/core';
import { AuthBaseService } from '../../services/auth/auth-base.service';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ToastService } from '../../services/toasts/toast-service.service';
import { StorageService } from '../../services/storage/storage.service';

@Component({
  selector: 'lib-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.css']
})
export class LoginPageComponent implements OnInit {
  registerForm:FormGroup
  constructor(private authBaseService:AuthBaseService,private storageService:StorageService,private formBuilder:FormBuilder,private toastService:ToastService) { }

  ngOnInit(): void {
   this.registerForm = this.formBuilder.group({
      username:["",Validators.required],
      password:["",Validators.required]
    })
  }
  login(){
    if(this.registerForm.valid){
      var request = Object.assign({},this.registerForm.value);
      this.authBaseService.login(request).subscribe({
        next:(response)=>{
          this.storageService.set("token",response.data.token);
          this.storageService.set("expiration",response.data.expiration);
          var date = new Date(response.data.expiration)
          console.log(date.getTime() - new Date().getTime());
          this.toastService.success("Ok");
        },error:(err)=>{
          this.toastService.error("Hata");
        }
      })
    }
  }

}
