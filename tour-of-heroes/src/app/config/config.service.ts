import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

import { Config } from './config';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  options = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json'
    }),
    params: new HttpParams(),
    // Add other defaults as needed
    // responseType: 'json',
    // observe: 'body',
  };

  configUrl = 'assets/config.json';

  constructor(private http: HttpClient) { }

  getConfig() {
    return this.http.get<Config>(this.configUrl);
  }

  getConfigResponse(): Observable<HttpResponse<Config>> {
    return this.http.get<Config>(
      this.configUrl, { observe: 'response' });
  }

  // Error handling function
  private handleError(error: any) {
    // In a real-world app, you may send the error to some remote logging infrastructure
    // For now, let's log it to the console
    console.error(error);
    return throwError(error);
  }
}
