import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { EncryptStorage } from 'storage-encryption';
import { GlobalConstants } from '../../shared/common/global-constants/global-constants';
import { CanActivate, Router } from '@angular/router';


const encryptStorage = new EncryptStorage(GlobalConstants.SecretKey, 'sessionStorage');
const _jwtHelper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class LoggedInGuard implements CanActivate {

  //#region Constructor
  constructor(private _router: Router) {
  }
  //#endregion

  //#region Public Methods
  canActivate() {
    const token = encryptStorage.decrypt('access_token');
    if (token && !_jwtHelper.isTokenExpired(token)) {
      this._router.navigate(['/dashboard']);
      return false;
    }

    return true;
  }
  //#endregion
}
