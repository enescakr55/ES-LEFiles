import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { LoginRequest } from 'projects/corelib/src/lib/models/auth/loginRequest';
import { LoginResponse } from 'projects/corelib/src/lib/models/auth/loginResponse';
import { RegisterRequest } from 'projects/corelib/src/lib/models/auth/registerRequest';
import { DataResponseModel, ResponseModel } from 'projects/corelib/src/lib/models/results/responseModel';
import { AuthBaseService } from 'projects/corelib/src/lib/services/auth/auth-base.service';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService implements AuthBaseService {

  constructor(private httpClient:HttpClient) { }
  login(req: LoginRequest): Observable<DataResponseModel<LoginResponse>> {
    var requestUrl = `${environment.apiUrl}auth/login`;
    return this.httpClient.post<DataResponseModel<LoginResponse>>(requestUrl,req);
  }
  register(req: RegisterRequest): Observable<ResponseModel> {
    throw new Error('Method not implemented.');
  }
}
