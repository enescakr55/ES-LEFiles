import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginPageComponent } from 'projects/corelib/src/lib/components/login-page/login-page.component';
import { RegisterPageComponent } from 'projects/corelib/src/lib/components/register-page/register-page.component';
import { AuthGuard } from 'projects/corelib/src/lib/guards/auth.guard';
import { NotAuthGuard } from 'projects/corelib/src/lib/guards/not-auth.guard';


const routes: Routes = [
  {path:"",component:LoginPageComponent},
  {path:"login",component:LoginPageComponent,pathMatch:"full",canActivate:[NotAuthGuard]},
  {path:"register",component:RegisterPageComponent,pathMatch:"full",canActivate:[NotAuthGuard]},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
