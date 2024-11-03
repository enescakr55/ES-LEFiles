import { NgModule } from '@angular/core';
import { CorelibComponent } from './corelib.component';

import { HttpClientModule } from '@angular/common/http';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ReactiveFormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    CorelibComponent,
    LoginPageComponent,
    RegisterPageComponent,


  ],
  imports: [
    HttpClientModule,
    ReactiveFormsModule
  ],
  exports: [
    CorelibComponent,
    LoginPageComponent,
    RegisterPageComponent
  ]
})
export class CorelibModule { }
