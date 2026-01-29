import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { SettingsResponse } from './models/settings-response.interface';

@Injectable({
  providedIn: 'root',
})
export class SettingsService {
  private readonly apiUrl = '/api/setting';

  constructor(private http: HttpClient) {}

  get(): Observable<SettingsResponse> {
    return this.http.get<SettingsResponse>(this.apiUrl);
  }
}
