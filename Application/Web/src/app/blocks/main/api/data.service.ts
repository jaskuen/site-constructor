import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {
  SiteConstructorData, UserSiteData
} from "../../../../types";
import {
  ApiResponse, CheckHostNameRequest, CheckHostNameResult,
  DownloadSiteRequest,
  GetSavedUserSiteDataRequest, GetSavedUserSiteDataResponse,
  GetSiteDataRequest, HostSiteRequest,
  SaveUserSiteDataRequest
} from "./DTOs";

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
    return this.http.post<SiteConstructorData>(this.apiUrl + "/post", data, {withCredentials: true, headers});
  }
  saveUserData(data: SaveUserSiteDataRequest): Observable<SaveUserSiteDataRequest> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    })
    return this.http.post<SaveUserSiteDataRequest>(this.apiUrl + "/save", data, {withCredentials: true, headers});
  }
  getSavedUserData(data: GetSavedUserSiteDataRequest): Observable<ApiResponse<GetSavedUserSiteDataResponse>> {
    let params = new HttpParams();
    params = params.set("userId", data.userId)
    return this.http.get<ApiResponse<GetSavedUserSiteDataResponse>>(this.apiUrl + "/load", {
      params,
      withCredentials: true,
      responseType: "json",
    })
  }
  downloadSite(data: DownloadSiteRequest): Observable<Blob> {
    let params = new HttpParams();
    params = params.set("userId", data.userId);
    params = params.set("fileName", data.fileName);
    return this.http.get(this.apiUrl + "/download", {
      params,
      responseType: "blob",
      withCredentials: true,
    });
  }
  hostSite(data: HostSiteRequest): Observable<string> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    });
    return this.http.post<string>(this.apiUrl + "/host", data, {withCredentials: true, headers});
  }
  isHostNameAvailable(data: CheckHostNameRequest): Observable<ApiResponse<CheckHostNameResult>> {
    let params = new HttpParams();
    params = params.set("siteHostName", data.siteHostName);
    return this.http.get<ApiResponse<CheckHostNameResult>>(this.apiUrl + "/checkHost", {
      params,
      responseType: "json",
      withCredentials: true,
    });
  }
}
