import { Component, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ActivatedRoute } from '@angular/router';
import { SharingItemInfoResponse } from '../../../models/cloud-management/sharingItemInfoResponse';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { environment } from 'projects/ui/src/environments/environment.prod';

@Component({
  selector: 'app-view-shared-item-details',
  standalone: false,

  templateUrl: './view-shared-item-details.component.html',
  styleUrl: './view-shared-item-details.component.css'
})
export class ViewSharedItemDetailsComponent implements OnInit {
  key:string;
  sharedItem:SharingItemInfoResponse;
  constructor(private toastService:ToastService,private cloudManagementService:CloudManagementService,private activatedRoute:ActivatedRoute){}
  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next:(param)=>{
        this.key = param["key"];
        this.cloudManagementService.sharingItemInfo(this.key).subscribe({
          next:(response)=>{
            this.sharedItem = response.data;
          },error:(err)=>{
            this.toastService.error("common.errorOccurred");
          }
        })
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
