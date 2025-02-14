import { Component, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ActivatedRoute } from '@angular/router';
import { SharingItemInfoResponse } from '../../../models/cloud-management/sharingItemInfoResponse';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { environment } from 'projects/ui/src/environments/environment.prod';
import { ViewComponentModalService } from 'projects/corelib/src/lib/services/componentModal/view-component-modal.service';
import { LoginPageComponent } from 'projects/corelib/src/lib/components/login-page/login-page.component';

@Component({
  selector: 'app-view-shared-item-details',
  standalone: false,

  templateUrl: './view-shared-item-details.component.html',
  styleUrl: './view-shared-item-details.component.css'
})
export class ViewSharedItemDetailsComponent implements OnInit {
  key:string;
  sharedItem:SharingItemInfoResponse;
  unauthenticated:boolean = false;
  unauthorized:boolean = false;
  constructor(private toastService:ToastService,private componentModalService:ViewComponentModalService,private cloudManagementService:CloudManagementService,private activatedRoute:ActivatedRoute){}
  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next:(param)=>{
        this.key = param["key"];
        this.getSharedItemInfo();
      }
    })
  }
  getSharedItemInfo(){
    this.cloudManagementService.sharingItemInfo(this.key).subscribe({
      next:(response)=>{
        this.sharedItem = response.data;
      },error:(err)=>{
        if(err.status == 401){
          //login page
          this.componentModalService.showModal("common.login", "component",LoginPageComponent,{modalView:true,minimized:true,topMessageKey:'fileSharing.pleaseLoginToAccessFile'}).then(x=>{
            this.getSharedItemInfo();
          })
        }else if(err.status == 403){
          this.unauthorized = true;
        }else{
          //this.toastService.error("common.errorOccurred");
          //general error page
        }

      }
    })
  }
  generateAccessKey():Promise<string> {
    return new Promise<string>((resolve,reject)=>{
      this.cloudManagementService.generateSharingItemAccessToken(this.key).subscribe({
        next:(response)=>{
          resolve(response.data);
          console.log(response.data);
        },error:(err)=>{
          reject();
        }
      })
    })
  }
  downloadFile(){
    this.generateAccessKey().then(data=>{
      const a = document.createElement('a')
      a.href = environment.apiUrl+`shared/${this.key}/download?token=${data}`
      a.download = environment.apiUrl+`shared/${this.key}/download?token=${data}`
      document.body.appendChild(a)
      a.click()
      document.body.removeChild(a)
    })
  }

}
