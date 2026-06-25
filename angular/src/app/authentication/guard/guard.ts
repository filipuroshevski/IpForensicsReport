import { Component, Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { EncryptStorage } from 'storage-encryption';
import { GlobalConstants } from '../../shared/common/global-constants/global-constants';
import { CanActivate, Router } from '@angular/router';


const encryptStorage = new EncryptStorage(GlobalConstants.SecretKey, 'sessionStorage');
const _jwtHelper = new JwtHelperService();

@Injectable({
  providedIn: 'root'
})
export class Guard implements CanActivate {

  //#region Constructor
  constructor(private _router: Router) {
  }
  //#endregion

  //#Private Methods
  canActivate() {
    if (encryptStorage.decrypt('access_token') && !_jwtHelper.isTokenExpired(encryptStorage.decrypt('access_token')!)) {
      return true;

    } else {
      this._router.navigate(['/login']);
      return false;
    }

  }
  //#endregion
}