import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, Observable } from 'rxjs';
import { Command } from '../models/command';
import { Platform } from '../models/platform';
import { ErrorHandlerService } from './error-handler.service';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root',
})
export class CommandService {
  constructor(
    private http: HttpClient,
    private errorHandler: ErrorHandlerService
  ) {}

  private url = environment.commandsUrl;

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

  addCommand(command: Command, platformId: number): Observable<Command> {
    const url = `${this.url}/${platformId}/commands`;
    return this.http
      .post<Command>(url, command, this.httpOptions)
      .pipe(catchError(this.errorHandler.handleError<Command>('addCommand')));
  }

  getCommands(platformId: number): Observable<Command[]> {
    const url = `${this.url}/${platformId}/commands`;
    return this.http
      .get<Command[]>(url)
      .pipe(
        catchError(this.errorHandler.handleError<Command[]>('getCommands', []))
      );
  }

  getCommand(platformId: number, id: number): Observable<Command> {
    const url = `${this.url}/${platformId}/commands/${id}`;
    return this.http
      .get<Command>(url)
      .pipe(catchError(this.errorHandler.handleError<Command>('getCommand')));
  }

  deleteCommand(platformId: number, id: number): Observable<Command> {
    const url = `${this.url}/${platformId}/commands/${id}`;
    return this.http
      .delete<Command>(url)
      .pipe(
        catchError(this.errorHandler.handleError<Command>('deleteCommand'))
      );
  }
}
