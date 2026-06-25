import { Injectable } from "@angular/core";
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { finalize } from 'rxjs/operators';
import { GlobalConstants } from "../global-constants/global-constants";
import { EncryptStorage } from 'storage-encryption';
import { lastValueFrom } from "rxjs";
import { SpinnerVisibilityService } from "ng-http-loader";


const encryptStorage = new EncryptStorage(GlobalConstants.SecretKey, 'sessionStorage');
@Injectable()
export class HttpService {

  //#region Fields
  //#endregion

  //#region Constructor
  constructor(private _httpClient: HttpClient,
    private _loaderService: SpinnerVisibilityService) {
  }
  //#endregion

  //#region Public Methods
  public httpPost(url: string, model: any) {
    this._loaderService.show();
    url = GlobalConstants.Host + url;
    return this._httpClient.post<any>(`${url}`, model, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }).set('Authorization', `Bearer ${encryptStorage.decrypt('access_token')}`)
    })
      .pipe(finalize(() => this._loaderService.hide()));
  }

  public httpGet(url: string) {
    this._loaderService.show();
    url = GlobalConstants.Host + url;
    return this._httpClient.get<any>(url, {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
      }).set('Authorization', `Bearer ${encryptStorage.decrypt('access_token')}`)
    })
      .pipe(finalize(() => this._loaderService.hide()));
  }

  //#endregion

}
