import { NgModule } from '@angular/core';
import { CorelibComponent } from './corelib.component';

import { HttpClient, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { LoginPageComponent } from './components/login-page/login-page.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TranslateLoader, TranslateModule, TranslatePipe, TranslateService, TranslateStore } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { CommonModule } from '@angular/common';
import { ComponentModalComponent } from './components/component-modal/component-modal.component';
import { FooterComponent } from './components/footer/footer.component';


export function HttpLoaderFactory(http:HttpClient){
  return new TranslateHttpLoader(http,"/assets/langs/",".json")
}
@NgModule({ declarations: [
        CorelibComponent,
        LoginPageComponent,
        RegisterPageComponent,
        ComponentModalComponent,
        FooterComponent
    ],
    exports: [
        CorelibComponent,
        LoginPageComponent,
        RegisterPageComponent,
        FooterComponent,
        TranslateModule,
        TranslatePipe
    ], imports: [ReactiveFormsModule,
        CommonModule,
        TranslateModule.forRoot({
            defaultLanguage: "tr-TR",
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient]
            }
        })], providers: [provideHttpClient(withInterceptorsFromDi())] })
export class CorelibModule { }
