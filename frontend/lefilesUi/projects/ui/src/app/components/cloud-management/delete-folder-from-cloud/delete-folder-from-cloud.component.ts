import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';

@Component({
  selector: 'app-delete-folder-from-cloud',
  templateUrl: './delete-folder-from-cloud.component.html',
  styleUrls: ['./delete-folder-from-cloud.component.css']
})
export class DeleteFolderFromCloudComponent implements OnInit {
  @Input() folderId:string;
  confirm:boolean = false;
  constructor(private cloudManagementService:CloudManagementService,private toastService:ToastService) { }

  ngOnInit(): void {
  
  }
  deleteFolder(){
    this.cloudManagementService.deleteFolder(this.folderId,true).subscribe({
      next:(response)=>{
        this.toastService.success("common.deletedSuccessfully");
      },error:(err)=>{
        this.toastService.error("common.errorOccurred");
      }
    })
  }
  

}
