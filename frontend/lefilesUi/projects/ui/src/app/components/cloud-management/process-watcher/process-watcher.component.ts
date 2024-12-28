import { Component, OnDestroy, OnInit } from '@angular/core';
import { FileProgressItem } from '../../../models/cloud-management/fileProgressItem';
import { WatchService } from '../../../services/watch.service';
import { Subscription } from 'rxjs';
declare var window:any;
@Component({
  selector: 'app-process-watcher',
  templateUrl: './process-watcher.component.html',
  styleUrls: ['./process-watcher.component.css']
})
export class ProcessWatcherComponent implements OnInit,OnDestroy {
  fileProgress:FileProgressItem[] = window["file_progress"];
  uploadSubscription:Subscription;
  downloadSubscription:Subscription;
  constructor(private watchService:WatchService) { }
  ngOnDestroy(): void {
    if(this.uploadSubscription){
      this.uploadSubscription.unsubscribe();
    }
    if(this.downloadSubscription){
      this.downloadSubscription.unsubscribe();
    }
  }

  ngOnInit(): void {
    this.uploadSubscription =  this.watchService.getUploadState().subscribe({
      next:(val)=>{
        this.fileProgress = window["file_progress"];
      }
    })
    this.downloadSubscription =  this.watchService.getDownloadState().subscribe({
      next:(val)=>{
        this.fileProgress = window["file_progress"];
      }
    })

  }

}
