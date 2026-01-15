import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { PaginatedList } from '../pagination/paginated-list.interface';
import { MirrorRequest } from './models/mirror-request.interface';
import { MirrorResponse } from './models/mirror-response.interface';

@Injectable({
  providedIn: 'root',
})
export class MirrorService {
  private readonly apiUrl = '/api/mirror';

  constructor(private http: HttpClient) {}

  get(pageSize: number = 20, page: number = 0): Observable<PaginatedList<MirrorResponse>> {
    const params = new HttpParams()
      .set('pageSize', pageSize.toString())
      .set('page', page.toString());
    
    return this.http.get<PaginatedList<MirrorResponse>>(this.apiUrl, { params });
  }

  getById(id: string): Observable<MirrorResponse> {
    return this.http.get<MirrorResponse>(`${this.apiUrl}/${id}`);
  }

  create(request: MirrorRequest): Observable<MirrorResponse> {
    return this.http.post<MirrorResponse>(this.apiUrl, request);
  }

  update(id: string, request: MirrorRequest): Observable<MirrorResponse> {
    return this.http.put<MirrorResponse>(`${this.apiUrl}/${id}`, request);
  }

  delete(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
