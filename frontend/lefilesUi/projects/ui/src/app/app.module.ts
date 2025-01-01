import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CorelibModule } from 'projects/corelib/src/public-api';
import { AuthBaseService } from 'projects/corelib/src/lib/services/auth/auth-base.service';
import { AuthService } from './services/auth.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { TokenInterceptor } from 'projects/corelib/src/lib/interceptors/token.interceptor';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { TranslateLoader, TranslateModule, TranslateStore } from '@ngx-translate/core';
import { MainComponent } from './components/main/main.component';
import { FileEntryItemComponent } from './components/file-management/file-entry-item/file-entry-item.component';
import { FileSystemComponent } from './components/file-management/file-system/file-system.component';
import { MyProfileComponent } from './components/my-profile/my-profile.component';
import { RegisterNewDeviceComponent } from './components/device-management/register-new-device/register-new-device.component';
import { VerticalMenuComponent } from './components/navigation/vertical-menu/vertical-menu.component';
import { HorizontalMenuComponent } from './components/navigation/horizontal-menu/horizontal-menu.component';
import { ClientRegistrationTokensComponent } from './components/device-management/client-registration-tokens/client-registration-tokens.component';
import { MyDevicesComponent } from './components/device-management/my-devices/my-devices.component';
import { ManageDevicesComponent } from './components/device-management/manage-devices/manage-devices.component';
import { CloudManagementComponent } from './components/cloud-management/cloud-management.component';
import { CreateFolderToCloudComponent } from './components/cloud-management/create-folder-to-cloud/create-folder-to-cloud.component';
import { UpdateFolderFromCloudComponent } from './components/cloud-management/update-folder-from-cloud/update-folder-from-cloud.component';
import { DeleteFolderFromCloudComponent } from './components/cloud-management/delete-folder-from-cloud/delete-folder-from-cloud.component';
import { ViewComponentModalService } from 'projects/corelib/src/lib/services/componentModal/view-component-modal.service';
import { CloudFileUploadComponent } from './components/cloud-management/cloud-file-upload/cloud-file-upload.component';
import { DeleteFileFromCloudComponent } from './components/cloud-management/delete-file-from-cloud/delete-file-from-cloud.component';
import { ProcessWatcherComponent } from './components/cloud-management/process-watcher/process-watcher.component';
import { ViewThumbnailComponent } from './components/cloud-management/view-thumbnail/view-thumbnail.component';
export function HttpLoaderFactory(http:HttpClient){
  return new TranslateHttpLoader(http,"/assets/langs/",".json")
}
@NgModule({
  declarations: [
    AppComponent,
    MainComponent,
    FileEntryItemComponent,
    FileSystemComponent,
    MyProfileComponent,
    RegisterNewDeviceComponent,
    VerticalMenuComponent,
    HorizontalMenuComponent,
    ClientRegistrationTokensComponent,
    MyDevicesComponent,
    ManageDevicesComponent,
    CloudManagementComponent,
    CreateFolderToCloudComponent,
    UpdateFolderFromCloudComponent,
    DeleteFolderFromCloudComponent,
    CloudFileUploadComponent,
    DeleteFileFromCloudComponent,
    ProcessWatcherComponent,
    ViewThumbnailComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    CorelibModule,
    HttpClientModule,
    ReactiveFormsModule,
    BrowserAnimationsModule, // required animations module
    ToastrModule.forRoot({
      positionClass:"toast-bottom-right",
      progressBar:true,
      progressAnimation:'decreasing',
      maxOpened:3,
      
    }), // ToastrModule added
    /*TranslateModule.forRoot({
      defaultLanguage:"tr-TR",
      loader:{
        
        provide:TranslateLoader,
        useFactory:HttpLoaderFactory,
        deps:[HttpClient]
      }
    })*/
  ],
  exports:[

  ],
  providers: [
    {provide:AuthBaseService,useClass:AuthService},
    {provide:HTTP_INTERCEPTORS,useClass:TokenInterceptor,multi:true},
    TranslateModule,
    TranslateStore,
    ViewComponentModalService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
