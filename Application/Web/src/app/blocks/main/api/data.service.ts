import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl: string = "https://localhost:7299/api/SiteDataAPI";

  constructor(private http: HttpClient) {}
  postData(data: any): Observable<any> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post<any>(this.apiUrl + "/GetData", data, {withCredentials: true, headers});
  }
  downloadSite(): Observable<Blob> {
    return this.http.get(this.apiUrl + "/DownloadResultSite", {
      responseType: "blob",
      withCredentials: true,
    });
  }
  test(): Observable<any> {
    return this.http.get(this.apiUrl + "/test")
  }
}
