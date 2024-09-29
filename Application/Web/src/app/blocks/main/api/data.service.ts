import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders, HttpParams} from "@angular/common/http";
import {Observable} from "rxjs";
import {
  ApiResponse,
  DownloadSiteRequest, GetSavedUserSiteDataRequest, GetSavedUserSiteDataResponse,
  GetSiteDataRequest,
  SaveUserSiteDataRequest,
  SiteConstructorData, UserSiteData
} from "../../../../types";

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
    return this.http.post<SiteConstructorData>(this.apiUrl + "/PostResultSiteData", data, {withCredentials: true, headers});
  }
  saveUserData(data: SaveUserSiteDataRequest): Observable<SaveUserSiteDataRequest> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
    })
    return this.http.post<SaveUserSiteDataRequest>(this.apiUrl + "/SaveUserSiteData", data, {withCredentials: true, headers});
  }
  getSavedUserData(data: GetSavedUserSiteDataRequest): Observable<ApiResponse<GetSavedUserSiteDataResponse>> {
    let params = new HttpParams();
    params = params.set("userId", data.userId)
    return this.http.get<ApiResponse<GetSavedUserSiteDataResponse>>(this.apiUrl + "/GetSavedUserSiteData", {
      params,
      withCredentials: true,
      responseType: "json",
    })
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
