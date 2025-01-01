import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ToastService } from 'projects/corelib/src/lib/services/toasts/toast-service.service';
import { WatchService } from '../../../services/watch.service';
declare var window:any;
//Ana sayfada indirmeleri yönetmek için bir arayüz eklenecek. Buradaki işlemler window'da herhangi bir parametreye yönlendirilecek. (mevcut part, toplam part,dosya adı, iptal seçeneği)...
@Component({
  selector: 'app-cloud-file-upload',
  templateUrl: './cloud-file-upload.component.html',
  styleUrls: ['./cloud-file-upload.component.css']
})
export class CloudFileUploadComponent implements OnInit {

  constructor(private cloudManagementService:CloudManagementService,private toastService:ToastService,private watchService:WatchService) { }
  @Input() folderId:string|undefined;
  entryId:string;
  uploadFile:File;
  fileChunk : 0;
  part: 0;
  currentFileName:string;
  uuid:string = (Math.random() + 1).toString(36).substring(7);
  ngOnInit(): void {
  
  }
  handleFile($ev:any){
    this.uploadFile = ($ev.target as any).files[0];
  }
  beginUpload(){
    this.fileChunk = 0;
    this.part = 0;
    if(this.uploadFile == null){
      this.toastService.error("cloudManagement.pleaseCheckFileForFileUpload");
      return;
    }
    this.cloudManagementService.createFileEntry(this.folderId).subscribe({
      next:(response)=>{
        console.log(response);
        this.entryId = response.data;
        this.currentFileName = this.uploadFile.name;
        this.startUpload();
      },error:(err)=>{
        this.toastService.error("common.errorOccurred");
        return;
      }
    })

  }
  startUpload(){
    var chunkSize = 1024 * 1024 * 10;

    if(this.fileChunk < this.uploadFile.size){
      var endStatus = this.fileChunk+chunkSize >= this.uploadFile.size ? true : false;
      this.uploadProcess(0,++this.part,this.uploadFile.slice(this.fileChunk,this.fileChunk+chunkSize),this.fileChunk == 0 ? true : false,endStatus).then((x)=>{
        if(x==true){
          this.fileChunk+=chunkSize;
          console.log("startupload-fnc")
          this.watchService.updateFileProgress({
            fileId:this.entryId,
            progressId:this.uuid,
            progress:Math.round((this.fileChunk*100) /this.uploadFile.size),
            status:"Uploading",
            fileName:this.currentFileName,
            text:"",
            type:"uploading",
            lastUpdate:new Date()
          })
          this.watchService.uploadStateChanged(endStatus);
          this.startUpload();
        }else{
          this.toastService.error("cloudManagement.fileUploadFailed");
          return;
        }
      })
    }else{
      this.toastService.success("File uploaded successfully");
    }
  }
  uploadProcess(tryCount:number,part:number,file:any,begin?:boolean,end?:boolean):Promise<boolean>{
    return new Promise<boolean>((resolve,reject)=>{

      this.cloudManagementService.uploadFilePart(this.entryId,part,file,begin,end,begin == true ? this.uploadFile.name : undefined).subscribe({
        next:(response)=>{
          resolve(true);
          return;
        },error:()=>{
          if(tryCount < 3){
            console.log("tryc")
            console.log(tryCount);
            resolve(this.uploadProcess(++tryCount,part,file,begin,end));
          }else{
            resolve(false);

          }
        }
      })
    })
  }

}
