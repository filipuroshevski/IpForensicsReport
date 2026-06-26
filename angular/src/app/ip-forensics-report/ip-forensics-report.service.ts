import { Injectable } from "@angular/core";
import { HttpService } from "../shared/common/services/http.service";
import { GenerateReportModel } from "../shared/models/ip-forensics-report/generate-report/generate-report.model";
import { SearchReportModel } from "../shared/models/ip-forensics-report/report-list/search-report.model";
import { map } from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class IpForensicsReportService {


  constructor(private _httpService: HttpService) { }

  generateReport(model: GenerateReportModel) {
    const url = "IpForensicsReport/GenerateReport";
    return this._httpService.httpPost(url, model);
  }

  getAllReports(model: SearchReportModel) {
    var url = "IpForensicsReport/GetAllReports";
    return this._httpService.httpPost(url, model)
      .pipe(map(resposne => {
        return resposne;
      }));
  };
}