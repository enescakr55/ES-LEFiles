import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CreateFolderRequest } from '../../../models/cloud-management/createFolderRequest';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-create-folder-to-cloud',
  templateUrl: './create-folder-to-cloud.component.html',
  styleUrls: ['./create-folder-to-cloud.component.css']
})
export class CreateFolderToCloudComponent implements OnInit {
  createFolderForm:FormGroup;
  @Input() folderId?:string = undefined;
  constructor(private cloudManagementService:CloudManagementService,private formBuilder:FormBuilder,private translateService:TranslateService,private toastService:ToastService) { }

  ngOnInit(): void {
    this.createFolderForm = this.formBuilder.group({
      folderName:["",Validators.required],
    })
  }
  createFolder(){
    if(this.createFolderForm.valid){
      var createFolderValues:CreateFolderRequest = Object.assign({},this.createFolderForm.value);
      createFolderValues.parentFolder = this.folderId ?? null;
      this.cloudManagementService.createFolder(createFolderValues).subscribe({
        next:(response)=>{
          this.toastService.success(this.translateService.instant('common.createdSuccessfully'));
        },error:(err)=>{this.toastService.error(this.translateService.instant('common.unknownError'))}
      })
    }else{
      this.toastService.error(this.translateService.instant("common.errorOccurred"))
    }

    
  }


}
