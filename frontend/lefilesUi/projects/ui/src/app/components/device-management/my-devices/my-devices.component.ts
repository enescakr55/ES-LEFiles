import { Component, OnInit } from '@angular/core';
import { DeviceManagementService } from '../../../services/device-management/device-management.service';
import { MyClientItemResponse } from '../../../models/device-management/myClientsItemResponse';

@Component({
    selector: 'app-my-devices',
    templateUrl: './my-devices.component.html',
    styleUrls: ['./my-devices.component.css'],
    standalone: false
})
export class MyDevicesComponent implements OnInit {
  myDevices:MyClientItemResponse[];
  constructor(private deviceManagementService:DeviceManagementService) { }

  ngOnInit(): void {
    this.refresh();
  }
  refresh(){
    this.deviceManagementService.getMyDevices().subscribe({
      next:(response)=>{
        this.myDevices = response.data;
      },error:(err)=>{
        
      }
    })
  }

}
