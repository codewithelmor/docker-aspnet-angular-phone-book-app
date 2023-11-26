import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { SelectListItem } from '../models/select-list-item';

@Injectable({
  providedIn: 'root'
})
export class LabelService {
  
  baseUrl: string = '/api/label';

  constructor(
    private http: HttpClient) { }

    dropdown(): Observable<SelectListItem[]> {
      return this.http.get<SelectListItem[]>(`${this.baseUrl}/dropdown`);
    }

}
