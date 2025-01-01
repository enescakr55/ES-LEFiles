import { Component, OnInit } from '@angular/core';
import { CloudManagementService } from '../../services/cloud-management/cloud-management.service';
import { FileAndFoldersResponse, FileSystemEntryItemResponse } from '../../models/file-management/fileSystemEntryItemResponse';
import { ViewComponentModalService } from 'projects/corelib/src/lib/services/componentModal/view-component-modal.service';
import { CreateFolderToCloudComponent } from './create-folder-to-cloud/create-folder-to-cloud.component';
import { UpdateFolderFromCloudComponent } from './update-folder-from-cloud/update-folder-from-cloud.component';
import { DeleteFolderFromCloudComponent } from './delete-folder-from-cloud/delete-folder-from-cloud.component';
import { CloudFileUploadComponent } from './cloud-file-upload/cloud-file-upload.component';
import { DeleteFileFromCloudComponent } from './delete-file-from-cloud/delete-file-from-cloud.component';
import { HttpEventType } from '@angular/common/http';
import { WatchService } from '../../services/watch.service';

@Component({
  selector: 'app-cloud-management',
  templateUrl: './cloud-management.component.html',
  styleUrls: ['./cloud-management.component.css']
})
export class CloudManagementComponent implements OnInit {
  parentFolder:string = null;
  selectedItem:FileSystemEntryItemResponse|null = null;
  fileSystemEntries:FileAndFoldersResponse;
  loading:boolean = false;
  openingFolder:string|null ;
  hierarchicalView:boolean = true;
  fileView:boolean = false;
  typeFilters:string[] = [];
  constructor(private cloudManagementService:CloudManagementService,private watchService:WatchService,private componentModal:ViewComponentModalService) { }

  ngOnInit(): void {
    this.getFiles();
    this.listenUploadProgress();
  }
  listenUploadProgress(){
    this.watchService.getUploadState().subscribe({
      next:(changed)=>{
        if(changed == true){
          this.getFiles();
        }

      }
    })
  }
  changeTypeFilter(filter:string){
    var filterIndex = this.typeFilters.indexOf(filter);

    if(filterIndex == -1){
      this.typeFilters.push(filter);
    }else{
      this.typeFilters.splice(filterIndex,1);
    }
    this.getFiles();

  }
  downloadFile(){
    var uuid = (Math.random() + 1).toString(36).substring(7);
    var currentItem = Object.assign({},this.selectedItem);
    this.cloudManagementService.downloadFile(this.selectedItem.id).subscribe({
      next:(data)=>{
        switch(data.type){
          case HttpEventType.DownloadProgress:
            console.log(data.loaded);
            this.watchService.updateFileProgress({
              fileName:this.watchService.getProgress(uuid) == undefined ? this.selectedItem.name : this.watchService.getProgress(uuid).fileName ,
              fileId:this.selectedItem.id,
              progressId:uuid,
              lastUpdate:new Date(),
              progress:Math.round((data.loaded *100 )/ (data.total ?? 1)),
              status:"Downloading",
              text:"",
              type:"downloading"
            })
            this.watchService.downloadStateChanged();
            break;
          case HttpEventType.Response:
            console.log(data.body);
            var progress = this.watchService.getProgress(uuid);
            this.cloudManagementService.saveFile(data.body,progress.fileName);
            break;
        }
      }
      ,
      error:(err)=>{
        alert("Hata");
      },
    })
  }
  showHierarchicalView(){
    this.fileView = false;
    this.hierarchicalView = true;
    this.getFiles();
  }
  showFileView(){
    this.fileView = true;
    this.hierarchicalView = false;
    this.getFiles();
  }
  getFiles(){
    var viewtype = this.fileView == true ? 'f' : 'h'; 
    var filters = this.typeFilters.length != 0 ? this.typeFilters.join(',') : null;
    this.cloudManagementService.getFileSystemEntries(this.parentFolder,{filters:filters,viewtype:viewtype}).subscribe({
      next:(response)=>{
        this.fileSystemEntries = response.data;
        this.openingFolder = null;
      }
    })
  }
  homeFolder(){
    this.parentFolder = null;
    this.getFiles();
  }
  createFolder(){
    this.componentModal.showModal("cloudManagement.createFolder","component",CreateFolderToCloudComponent,{folderId:this.parentFolder}).then(()=>{
      this.getFiles();
    })
  }
  updateFolderModal(){
    this.componentModal.showModal("cloudManagement.updateFolder","component",UpdateFolderFromCloudComponent,{folderId:this.selectedItem.id}).then(()=>{
      this.getFiles();
    });
  }
  deleteFolderModal(){
    this.componentModal.showModal("cloudManagement.deleteFolder","component",DeleteFolderFromCloudComponent,{folderId:this.selectedItem.id}).then(()=>{
      this.getFiles();
    });
  }
  deleteFileModal(){
    this.componentModal.showModal("cloudManagement.deleteFile","component",DeleteFileFromCloudComponent,{fileId:this.selectedItem.id}).then(()=>{
      this.getFiles();
    });
  }
  uploadFile(){
    this.componentModal.showModal("cloudManagement.uploadFile","component",CloudFileUploadComponent,{folderId:this.parentFolder}).then(()=>{
      this.getFiles();
    });
  }
  dblAction(entry:FileSystemEntryItemResponse){
    if(entry.type == 0){
      this.openFolder(entry.id);
    }
  }
  openFolder(id:string){
    
    this.parentFolder = id;
    this.openingFolder = id;
    this.getFiles();
    this.selectedItem = null;
  }
  selectFolder(item:FileSystemEntryItemResponse){
    if(this.selectedItem != null && (this.selectedItem.id == item.id)){
      this.selectedItem = null;
    }else{
      this.selectedItem = item;
    }
  }
  deselectAll($ev:MouseEvent){
    if($ev.target !== $ev.currentTarget){
      return;
    }
    this.selectedItem = null;
  }

}
