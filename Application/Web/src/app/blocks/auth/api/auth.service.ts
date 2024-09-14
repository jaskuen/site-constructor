import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {CookieService} from "ngx-cookie-service";
import {ApiResponse, AuthData, LoginResponse} from "../../../../types";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _apiUrl: string = "https://localhost:7299/api/UserAuth";

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  public login(data: AuthData): Observable<ApiResponse<LoginResponse>> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<ApiResponse<LoginResponse>>(this._apiUrl + "/login", data, {withCredentials: true, headers});
  }

  public logout() {
    this.cookieService.delete("tasty-cookies", "/");
    localStorage.removeItem("userId");
  }

  public checkLogin(login: string): Observable<boolean> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<boolean>(this._apiUrl + "/checkLogin", { login }, {withCredentials: true, headers});
  }

  public register(data: AuthData): Observable<AuthData> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<AuthData>(this._apiUrl + "/register", data, {headers});
  }


}