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
import { PreviewFileComponent } from './preview-file/preview-file.component';
import { FileItemDetailsResponse } from '../../models/file-management/fileItemDetailsResponse';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { FolderItemDetailsResponse } from '../../models/file-management/folderItemDetailsResponse';
import { RenameFileComponent } from './rename-file/rename-file.component';
import { Observable } from 'rxjs';
import { ShareFileComponent } from './share-file/share-file.component';

@Component({
    selector: 'app-cloud-management',
    templateUrl: './cloud-management.component.html',
    styleUrls: ['./cloud-management.component.css'],
    standalone: false
})
export class CloudManagementComponent implements OnInit {
  parentFolder: string = null;
  selectedItem: FileSystemEntryItemResponse | null = null;
  fileSystemEntries: FileAndFoldersResponse;
  loading: boolean = false;
  openingFolder: string | null;
  hierarchicalView: boolean = true;
  fileView: boolean = false;
  typeFilters: string[] = [];
  showDetailsArea: boolean = true;
  fileDetails: FileItemDetailsResponse;
  folderDetails:FolderItemDetailsResponse;
  fileDetailsLoading: boolean = false;
  fileDetailsError: boolean = false;
  multipleSelection: boolean = false;
  selectedItems: FileSystemEntryItemResponse[] = []; //Multiple selection
  listView:boolean = false;
  movement: boolean = false; //Çoklu seçim


  //File Search
  searchText:string|null = null;
  //Folder Movement
  moveFolder:boolean = false;
  movingSourceFolder:string;


  //File Movement

  moveProcess:boolean = false;
  movingItems: string[] = [];

  //File Copy
  copyProcess:boolean = false;
  constructor(private cloudManagementService: CloudManagementService, private toastService: ToastService, private watchService: WatchService, private componentModal: ViewComponentModalService) { }

  ngOnInit(): void {
    this.getFiles();
    this.listenUploadProgress();
  }
  listenUploadProgress() {
    this.watchService.getUploadState().subscribe({
      next: (changed) => {
        if (changed == true) {
          this.getFiles();
        }

      }
    })
  }
  changeTypeFilter(filter: string) {
    var filterIndex = this.typeFilters.indexOf(filter);

    if (filterIndex == -1) {
      this.typeFilters.push(filter);
    } else {
      this.typeFilters.splice(filterIndex, 1);
    }
    this.getFiles();

  }

