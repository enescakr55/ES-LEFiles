import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterNewDeviceRequest } from '../../models/device-management/registerNewDeviceRequest';
import { environment } from 'projects/ui/src/environments/environment.prod';
import { ResponseModel } from 'projects/corelib/src/lib/models/results/responseModel';

@Injectable({
  providedIn: 'root'
})
export class DeviceManagementService {

  constructor(private httpClient:HttpClient) { }
  registerNewDeviceRequest(request:RegisterNewDeviceRequest){
    var apiUrl = environment.apiUrl;
    return this.httpClient.post<ResponseModel>(`${apiUrl}clients/client-registration/new`,request);
  }
}
