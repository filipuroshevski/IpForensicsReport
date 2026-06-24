import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";


@Injectable()
export class HttpService {

  private readonly baseUrl = 'http://localhost:5126/api';

  constructor(private http: HttpClient) {}

  public httpPost(url: string, model: any) {
    return this.http.post(`${this.baseUrl}/${url}`, model);
  }

  public httpGet(url: string) {
    return this.http.get(`${this.baseUrl}/${url}`);
  }
}