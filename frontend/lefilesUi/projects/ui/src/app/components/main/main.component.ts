import { Component, OnInit } from '@angular/core';
import { ViewComponentModalService } from 'projects/corelib/src/lib/services/componentModal/view-component-modal.service';
import { CreateFolderToCloudComponent } from '../cloud-management/create-folder-to-cloud/create-folder-to-cloud.component';

@Component({
    selector: 'app-main',
    templateUrl: './main.component.html',
    styleUrls: ['./main.component.css'],
    standalone: false
})
export class MainComponent implements OnInit {

  constructor(private componentModal:ViewComponentModalService) { }

  ngOnInit(): void {
  }


}
