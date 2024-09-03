import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";
import {CookieService} from "ngx-cookie-service";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _apiUrl: string = "https://localhost:7299/api/UserAuth";

  constructor(private http: HttpClient, private cookieService: CookieService) {}

  public login(data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this._apiUrl + "/login", data, {withCredentials: true, headers});
  }

  public register(data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this._apiUrl + "/register", data, {headers});
  }

  public logout() {
    this.cookieService.delete("tasty-cookies", "/");
  }
}
