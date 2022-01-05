import { Injectable } from '@angular/core';
import { MessageService } from './message.service';
import { Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ErrorHandlerService {
  constructor(private messageService: MessageService) {}

  /**
   * Handle Http operation that failed.
   * Let the app continue.
   * @param operation - name of the operation that failed
   * @param result - optional value to return as the observable result
   */
  handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send the error to remote logging infrastructure
      console.error(error); // log to console instead

      // TODO: better job of transforming error for user consumption
      switch (error.status) {
        case 0:
          this.messageService.add(
            'Network Error! Service is not available at the moment!'
          );
          break;

        case 400:
          this.messageService.add(
            'Bad Request! Please fill in all the fields!'
          );
          break;

        case 404:
          this.messageService.add(
            'Not Found! Resource you requested is missing!'
          );
          break;

        case 500:
          this.messageService.add('Server Error! Oops my bad :)');
          break;

        default:
          this.messageService.add('Unknown Error! Check the console!');
          break;
      }

      // Let the app keep running by returning an empty result.
      return of(result as T);
    };
  }
}
