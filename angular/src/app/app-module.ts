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

@NgModule({
  declarations: [App, Login, Register],
  imports: [BrowserModule, AppRoutingModule, FormsModule],
  providers: [
    provideBrowserGlobalErrorListeners(),
    provideHttpClient(),
    AuthenticationService,
    HttpService
  ],
  bootstrap: [App],
})
export class AppModule {}