  downloadFile() {
    var uuid = (Math.random() + 1).toString(36).substring(7);
    var currentItem = Object.assign({}, this.selectedItem);
    this.cloudManagementService.downloadFile(this.selectedItem.id).subscribe({
      next: (data) => {
        switch (data.type) {
          case HttpEventType.DownloadProgress:
            console.log(data.loaded);
            this.watchService.updateFileProgress({
              fileName: this.watchService.getProgress(uuid) == undefined ? this.selectedItem.name : this.watchService.getProgress(uuid).fileName,
              fileId: this.selectedItem.id,
              progressId: uuid,
              lastUpdate: new Date(),
              progress: Math.round((data.loaded * 100) / (data.total ?? 1)),
              status: "Downloading",
              text: "",
              type: "downloading"
            })
            this.watchService.downloadStateChanged();
            break;
          case HttpEventType.Response:
            console.log(data.body);
            var progress = this.watchService.getProgress(uuid);
            this.cloudManagementService.saveFile(data.body, progress.fileName);
            break;
        }
      }
      ,
      error: (err) => {
        alert("Hata");
      },
    })
  }
  showHierarchicalView() {
    this.fileView = false;
    this.hierarchicalView = true;
    this.getFiles();
  }
  showFileView() {
    this.fileView = true;
    this.hierarchicalView = false;
    this.getFiles();
  }
  changeMultipleSelection(){
    this.multipleSelection = !this.multipleSelection;
    this.selectedItem = null;
    this.selectedItems = [];
  }
  selectedItemsFindIndex(itemid: string) {
    return this.selectedItems.findIndex(x => x.id == itemid);
  }
  selectedFolderCount(){
    return this.selectedItems.filter(x=>x.type == 0).length;
  }
  selectedFileCount(){
    return this.selectedItems.filter(x=>x.type == 1).length;
  }
  getFiles():Promise<boolean> {
    return new Promise((resolve,reject)=>{
      var viewtype = this.fileView == true ? 'f' : 'h';
      var filters = this.typeFilters.length != 0 ? this.typeFilters.join(',') : null;
      this.cloudManagementService.getFileSystemEntries(this.parentFolder, { filters: filters, viewtype: viewtype }).subscribe({
        next: (response) => {
          this.fileSystemEntries = response.data;
          this.openingFolder = null;
          resolve(true);
        },error:(err)=>{
          reject(false);
        }
      })
    })

  }
  previewFile(file:FileSystemEntryItemResponse = undefined) {
    this.componentModal.showModal("cloudManagement.preview", "component", PreviewFileComponent, { fileId: file == undefined ? this.selectedItem.id : file.id }, { width: "medium" });
  }
  homeFolder() {
    this.parentFolder = null;
    this.getFiles();
  }
  renameFile(){
    this.componentModal.showModal("cloudManagement.renameFile","component",RenameFileComponent, {fileName:this.selectedItem.name,fileId:this.selectedItem.id}).then(()=>{
      this.getFiles().then(()=>{
        this.selectedItem = this.fileSystemEntries.entries.find(x=>x.id == this.selectedItem.id);
        this.getFileDetails(this.selectedItem);
      });
      
    })
  }
  createFolder() {
    this.componentModal.showModal("cloudManagement.createFolder", "component", CreateFolderToCloudComponent, { folderId: this.parentFolder }).then(() => {
      this.getFiles();
    })
  }
  shareFile(){
    this.componentModal.showModal("cloudManagement.shareFile","component",ShareFileComponent,{fileId:this.selectedItem.id}).then(()=>{
      this.getFileDetails(this.selectedItem);
    })
  }
  updateFolderModal() {
    this.componentModal.showModal("cloudManagement.updateFolder", "component", UpdateFolderFromCloudComponent, { folderId: this.selectedItem.id }).then(() => {
      this.getFiles();
    });
  }
  deleteFolderModal() {
    this.componentModal.showModal("cloudManagement.deleteFolder", "component", DeleteFolderFromCloudComponent, { folderId: this.selectedItem.id }).then(() => {
      this.getFiles();
    });
  }
  deleteFileModal() {
    this.componentModal.showModal("cloudManagement.deleteFile", "component", DeleteFileFromCloudComponent, { fileId: this.selectedItem.id }).then(() => {
      this.getFiles();
    });
  }
  uploadFile() {
    this.componentModal.showModal("cloudManagement.uploadFile", "component", CloudFileUploadComponent, { folderId: this.parentFolder }).then(() => {
      this.getFiles();
    });
  }
  dblAction(entry: FileSystemEntryItemResponse) {
    if (entry.type == 0) {
      this.openFolder(entry.id);
    }else if(entry.type == 1){
      if(entry.allowPreview == true) {
        this.selectedItem = entry;
        this.previewFile(entry);
      }
    }
  }
  openFolder(id: string) {

    this.fileDetails = null;
    this.folderDetails = null;
    this.parentFolder = id;
    this.openingFolder = id;
    this.getFiles();
    this.selectedItem = null;

  }
  selectEntry(item: FileSystemEntryItemResponse) {
    if (!this.movement) {
      if (this.multipleSelection == false) {
        if (this.selectedItem != null && (this.selectedItem.id == item.id)) {
          this.selectedItem = null;
        } else {
          this.selectedItem = item;
          this.getFileDetails(item);
        }
      }else if(this.multipleSelection == true){
        this.selectedItem = null;
        var selectedIndex = this.selectedItemsFindIndex(item.id)
        if(selectedIndex == -1){
          this.selectedItems.push(item);
        }else{
          this.selectedItems.splice(selectedIndex,1);
        }
      }
    }
  }

