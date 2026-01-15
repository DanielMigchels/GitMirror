import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedList } from '../pagination/paginated-list.interface';
import { PlatformRequest } from './models/platform-request.interface';
import { PlatformResponse } from './models/platform-response.interface';

@Injectable({
  providedIn: 'root',
})
export class PlatformService {
  private readonly apiUrl = '/api/platform';

  constructor(private http: HttpClient) {}

  get(pageSize: number = 20, page: number = 0): Observable<PaginatedList<PlatformResponse>> {
    const params = new HttpParams()
      .set('pageSize', pageSize.toString())
      .set('page', page.toString());
    
    return this.http.get<PaginatedList<PlatformResponse>>(this.apiUrl, { params });
  }

  getById(id: string): Observable<PlatformResponse> {
    return this.http.get<PlatformResponse>(`${this.apiUrl}/${id}`);
  }

  create(request: PlatformRequest): Observable<PlatformResponse> {
    return this.http.post<PlatformResponse>(this.apiUrl, request);
  }

  update(id: string, request: PlatformRequest): Observable<PlatformResponse> {
    return this.http.put<PlatformResponse>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
