import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {SiteConstructorData} from "../../../../types";

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl: string = "https://localhost:7299/api/SiteDataAPI";

  constructor(private http: HttpClient) {}
  postData(data: SiteConstructorData): Observable<SiteConstructorData> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post<SiteConstructorData>(this.apiUrl + "/GetData", data, {withCredentials: true, headers});
  }
  downloadSite(userId: string): Observable<Blob> {
    const params = new HttpParams().set("userId", userId);
    return this.http.get(this.apiUrl + "/DownloadResultSite", {
      params,
      responseType: "blob",
      withCredentials: true,
    });
  }
  test(): Observable<any> {
    return this.http.get(this.apiUrl + "/test")
  }
}
