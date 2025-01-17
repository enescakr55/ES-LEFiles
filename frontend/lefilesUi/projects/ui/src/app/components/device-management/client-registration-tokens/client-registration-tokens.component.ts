import { Component, EventEmitter, OnInit, Output } from '@angular/core';
import { DeviceManagementService } from '../../../services/device-management/device-management.service';
import { ClientRegistrationTokenItemModel } from '../../../models/device-management/registrationTokenItem';

@Component({
    selector: 'app-client-registration-tokens',
    templateUrl: './client-registration-tokens.component.html',
    styleUrls: ['./client-registration-tokens.component.css'],
    standalone: false
})
export class ClientRegistrationTokensComponent implements OnInit {
  registrationTokenItems:ClientRegistrationTokenItemModel[];
  @Output() refreshOut = new EventEmitter<any>(null);
  constructor(private deviceManagementService:DeviceManagementService) { }

  ngOnInit(): void {
    this.refresh();
  }
  public refresh(){
    this.deviceManagementService.getRegistrationTokens().subscribe({
      next:(response)=>{
        this.registrationTokenItems = response.data;
      },error:(err)=>{
        
      }
    })
  }

}
