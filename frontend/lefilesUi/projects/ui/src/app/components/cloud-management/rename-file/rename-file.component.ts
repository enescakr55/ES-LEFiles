import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { RenameFileRequest } from '../../../models/cloud-management/renameFileRequest';

@Component({
  selector: 'app-rename-file',
  standalone:false,
  templateUrl: './rename-file.component.html',
  styleUrl: './rename-file.component.css'
})
export class RenameFileComponent implements OnInit {
  renameFileForm:FormGroup
  @Input() fileId:string;
  @Input() fileName:string;
  constructor(private cloudManagementService:CloudManagementService,private formBuilder:FormBuilder,private toastService:ToastService) {}
  ngOnInit(): void {
    this.renameFileForm = this.formBuilder.group({
      fileId:[this.fileId,Validators.required],
      fileName:[this.fileName,Validators.required]
    })
  }
  renameFile(){
    if(this.renameFileForm.valid){
      var renameFileReq:RenameFileRequest = Object.assign({},this.renameFileForm.value);
      this.cloudManagementService.renameFile(renameFileReq).subscribe({
        next:(response)=>{
          this.toastService.success("common.fileSuccessfullyRenamed");
        },error:(err)=>{
          this.toastService.error("common.errorOccurred");
        }
      })
    }else{
      this.toastService.error("common.pleaseCheckFrom");
    }
  }

  
}
