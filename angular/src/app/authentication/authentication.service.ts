import { Injectable } from "@angular/core";
import { RegisterModel } from "../shared/models/register/register.model";
import { HttpService } from "../shared/common/services/http.service";
import { LoginModel } from "../shared/models/login/login.model";
import { BehaviorSubject, Subject } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

   loginUser = new Subject<boolean>();

  constructor(private _httpService: HttpService) {}

  registerUser(model: RegisterModel) {
    const url = "Authentication/RegisterUser";
    return this._httpService.httpPost(url, model);
  }

  login(model: LoginModel) {
    const url = "Authentication/Login";
    return this._httpService.httpPost(url, model);
  }
}