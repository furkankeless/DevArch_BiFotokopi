


// i18 kullanıclak ise aşağıdaki metod aktif edilecek

import { HTTP_INTERCEPTORS, HttpClient, HttpClientModule } from "@angular/common/http";
import { RouterModule } from "@angular/router";
import { ComponentsModule } from "./core/modules/components.module";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { AppRoutingModule } from "./app.routing";

import { CUSTOM_ELEMENTS_SCHEMA, NgModule } from "@angular/core";
import { AppComponent } from "./app.component";
import { AdminLayoutComponent } from "./core/components/app/layouts/admin-layout/admin-layout.component";
import { TranslateModule, TranslateLoader } from "@ngx-translate/core";
import { LoginGuard } from "./core/guards/login-guard";
import { AuthInterceptorService } from "./core/interceptors/auth-interceptor.service";
import { HttpEntityRepositoryService } from "./core/services/http-entity-repository.service";
import { TranslationService } from "./core/services/Translation.service";

//  export function HttpLoaderFactory(http: HttpClient) {
//    
//    var asd=new TranslateHttpLoader(http, '../../../../assets/i18n/', '.json'); 
//    return asd;
//  }


export function tokenGetter() {
  return localStorage.getItem("token");
}


@NgModule({
  imports: [
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ComponentsModule,
    RouterModule,
    AppRoutingModule,
    NgMultiSelectDropDownModule.forRoot(),
    SweetAlert2Module.forRoot(),
    NgModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        //useFactory:HttpLoaderFactory, //i18 kullanılacak ise useClass kapatılıp yukarıda bulunan HttpLoaderFactory ve bu satır aktif edilecek
        useClass: TranslationService,
        deps: [HttpClient]
      }

    }),
   
   
  ],
  declarations: [
    AppComponent,
    AdminLayoutComponent
  ],

  providers: [
    LoginGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptorService,
      multi: true,
    },    
    HttpEntityRepositoryService,
  ],
  bootstrap: [AppComponent],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
})
export class AppModule { }
