import { Injectable, isDevMode } from '@angular/core';
import { environment as dev } from '../Environments/Environment';
import { environment as prod} from '../Environments/Environment.prod';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

class EnvironmentsService {
  Environment: environmentDto;

  constructor() { 
    this.Environment = isDevMode() ? dev : prod
  }

  getEnvironment() {
    return this.Environment
  }
  

}

interface environmentDto{
  production: boolean,
  BACKEND_URL: string
}


export {EnvironmentsService, environmentDto }