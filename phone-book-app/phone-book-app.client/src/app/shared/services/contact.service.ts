import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ContactInputModel } from '../models/input-models/contact.input-model';
import { ContactViewModel } from '../models/view-models/contact.view-model';

@Injectable({
  providedIn: 'root'
})
export class ContactService {

  baseUrl: string = '/api/contact';

  constructor(
    private http: HttpClient) { }

    list(): Observable<ContactViewModel[]> {
      return this.http.get<ContactViewModel[]>(this.baseUrl);
    }

    create(model: ContactInputModel): Observable<ContactViewModel> {
      return this.http.post<ContactViewModel>(this.baseUrl, model);
    }

    update(id: number, model: ContactInputModel): Observable<ContactViewModel> {
      return this.http.put<ContactViewModel>(`${this.baseUrl}/${id}`, model);
    }

    delete(id: number): Observable<void> {
      return this.http.delete<void>(`${this.baseUrl}/${id}`);
    }

}
