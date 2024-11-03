import { Injectable } from '@angular/core';
import { LoginRequest } from '../../models/auth/loginRequest';
import { Observable } from 'rxjs';
import { DataResponseModel, ResponseModel } from '../../models/results/responseModel';
import { LoginResponse } from '../../models/auth/loginResponse';
import { RegisterRequest } from '../../models/auth/registerRequest';

@Injectable({
  providedIn: 'root'
})
export abstract class AuthBaseService {

  constructor() { }
  abstract login(req:LoginRequest):Observable<DataResponseModel<LoginResponse>>
  abstract register(req:RegisterRequest):Observable<ResponseModel>
}
