import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Login } from './authentication/login/login';
import { Register } from './authentication/register/register';
import { Dashboard } from './ip-forensics-report/dashboard/dashboard';
import { Guard } from './authentication/guard/guard';
import { LoggedInGuard } from './authentication/guard/logged-in-guard';

const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: Login, canActivate: [LoggedInGuard] },
  { path: 'register', component: Register, canActivate: [LoggedInGuard] },
   { path: 'dashboard', component: Dashboard, canActivate: [Guard] },
  { path: '**', redirectTo: 'login' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
