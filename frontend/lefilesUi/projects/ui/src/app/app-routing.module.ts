import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from 'projects/corelib/src/lib/components/login-page/login-page.component';
import { RegisterPageComponent } from 'projects/corelib/src/lib/components/register-page/register-page.component';
import { AuthGuard } from 'projects/corelib/src/lib/guards/auth.guard';
import { NotAuthGuard } from 'projects/corelib/src/lib/guards/not-auth.guard';
import { MainComponent } from './components/main/main.component';
import { MyDevicesComponent } from './components/my-devices/my-devices.component';


const routes: Routes = [
  {path:"",component:MainComponent,canActivate:[AuthGuard]},
  {path:"main",component:MainComponent,canActivate:[AuthGuard]},
  {path:"devices",component:MyDevicesComponent,pathMatch:"full",canActivate:[AuthGuard]},
  {path:"login",component:LoginPageComponent,pathMatch:"full",canActivate:[NotAuthGuard]},
  {path:"register",component:RegisterPageComponent,pathMatch:"full",canActivate:[NotAuthGuard]},

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
