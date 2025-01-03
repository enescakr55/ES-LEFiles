import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-preview-file',
  templateUrl: './preview-file.component.html',
  styleUrls: ['./preview-file.component.css']
})
export class PreviewFileComponent implements OnInit {
  @Input() fileId:string;
  imagePreview:SafeResourceUrl;
  constructor(private cloudManagementService:CloudManagementService,private activatedRoute:ActivatedRoute,private domSanitizer:DomSanitizer) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next:(param)=>{
        if(this.fileId == null){
          this.fileId = param["id"];
        }

        this.cloudManagementService.getImagePreview(this.fileId).subscribe({
          next:(data)=>{
            var img = URL.createObjectURL(data);
            this.imagePreview = this.domSanitizer.bypassSecurityTrustResourceUrl(img);
          }
        })
      }
    })
  }

}
