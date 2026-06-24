import { Injectable } from "@angular/core";
import { RegisterModel } from "../shared/models/register/register.model";
import { HttpService } from "../shared/common/services/http.service";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private httpService: HttpService) {}

  registerUser(model: RegisterModel) {
    const url = "Authentication/RegisterUser";

    return this.httpService.httpPost(url, model);
  }
}