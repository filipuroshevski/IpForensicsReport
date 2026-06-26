import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { provideHttpClient } from '@angular/common/http';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import { Login } from './authentication/login/login';
import { Register } from './authentication/register/register';
import { AuthenticationService } from './authentication/authentication.service';
import { HttpService } from './shared/common/services/http.service';
import { JwtModule } from '@auth0/angular-jwt';
import { EncryptStorage } from 'storage-encryption';
import { GlobalConstants } from './shared/common/global-constants/global-constants';
import { Guard } from './authentication/guard/guard';
import { LoggedInGuard } from './authentication/guard/logged-in-guard';
import { Dashboard } from './ip-forensics-report/dashboard/dashboard';
import { NgHttpLoaderComponent } from 'ng-http-loader';
import { GenerateReport } from './ip-forensics-report/generate-report/generate-report';
import { ReportList } from './ip-forensics-report/report-list/report-list';
import { IpForensicsReportService } from './ip-forensics-report/ip-forensics-report.service';
import { ToastrModule } from 'ngx-toastr';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

const encryptStorage = new EncryptStorage(GlobalConstants.SecretKey, 'sessionStorage');

@NgModule({
  declarations: [App, Login, Register, Dashboard, GenerateReport, ReportList],
  imports: [
    BrowserAnimationsModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    NgHttpLoaderComponent,
    JwtModule.forRoot({
      config: {
        tokenGetter: () => {
          return encryptStorage.decrypt('access_token');
        },
      },
    }),
     ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true
    })

  ],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(),
    AuthenticationService,
    HttpService,
    Guard,
    LoggedInGuard,
    IpForensicsReportService
  ],
  bootstrap: [App],
})
export class AppModule { }
