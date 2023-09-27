import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoaderService {
  private loaderSubject = new BehaviorSubject<boolean>(true);

  public loading$: Observable<boolean> = this.loaderSubject.asObservable();

  show() : void {
    this.loaderSubject.next(true)
  }

  hide() : void {
    this.loaderSubject.next(false)
  }
}
