import { Component, ErrorHandler, OnInit } from '@angular/core';
import { AuthBaseService } from '../../services/auth/auth-base.service';
import { FormGroup } from '@angular/forms';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { ToastService } from '../../services/toasts/toast-service.service';
import { StorageService } from '../../services/storage/storage.service';
import { ErrorHandlerService } from '../../services/handler/error-handler.service';
import { Router } from '@angular/router';

@Component({
    selector: 'lib-login-page',
    templateUrl: './login-page.component.html',
    styleUrls: ['./login-page.component.css'],
    standalone: false
})
export class LoginPageComponent implements OnInit {
  loginForm:FormGroup;
  loading:boolean = false;
  topMessageKey:string|null = null;
  bottomMessageKey:string|null = null;
  minimized:boolean = false;
  modalView:boolean = false;
  constructor(private authBaseService:AuthBaseService,private router:Router,private storageService:StorageService,private formBuilder:FormBuilder,private toastService:ToastService) { }

  ngOnInit(): void {
   this.loginForm = this.formBuilder.group({
      username:["",Validators.required],
      password:["",Validators.required]
    })
  }
  login(){
    var rememberMe = (document.getElementById("rememberMe") as HTMLInputElement).checked
    if(this.loginForm.valid){
      this.loading = true;
      var request = Object.assign({},this.loginForm.value);
      this.authBaseService.login(request).subscribe({
        next:(response)=>{
          this.storageService.set("token",response.data.token);
          this.storageService.set("expiration",response.data.expiration);
          var date = new Date(response.data.expiration)
          console.log(date.getTime() - new Date().getTime());
          this.toastService.success("authentication.loggedSuccessfully");
          this.storageService.set("username",request.username);
          if(rememberMe){
            this.storageService.set("savedUsername",request.username);
          }
          this.loading = false;
          if(this.modalView == false){
            this.router.navigate(["/main"]);
          }else{
            var closeBtn = document.getElementById("componentModal").querySelector("#modal-close-btn");
            (closeBtn as HTMLButtonElement).click();
            console.log(closeBtn);
          }

        },error:(err)=>{
          this.loading = false;
          ErrorHandlerService.HandleError(err);
        }
      })
    }
  }

}
