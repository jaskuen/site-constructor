import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl: string = "https://localhost:7299/api/SiteDataAPI";

  constructor(private http: HttpClient) {}
  token: string | null = localStorage.getItem("token");
  postData(data: any): Observable<any> {
    console.log(this.token)
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
      'Content-Type': 'application/json',
    });
    return this.http.post<any>(this.apiUrl + "/GetData", data, {headers});
  }
  downloadSite(): Observable<Blob> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
    });
    return this.http.get(this.apiUrl + "/DownloadResultSite", {
      responseType: "blob"
    });
  }
  test(): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${this.token}`,
    })
    return this.http.get(this.apiUrl + "/test", {})
  }
}
