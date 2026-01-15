import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedList } from '../pagination/paginated-list.interface';
import { HistoryRequest } from './models/history-request.interface';
import { HistoryResponse } from './models/history-response.interface';

@Injectable({
  providedIn: 'root',
})
export class HistoryService {
  private readonly apiUrl = '/api/history';

  constructor(private http: HttpClient) {}

  get(pageSize: number = 20, page: number = 0): Observable<PaginatedList<HistoryResponse>> {
    const params = new HttpParams()
      .set('pageSize', pageSize.toString())
      .set('page', page.toString());
    
    return this.http.get<PaginatedList<HistoryResponse>>(this.apiUrl, { params });
  }

  getById(id: string): Observable<HistoryResponse> {
    return this.http.get<HistoryResponse>(`${this.apiUrl}/${id}`);
  }

  create(request: HistoryRequest): Observable<HistoryResponse> {
    return this.http.post<HistoryResponse>(this.apiUrl, request);
  }

  update(id: string, request: HistoryRequest): Observable<HistoryResponse> {
    return this.http.put<HistoryResponse>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