  deselectAll($ev: MouseEvent) {
    if ($ev.target !== $ev.currentTarget) {
      return;
    }
    this.deselectAllProcess();
  }
  deselectAllProcess(){
    this.selectedItem = null;
    this.selectedItems = [];
    this.fileDetails = null;
  }
  getFileDetails(entry: FileSystemEntryItemResponse) {
    if (entry.type == 1 && this.showDetailsArea == true) {
      this.fileDetailsLoading = true;
      this.fileDetailsError = false;
      this.cloudManagementService.getFileDetails(entry.id).subscribe({
        next: (response) => {
          this.fileDetails = response.data;
          this.fileDetailsLoading = false;
        }, error: (err) => {
          this.fileDetailsLoading = false;
          this.fileDetailsError = true;
        }
      })
    }else if(entry.type == 0 && this.showDetailsArea) {
      this.fileDetailsLoading = true;
      this.fileDetailsError = false;
      this.cloudManagementService.getFolderDetails(entry.id).subscribe({
        next: (response) => {
          this.folderDetails = response.data;
          this.fileDetailsLoading = false;
        }, error: (err) => {
          this.fileDetailsLoading = false;
          this.fileDetailsError = true;
        }
      })
    } else {
      this.fileDetails = null;
      this.fileDetailsLoading = false;
    }

  }
  acceptFilesForMovement(){
    this.copyProcess = false;
    this.moveProcess = true;
    this.selectFilesForMovement();
  }
  acceptFilesForCopying(){
    this.moveProcess = false;
    this.copyProcess = true;
    this.selectFilesForMovement();
  }
  selectFilesForMovement() {

    var itemIds: string[] = [];
    if (!this.multipleSelection) {
      itemIds.push(this.selectedItem.id);
    } else {
      var selectedIds = this.selectedItems.map((val, index, arr) => {
        return val.id
      });
      itemIds.push(...selectedIds);
    }
    this.movingItems = itemIds;
    if (itemIds.length > 0) {
      this.movement = true;
    } else {
      this.toastService.error("coomon.pleaseSelectAtLeastOneFileForMovement");
    }
  }

  copyFiles(){
    var destinationFolder = this.parentFolder;
    this.cloudManagementService.copyFiles({
      destination: destinationFolder,
      sourceFiles: this.movingItems
    }).subscribe({
      next: (response) => {
        this.movement = false;
        this.moveProcess = false;
        this.selectedItem = null;
        this.selectedItems = [];
        this.toastService.success("cloudManagement.filesMovedSuccessfully");
        this.getFiles();
      }, error: (err) => {
        this.toastService.error("cloudManagement.fileMovingError");
      }
    })
  }

  moveFiles() {
    var destinationFolder = this.parentFolder;
    this.cloudManagementService.moveFiles({
      destination: destinationFolder,
      sourceFiles: this.movingItems
    }).subscribe({
      next: (response) => {
        
        this.movement = false;
        this.moveProcess = false;
        this.selectedItem = null;
        this.selectedItems = [];
        this.toastService.success("cloudManagement.filesCopiedSuccessfully");
        this.getFiles();
      }, error: (err) => {
        this.toastService.error("cloudManagement.fileCopyingError");
      }
    })
  }
  beginMoveFolder(){
    this.moveFolder = true;
    this.movingSourceFolder = this.selectedItem.id;
    this.movement = true;
  }
  acceptMoveFolder(){
    this.cloudManagementService.moveFolder({
      sourceFolderId:this.movingSourceFolder,
      targetFolderId:this.parentFolder
    }).subscribe({
      next:(response)=>{
        this.toastService.success("cloudManagement.folderMovedSuccessfully");
        this.getFiles();
        this.selectedItem = null;
        this.moveFolder = false;
        this.movement = false;
        this.movingSourceFolder = null;
      },error:(err)=>{
        this.toastService.error("common.errorOccurred");
        this.selectedItem = null;
        this.movement = false;
        this.moveFolder = false;
        this.movingSourceFolder = null;
      }
    })
  }
  cancelMovement() {
    this.movement = false;
    this.copyProcess = false;
    this.moveProcess = false;
    this.moveFolder = false;
    this.movingSourceFolder = null;
    this.selectedItem = null;
    this.selectedItems = [];
  }
  searchInputKeydown(ref:HTMLInputElement, keyEvent:any){
    if(keyEvent.code == "Enter"){
      this.searchBySearchParam(ref.value);
    }
  }
  searchBySearchParam(param:string){
    if(param == null || param == ""){
      this.searchText = null;
      this.getFiles();
    }else{
      this.cloudManagementService.searchFilesystem(param).subscribe({
        next:(response)=>{
          this.searchText = response.data.searchText;
          this.fileSystemEntries = {parents:[],entries:response.data.result}
        },error:(err)=>{
          this.toastService.error("common.errorOccurred");
        }
      })
    }

  }

}
