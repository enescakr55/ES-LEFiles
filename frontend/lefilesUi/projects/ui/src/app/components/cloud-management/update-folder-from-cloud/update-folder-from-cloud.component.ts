import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { UpdateFolderContentsResponse } from '../../../models/cloud-management/updateFolderContentsResponse';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UpdateFolderRequest } from '../../../models/cloud-management/updateFolderRequest';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'app-update-folder-from-cloud',
    templateUrl: './update-folder-from-cloud.component.html',
    styleUrls: ['./update-folder-from-cloud.component.css'],
    standalone: false
})
export class UpdateFolderFromCloudComponent implements OnInit {
  @Input() folderId:string;
  updateContents:UpdateFolderContentsResponse;
  updateFolderForm:FormGroup;
  constructor(private cloudManagementService:CloudManagementService,private formBuilder:FormBuilder,private translateService:TranslateService,private toastService:ToastService) { }

  ngOnInit(): void {
    this.getUpdateContents();
  }
  prepareForm(){
    this.updateFolderForm = this.formBuilder.group({
      folderId:[this.folderId,Validators.required],
      folderName:[this.updateContents.folderName,Validators.required],
      shared:[this.updateContents.shared]
    })
  }
  getUpdateContents(){
    this.cloudManagementService.updateFolderContents(this.folderId).subscribe({
      next:(response)=>{
        this.updateContents = response.data;
        this.prepareForm();


      }
    })
  }
  updateFolder(){
    if(this.updateFolderForm.valid){
      var updateFolderReq:UpdateFolderRequest = Object.assign({},this.updateFolderForm.value);
      this.cloudManagementService.updateFolder(updateFolderReq).subscribe({
        next:(response)=>{
          this.toastService.success("common.updatedSuccessfully");
        },error:(err)=>{
          this.toastService.error("common.errorOccurred");
        }
      })
    }else{
      this.toastService.error("common.invalidForm");
    }
  }

}
