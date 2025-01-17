import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
    selector: 'app-view-thumbnail',
    templateUrl: './view-thumbnail.component.html',
    styleUrls: ['./view-thumbnail.component.css'],
    standalone: false
})
export class ViewThumbnailComponent implements OnInit {
  @Input() fileItemId:string;
  @Input() imgStyle:string;
  img:SafeResourceUrl;
  constructor(private cloudManagementService:CloudManagementService,private domSanitizer:DomSanitizer) { }

  ngOnInit(): void {
    this.getImage();
  }
  getImage(){
    this.cloudManagementService.getThumbnail(this.fileItemId).subscribe({
      next:(data)=>{
        var objUrl = URL.createObjectURL(data);
        this.img = this.domSanitizer.bypassSecurityTrustResourceUrl(objUrl);
      },error:(err)=>{

      }
    })
  }

}
