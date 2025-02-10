import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from 'projects/corelib/src/lib/components/login-page/login-page.component';
import { RegisterPageComponent } from 'projects/corelib/src/lib/components/register-page/register-page.component';
import { AuthGuard } from 'projects/corelib/src/lib/guards/auth.guard';
import { NotAuthGuard } from 'projects/corelib/src/lib/guards/not-auth.guard';
import { MainComponent } from './components/main/main.component';
import { DeviceManagementService } from './services/device-management/device-management.service';
import { ManageDevicesComponent } from './components/device-management/manage-devices/manage-devices.component';
import { CloudManagementComponent } from './components/cloud-management/cloud-management.component';
import { PreviewFileComponent } from './components/cloud-management/preview-file/preview-file.component';
import { ViewSharedItemDetailsComponent } from './components/cloud-management/view-shared-item-details/view-shared-item-details.component';


const routes: Routes = [
  {path:"",component:MainComponent,canActivate:[AuthGuard]},
  {path:"main",component:CloudManagementComponent,canActivate:[AuthGuard]},
  {path:"files",component:CloudManagementComponent,canActivate:[AuthGuard]},
  {path:"preview/:id",component:PreviewFileComponent,canActivate:[AuthGuard]},
  {path:"devices",component:ManageDevicesComponent,pathMatch:"full",canActivate:[AuthGuard]},
  {path:"login",component:LoginPageComponent,pathMatch:"full",canActivate:[NotAuthGuard]},
  {path:"register",component:RegisterPageComponent,pathMatch:"full",canActivate:[NotAuthGuard]},
  {path:"shared/:key/info",component:ViewSharedItemDetailsComponent,pathMatch:"full"}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
