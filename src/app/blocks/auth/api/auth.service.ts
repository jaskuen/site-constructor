import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private _apiUrl: string = "https://localhost:7299/api/UserAuth";

  constructor(private http: HttpClient) {}

  public login(data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this._apiUrl + "/login", data, {headers});
  }

  public register(data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this._apiUrl + "/register", data, {headers});
  }
}
