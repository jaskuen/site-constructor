import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {Observable} from "rxjs";

@Injectable({
  providedIn: 'root'
})
export class DataService {
  private apiUrl: string = "http://localhost:5201/api";

  constructor(private http: HttpClient) {}

  postData(data: any): Observable<any> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json'});
    return this.http.post<any>(this.apiUrl + "/SiteConstructor", data, {headers});
  }
}
