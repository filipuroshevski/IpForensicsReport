import { ChangeDetectorRef, Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthenticationService } from '../authentication.service';
import { UtilsService } from '../../shared/common/services/utils.service';
import { LoginModel } from '../../shared/models/login/login.model';
import { SystemErrorModel } from '../../shared/models/system-error/system-error.model';
import { Subject, takeUntil } from 'rxjs';
import { GlobalConstants } from '../../shared/common/global-constants/global-constants';
import { EncryptStorage } from 'storage-encryption';
import { JwtHelperService } from '@auth0/angular-jwt';

const encryptStorage = new EncryptStorage(GlobalConstants.SecretKey, 'sessionStorage');

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {

  //#region Fields
  public errorModel: any;
  public loginModel: LoginModel = new LoginModel;
  public unsubscribe: Subject<void> = new Subject<void>();

  //#endregion

  //#region Constructor
  constructor(public _authenticationService: AuthenticationService,
    private _utilsService: UtilsService,
    private _jwtHelperService: JwtHelperService,
    private _cd: ChangeDetectorRef,
    private _router: Router) {

  }

  ngOnInit(): void {
  }

  ngOnDestroy() {
    this.unsubscribe.next()
    this.unsubscribe.complete();
  }
  //#endregion

  //#region Public Methods

  login(model: LoginModel) {
    this.errorModel = null;
    this._authenticationService.login(model)
      .pipe(takeUntil(this.unsubscribe))
      .subscribe({
        next: (response: any) => {
          encryptStorage.encrypt('access_token', response.token);

          const encryptedToken =
            encryptStorage.storage?.['access_token'];

          localStorage.setItem('access_token', String(encryptedToken));


          this._authenticationService.loginUser.next(true);

          this._router.navigate(['/dashboard']);

        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
          this._cd.detectChanges();
        }
      });

  }


  //#endregion
}
