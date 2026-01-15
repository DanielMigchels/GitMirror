import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedList } from '../pagination/paginated-list.interface';
import { RepositoryRequest } from './models/repository-request.interface';
import { RepositoryResponse } from './models/repository-response.interface';

@Injectable({
  providedIn: 'root',
})
export class RepositoryService {
  private readonly apiUrl = '/api/repository';

  constructor(private http: HttpClient) {}

  get(pageSize: number = 20, page: number = 0): Observable<PaginatedList<RepositoryResponse>> {
    const params = new HttpParams()
      .set('pageSize', pageSize.toString())
      .set('page', page.toString());
    
    return this.http.get<PaginatedList<RepositoryResponse>>(this.apiUrl, { params });
  }

  getById(id: string): Observable<RepositoryResponse> {
    return this.http.get<RepositoryResponse>(`${this.apiUrl}/${id}`);
  }

  create(request: RepositoryRequest): Observable<RepositoryResponse> {
    return this.http.post<RepositoryResponse>(this.apiUrl, request);
  }

  update(id: string, request: RepositoryRequest): Observable<RepositoryResponse> {
    return this.http.put<RepositoryResponse>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
