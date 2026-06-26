import { ChangeDetectorRef, Component } from '@angular/core';
import { ReportBaseModel, ReportViewModel } from '../../shared/models/ip-forensics-report/report-list/report-view.model';
import { IpForensicsReportService } from '../ip-forensics-report.service';
import { UtilsService } from '../../shared/common/services/utils.service';
import { SearchReportModel } from '../../shared/models/ip-forensics-report/report-list/search-report.model';
import { Subject, takeUntil } from 'rxjs';
import { Location } from '@angular/common';

@Component({
  selector: 'app-report-list',
  standalone: false,
  templateUrl: './report-list.html',
  styleUrl: './report-list.css',
})
export class ReportList {
  //#region Fields

  public errorModel: any;
  public unsubscribe: Subject<void> = new Subject<void>();
  public reportViewModel: ReportViewModel = new ReportViewModel();
  public startRow: number = 0;
  public endRow: number = 0;
  //#endregion

  //#region Constructor
  constructor(private _ipForensicsReportService: IpForensicsReportService,
    public _utilsService: UtilsService,
    private _cd: ChangeDetectorRef,
    private _location: Location
  ) {

  }
  ngOnInit(): void {
    this.initializeTableView();
  }

  ngOnDestroy() {
    this.unsubscribe.next()
    this.unsubscribe.complete();
  }
  //#endregion

  //#region Public Methods

  initializeTableView() {
    this.reportViewModel.SearchReportModel = new SearchReportModel();
    this.reportViewModel.SearchReportModel.CurrentPageNumber = 1;
    this.reportViewModel.SearchReportModel.TotalPages = 0;
    this.reportViewModel.SearchReportModel.PageSize = 10
    this.getAllReports(this.reportViewModel.SearchReportModel);
  };
  getAllReports(model: SearchReportModel) {
    this._ipForensicsReportService.getAllReports(model)
      .pipe(takeUntil(this.unsubscribe))
      .subscribe({
        next: (response: any) => {
          this.reportViewModel.ReportBaseModels = response.ReportBaseModels;
          this.reportViewModel.SearchReportModel = response.SearchReportModel;
          this.startRow = this.reportViewModel.SearchReportModel.PageSize * (this.reportViewModel.SearchReportModel.CurrentPageNumber - 1) + 1;
          this.endRow = Math.min(this.startRow + this.reportViewModel.SearchReportModel.PageSize - 1, this.reportViewModel.SearchReportModel.TotalRows);
          this._cd.detectChanges();
        },
        error: errorResponse => {
          this.errorModel = this._utilsService.parseErrors(errorResponse);
        }
      });
  };

  firstPage() {
    if (this.reportViewModel.SearchReportModel.CurrentPageNumber > 1) {
      this.reportViewModel.SearchReportModel.CurrentPageNumber = 1;
    }
    this.getAllReports(this.reportViewModel.SearchReportModel);
  };
  lastPage() {
    if (this.reportViewModel.SearchReportModel.CurrentPageNumber < this.reportViewModel.SearchReportModel.TotalPages) {
      this.reportViewModel.SearchReportModel.CurrentPageNumber = this.reportViewModel.SearchReportModel.TotalPages;
      this.getAllReports(this.reportViewModel.SearchReportModel);
    }
  };
  nextPage() {
    if (this.reportViewModel.SearchReportModel.CurrentPageNumber < this.reportViewModel.SearchReportModel.TotalPages) {
      this.reportViewModel.SearchReportModel.CurrentPageNumber++;
      this.getAllReports(this.reportViewModel.SearchReportModel);
    }
  };
  previousPage() {
    if (this.reportViewModel.SearchReportModel.CurrentPageNumber > 1) {
      this.reportViewModel.SearchReportModel.CurrentPageNumber--;
      this.getAllReports(this.reportViewModel.SearchReportModel);
    }
  };

  goBack() {
    this._location.back();
  }
  //#endregion

}
