import { Component, Input, OnInit } from '@angular/core';
import { CloudManagementService } from '../../../services/cloud-management/cloud-management.service';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeResourceUrl } from '@angular/platform-browser';

@Component({
    selector: 'app-preview-file',
    templateUrl: './preview-file.component.html',
    styleUrls: ['./preview-file.component.css'],
    standalone: false
})
export class PreviewFileComponent implements OnInit {
  @Input() fileId:string;
  type:string;
  fileResource:SafeResourceUrl;
  @Input() elStyle:string;
  loading:boolean;
  constructor(private cloudManagementService:CloudManagementService,private activatedRoute:ActivatedRoute,private domSanitizer:DomSanitizer) { }

  ngOnInit(): void {
    this.activatedRoute.params.subscribe({
      next:(param)=>{
        if(this.fileId == null){
          this.fileId = param["id"];
        }
        this.loading = true;
        this.cloudManagementService.getPreviewData(this.fileId).subscribe({
          next:(data)=>{
            console.log(data.size)
            if(data.type == "image/png"){
              this.type = "image";
            }else if(data.type == "audio/mpeg"){
              this.type = "audio";
            }
            var dataObj = URL.createObjectURL(data);
            this.loading = false;
            console.log(data.type);
            console.log(this.type);

            this.fileResource = this.domSanitizer.bypassSecurityTrustResourceUrl(dataObj);
          },error:(err)=>{
            this.loading = false;
          }
        })
      }
    })
  }

}
