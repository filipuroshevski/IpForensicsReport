import { ChangeDetectorRef, Component } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { RegisterModel } from '../../shared/models/register/register.model';
import { AuthenticationService } from '../authentication.service';
import { UtilsService } from '../../shared/common/services/utils.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.html',
  styleUrl: './register.css',
})
export class Register {

  //#region Fields

  public errorModel: any;
  public unsubscribe: Subject<void> = new Subject<void>();
  public registerModel: RegisterModel = new RegisterModel();
  public successMessage: string | null = null;
  //#endregion

  //#region Constructor
  constructor(
    private _authenticationService: AuthenticationService,
    public _utilsService: UtilsService,
    private _cd: ChangeDetectorRef,
    private _router: Router) { }

  ngOnInit(): void {

  }

  ngOnDestroy() {
    this.unsubscribe.next()
    this.unsubscribe.complete();
  }
  //#endregion

  //#region Public Methods

  registerUser(model: RegisterModel) {
    this.errorModel = null;
    this._authenticationService.registerUser(model)
      .pipe(takeUntil(this.unsubscribe))
      .subscribe({
        next: (response) => {
          this.successMessage = 'Registration completed successfully. Redirecting to login...';
          this._cd.detectChanges();
          setTimeout(() => {
            this._router.navigate(['/login']);
          }, 2000);
        },
        error: errorResponse => {
          debugger
          this.errorModel = this._utilsService.parseErrors(errorResponse);
          this._cd.detectChanges();
        }
      });
  };

}
