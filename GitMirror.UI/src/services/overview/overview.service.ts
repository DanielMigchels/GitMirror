import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { OverviewResponse } from './models/overview-response.interface';

@Injectable({
  providedIn: 'root',
})
export class OverviewService {
  private readonly apiUrl = '/api/overview';

  constructor(private http: HttpClient) {}

  get(): Observable<OverviewResponse> {
    return this.http.get<OverviewResponse>(this.apiUrl);
  }
}
