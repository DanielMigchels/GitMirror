import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SettingsResponse } from './models/settings-response.interface';
import { SettingsRequest } from './models/settings-request.interface';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  private readonly apiUrl = '/api/setting';

  constructor(private http: HttpClient) {}

  get(): Observable<SettingsResponse> {
    return this.http.get<SettingsResponse>(this.apiUrl);
  }

  update(request: SettingsRequest): Observable<SettingsResponse> {
    return this.http.put<SettingsResponse>(this.apiUrl, request);
  }

  triggerJob(jobName: string) {
    return this.http.post<SettingsResponse>(`${this.apiUrl}/trigger`, { jobName });
  };
}
