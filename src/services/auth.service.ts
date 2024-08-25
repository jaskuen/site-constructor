import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl: string = "https://localhost:7299/api/UserAuth";

  constructor(private http: HttpClient) {}

  login(data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this.apiUrl + "/login", data, {headers});
  }

  register(data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this.apiUrl + "/register", data, {headers});
  }
}
