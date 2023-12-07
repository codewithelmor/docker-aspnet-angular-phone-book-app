import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { SelectListItem } from '../models/select-list-item';

@Injectable({
  providedIn: 'root'
})
export class LabelService {
  
  apiUrl: string = environment.apiUrl;
  baseUrl: string = `${this.apiUrl}/api/v1/label`;

  constructor(
    private http: HttpClient) { }

    dropdown(): Observable<SelectListItem[]> {
      return this.http.get<SelectListItem[]>(`${this.baseUrl}/dropdown`);
    }

}
