import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';

@Component({
  selector: 'app-delete-file-from-cloud',
  templateUrl: './delete-file-from-cloud.component.html',
  styleUrls: ['./delete-file-from-cloud.component.css']
})
export class DeleteFileFromCloudComponent implements OnInit {
  @Input() fileId:string;
  confirm:boolean = false;
  constructor(private cloudManagementService:CloudManagementService,private toastService:ToastService) { }

  ngOnInit(): void {
  }
  deleteFile(){
    this.cloudManagementService.deleteFile(this.fileId,this.confirm).subscribe({
      next:(response)=>{
        this.toastService.success("common.deletedSuccessfully");
      },error:(err)=>{
        this.toastService.error("common.errorOccurred");
      }
    })
  }

}
