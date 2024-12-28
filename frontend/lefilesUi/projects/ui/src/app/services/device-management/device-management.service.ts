import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { RegisterNewDeviceRequest } from '../../models/device-management/registerNewDeviceRequest';
import { environment } from 'projects/ui/src/environments/environment.prod';
import { DataResponseModel, ResponseModel } from 'projects/corelib/src/lib/models/results/responseModel';
import { ClientRegistrationTokenItemModel } from '../../models/device-management/registrationTokenItem';
import { MyClientItemResponse } from '../../models/device-management/myClientsItemResponse';

@Injectable({
  providedIn: 'root'
})
export class DeviceManagementService {

  constructor(private httpClient:HttpClient) { }
  registerNewDeviceRequest(request:RegisterNewDeviceRequest){
    var apiUrl = environment.apiUrl;
    return this.httpClient.post<ResponseModel>(`${apiUrl}clients/client-registration/new`,request);
  }
  getRegistrationTokens(){
    var apiUrl = environment.apiUrl;
    return this.httpClient.get<DataResponseModel<ClientRegistrationTokenItemModel[]>>(`${apiUrl}clients/registration-tokens/list`);
  }
  getMyDevices(){
    var apiUrl = environment.apiUrl;
    return this.httpClient.get<DataResponseModel<MyClientItemResponse[]>>(`${apiUrl}clients/my-devices`);
  }
}
