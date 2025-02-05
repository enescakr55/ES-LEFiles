import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ToastrService } from 'ngx-toastr';
import { FileItemDetailsResponse } from '../../../models/file-management/fileItemDetailsResponse';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ShareFileRequest } from '../../../models/cloud-management/shareFileRequest';

@Component({
  selector: 'app-share-file',
  standalone: false,
  
  templateUrl: './share-file.component.html',
  styleUrl: './share-file.component.css'
})
export class ShareFileComponent implements OnInit {
  constructor(private cloudManagementService:CloudManagementService,private toastService:ToastService,private formBuilder:FormBuilder) {}
  shareForm:FormGroup;
  @Input() fileId:string;
  loading:boolean;
  shareSecurity:number = 0;
  fileDetails:FileItemDetailsResponse;
  ngOnInit(): void {
    this.getFileDetails();
    this.shareForm = this.formBuilder.group({
      fileId:[this.fileId,Validators.required],
      end:[null],
      access:[0],
      users:[null]
    })
    this.shareForm.get("users").disable();
  }

  getFileDetails(){
    this.loading = true;
    this.cloudManagementService.getFileDetails(this.fileId).subscribe({
      next:(response)=>{
        this.loading = false;
        this.fileDetails = response.data;
      },error:(err)=>{
        this.toastService.error("common.errorOccurred");
      }
    })
  }
  shareSecurityChanged(val:any){
    this.shareSecurity = typeof(val) == "string" ? parseInt(val) : val;
    if(this.shareSecurity == 2){
      this.shareForm.get("users").enable();
    }else{
      this.shareForm.get("users").disable();
      this.shareForm.get("users").setValue("");
    }
  }
  share(){
    if(this.shareForm.valid){
      var request:ShareFileRequest = this.shareForm.value;
      if((request.end as any) == ''){
        request.end = null;
      }else if(request.end != null && new Date(request.end).getDate() < new Date().getDate()){
        console.log(new Date(request.end).getDate());
        console.log(new Date().getDate())
        this.toastService.error("shareFile.endDateCannotBeLowerThanCurrentDate");
        return;
      }
      if(request.end != null){
        request.end = new Date(request.end).toJSON()
      }
      if(request.users == ''){
        request.users = null;
      }
      if(typeof(request.access) == "string"){
        request.access = parseInt(request.access);
      }
      this.cloudManagementService.shareFile(request).subscribe({
        next:(response)=>{
          this.toastService.success("shareFile.sharedSuccessfully");
        },error:(err)=>{
          this.toastService.error("common.errorOccurred");
        }
      })
      
    }else{
      this.toastService.error("common.pleaseCheckForm");
    }

  }
}
