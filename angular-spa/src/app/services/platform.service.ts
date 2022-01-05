import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { ErrorHandlerService } from './error-handler.service';
import { Platform } from '../models/platform';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class PlatformService {
  constructor(
    private http: HttpClient,
    private errorHandler: ErrorHandlerService
  ) {}

  private url = environment.platformsUrl;

  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' }),
  };

  getPlatforms(): Observable<Platform[]> {
    return this.http
      .get<Platform[]>(this.url)
      .pipe(
        catchError(
          this.errorHandler.handleError<Platform[]>('getPlatforms', [])
        )
      );
  }

  getPlatform(id: number): Observable<Platform> {
    const url = `${this.url}/${id}`;
    return this.http
      .get<Platform>(url)
      .pipe(
        catchError(
          this.errorHandler.handleError<Platform>(`getPlatform id=${id}`)
        )
      );
  }

  addPlatform(platform: Platform): Observable<Platform> {
    return this.http
      .post<Platform>(this.url, platform, this.httpOptions)
      .pipe(catchError(this.errorHandler.handleError<Platform>('addPlatform')));
  }

  deletePlatform(id: number): Observable<Platform> {
    const url = `${this.url}/${id}`;

    return this.http
      .delete<Platform>(url, this.httpOptions)
      .pipe(
        catchError(this.errorHandler.handleError<Platform>('deletePlatform'))
      );
  }
}
