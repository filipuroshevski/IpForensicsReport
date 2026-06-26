import { SearchReportModel } from "./search-report.model";

export class ReportViewModel{
    public SearchReportModel: SearchReportModel = new SearchReportModel();
    public ReportBaseModels: Array<ReportBaseModel> = new Array<ReportBaseModel>();
}

export class ReportBaseModel{
    public Id:number=0;
    public IpAddress:string="";
    public AbuseConfidenceScore:string="";
    public TotalReports:string="";
    public LastReportedDate:string="";
    public Continent:string="";
    public Country:string="";
    public Region:string="";
    public City:string="";
    public Mobile:string="";
    public Proxy:string="";
    public Hosting:string="";
    public Tor:string="";
     public CreatedDate:string="";

}