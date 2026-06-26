import { HttpErrorResponse } from "@angular/common/http";
import { EventEmitter, Injectable } from "@angular/core";
import { Subject } from "rxjs";
import { SystemErrorModel } from "../../models/system-error/system-error.model";
import { Router } from "@angular/router";

@Injectable({
  providedIn: 'root'
})
export class UtilsService {

  private systemErrorSubject = new Subject<SystemErrorModel>();
  systemErrorModel$ = this.systemErrorSubject.asObservable();
  systemErrorModel = new EventEmitter<SystemErrorModel>();

  constructor(private _router: Router) {

  }

  public parseErrors(errorResponse: HttpErrorResponse) {
    if (errorResponse != null && errorResponse.status == 404) {
      this._router.navigateByUrl('page-not-found');
    }
    if (errorResponse != null && errorResponse.status != 0) {

      var errorsToModel = JSON.parse(errorResponse.error.Value);
      var validationErrors = this.validationErrors(errorsToModel);

      if (validationErrors["SystemErrorOccured"] != undefined && validationErrors["SystemErrorOccured"] != null) {
        var systemErrorModel = new SystemErrorModel();
        systemErrorModel.hasErrorOccured = true;
        systemErrorModel.ErrorMessage = validationErrors["SystemErrorOccured"];
        this.systemErrorModel.emit(systemErrorModel);
      }

      return validationErrors;

    } else {
      return null;
    }
  }

  private validationErrors(errors: any[]) {

    const model: { [key: string]: any } = {};

    if (!Array.isArray(errors)) return model;

    errors.forEach(error => {
      model[error.Key] = error.Message;
    });

    return model;
  }
}