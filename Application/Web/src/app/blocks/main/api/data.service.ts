import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {ApiResponse, DownloadSiteRequest, GetSiteDataRequest, SiteConstructorData} from "../../../../types";

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl: string = "https://localhost:7299/api/SiteDataAPI";

  constructor(private http: HttpClient) {}
  postData(data: GetSiteDataRequest): Observable<SiteConstructorData> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post<SiteConstructorData>(this.apiUrl + "/GetData", data, {withCredentials: true, headers});
  }
  downloadSite(data: DownloadSiteRequest): Observable<Blob> {
    let params = new HttpParams();
    params = params.set("userId", data.userId);
    params = params.set("fileName", data.fileName);
    return this.http.get(this.apiUrl + "/DownloadResultSite", {
      params,
      responseType: "blob",
      withCredentials: true,
    });
  }
}
