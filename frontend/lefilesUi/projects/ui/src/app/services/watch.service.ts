import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { FileProgressItem } from '../models/cloud-management/fileProgressItem';
declare var window:any;
@Injectable({
  providedIn: 'root'
})
export class WatchService {

  constructor() { }
  private uploadState = new BehaviorSubject<boolean|null>(null);
  private downloadState = new BehaviorSubject<boolean|null>(null);
  download$ = this.downloadState.asObservable();
  upload$ = this.uploadState.asObservable();
  downloadStateChanged(){
    this.downloadState.next(!this.downloadState.getValue());
  }
  getDownloadState(){
    return this.download$;
  }
  uploadStateChanged(){
    this.uploadState.next(!this.uploadState.getValue());
  }
  getUploadState(){
    return this.upload$;
  }
  updateFileProgress(fileProgressItem:FileProgressItem){
    if(window["file_progress"] == null){
      window["file_progress"] = [];
    }
    var progressList = window["file_progress"] as FileProgressItem[];
    var index = progressList.findIndex(x=>x.progressId == fileProgressItem.progressId);
    if(index != -1){
      progressList[index].progress = fileProgressItem.progress;
    }else{
      progressList.push(fileProgressItem);
    }
    window["file_progress"] = progressList;
  }
}
