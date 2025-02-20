import { Component, ElementRef, OnInit } from '@angular/core';
import { UserProfileService } from '../../services/user-profile/user-profile.service';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { ProfileDetailsResponse } from '../../models/user-profile/profileDetailsResponse';
import { ErrorHandlerService } from 'projects/corelib/src/lib/services/handler/error-handler.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
    selector: 'app-my-profile',
    templateUrl: './my-profile.component.html',
    styleUrls: ['./my-profile.component.css'],
    standalone: false
})
export class MyProfileComponent implements OnInit {
  profileDetails:ProfileDetailsResponse;
  loading:boolean;
  updateProfileForm:FormGroup;
  changePasswordForm:FormGroup;
  updateProfileLoader:boolean = false;
  changePasswordLoader:boolean = false;
  constructor(private formBuilder:FormBuilder,private userProfileService:UserProfileService,private toastService:ToastService) { }

  ngOnInit(): void {
    this.getProfileDetails().then(x=>{
      this.createChangePasswordForm();
      this.createUpdateProfileForm();
    });
  }
  getProfileDetails(){
    return new Promise<boolean>((resolve,reject)=>{
      this.loading = true;
      this.userProfileService.getProfileDetails().subscribe({
        next:(response)=>{
          this.loading = false;
          this.profileDetails = response.data;
          resolve(true);
        },error:(err)=>{
          ErrorHandlerService.HandleError(err);
          reject();
        }
      })
    })

  }
  createChangePasswordForm(){
    this.changePasswordForm = this.formBuilder.group({
      oldPassword:["",Validators.required],
      newPassword:["",Validators.required],
      newPasswordAgain:["",Validators.required]
    })
  }
  createUpdateProfileForm(){
    this.updateProfileForm = this.formBuilder.group({
      firstname:[this.profileDetails.firstname,Validators.required],
      lastname:[this.profileDetails.lastname,Validators.required],
      email:[this.profileDetails.email,Validators.required]
    })
  }
  updateProfile(){
    if(this.updateProfileForm.valid){
      this.updateProfileLoader = true;
      var request = Object.assign({},this.updateProfileForm.value);
      this.userProfileService.updateProfile(request).subscribe({
        next:(response)=>{
          this.toastService.success("profile.profileUpdatedSuccessfully");
          this.getProfileDetails();
          this.updateProfileLoader = false;
        },error:(err)=>{
          this.updateProfileLoader = false;
          ErrorHandlerService.HandleError(err);
        }
      })
    }else{
      this.toastService.error("common.pleaseCheckForm");
    }
  }
  changePassword(){
    if(this.changePasswordForm.valid){
      this.changePasswordLoader = true;
      var request = Object.assign({},this.changePasswordForm.value);
      this.userProfileService.changePassword(request).subscribe({
        next:(response)=>{
          this.changePasswordLoader = false;
          this.toastService.success("profile.passwordChangedSuccessfully");
          this.getProfileDetails();
        },error:(err)=>{
          this.changePasswordLoader = false;
          ErrorHandlerService.HandleError(err);
        }
      })
    }else{
      this.toastService.error("common.pleaseCheckForm");
    }
  }

}
