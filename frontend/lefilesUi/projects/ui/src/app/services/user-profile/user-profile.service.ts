import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { DataResponseModel, ResponseModel } from 'projects/corelib/src/lib/models/results/responseModel';
import { environment } from 'projects/ui/src/environments/environment.prod';
import { ProfileDetailsResponse } from '../../models/user-profile/profileDetailsResponse';
import { ChangePasswordRequest } from '../../models/user-profile/changePasswordRequest';
import { UpdateProfileRequest } from '../../models/user-profile/updateProfileRequest';

@Injectable({
  providedIn: 'root'
})
export class UserProfileService {

  constructor(private httpClient:HttpClient) { }
  getProfileDetails(){
    var apiUrl = environment.apiUrl;
    return this.httpClient.get<DataResponseModel<ProfileDetailsResponse>>(apiUrl+"profile/details");
  }
  changePassword(changePasswordRequest:ChangePasswordRequest){
    var apiUrl = environment.apiUrl;
    return this.httpClient.post<ResponseModel>(apiUrl+"profile/changepassword",changePasswordRequest);
  }
  updateProfile(updateProfileRequest:UpdateProfileRequest){
    var apiUrl = environment.apiUrl;
    return this.httpClient.post<ResponseModel>(apiUrl+"profile/update",updateProfileRequest);
  }
}
