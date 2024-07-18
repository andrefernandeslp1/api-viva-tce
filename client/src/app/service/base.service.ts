// base.service.ts
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export abstract class BaseService<T> {
  protected httpClient: HttpClient;
  protected apiUrl: string;

  constructor(httpClient: HttpClient, apiUrl: string) {
    this.httpClient = httpClient;
    this.apiUrl = apiUrl;
  }

  list(): Observable<T[]> {
    return this.httpClient.get<T[]>(this.apiUrl);
  }

  getOne(id: number): Observable<T> {
    return this.httpClient.get<T>(`${this.apiUrl}/${id}`);
  }

  create(item: T): Observable<T> {
    return this.httpClient.post<T>(this.apiUrl, item);
  }

  update(id: number, item: T): Observable<T> {
    return this.httpClient.put<T>(`${this.apiUrl}/${id}`, item);
  }

  delete(id: number): Observable<any> {
    return this.httpClient.delete(`${this.apiUrl}/${id}`);
  }
}
