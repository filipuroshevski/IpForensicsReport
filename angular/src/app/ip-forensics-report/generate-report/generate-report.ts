import { ChangeDetectorRef, Component } from '@angular/core';
import { Subject, takeUntil } from 'rxjs';
import { GenerateReportModel } from '../../shared/models/ip-forensics-report/generate-report/generate-report.model';
import { IpForensicsReportService } from '../ip-forensics-report.service';
import { UtilsService } from '../../shared/common/services/utils.service';
import { Location } from '@angular/common';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-generate-report',
  standalone: false,
  templateUrl: './generate-report.html',
  styleUrl: './generate-report.css',
})
export class GenerateReport {

  //#region Fields

  public errorModel: any;
  public unsubscribe: Subject<void> = new Subject<void>();
  public generateReportModel: GenerateReportModel = new GenerateReportModel();
  public successMessage: string | null = null;
  //#endregion

  //#region Constructor
  constructor(private _ipForensicsReportService: IpForensicsReportService,
    public _utilsService: UtilsService,
    private _cd: ChangeDetectorRef,
    private _location: Location,
    private _toasterService: ToastrService
  ) {

  }

  ngOnInit(): void {

  }

  ngOnDestroy() {
    this.unsubscribe.next()
    this.unsubscribe.complete();
  }

  //#endregion

  //#region Public Methods

  generateReport(model: GenerateReportModel) {
    this.errorModel = null;
    this._ipForensicsReportService.generateReport(model)
      .pipe(takeUntil(this.unsubscribe))
      .subscribe({
        next: (response) => {
          this.successMessage = 'The report was generated successfully';
          this.generateReportModel.IpAddress = "";
          this._toasterService.success('The report was generated successfully');
          this._cd.detectChanges();
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
          this._cd.detectChanges();
        }
      });
  };

  goBack() {
    this._location.back();
  }
  //#endregion

}
