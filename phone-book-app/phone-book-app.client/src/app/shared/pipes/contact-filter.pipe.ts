import { Pipe, PipeTransform } from '@angular/core';
import { filter } from 'lodash';
import { Observable, map } from 'rxjs';
import { ContactViewModel } from '../models/view-models/contact.view-model';

@Pipe({
  name: 'contactFilter'
})
export class ContactFilterPipe implements PipeTransform {

  transform(values: Observable<ContactViewModel[]>, labelId: string): Observable<ContactViewModel[]> {
    //return filter(values, m => m['label']['value'] === labelId);
    if (labelId !== undefined && labelId !== null && labelId !== '') {
      return values.pipe(
        map(contacts => filter(contacts, c => c['label']['value'] === labelId))
      );
    }
    return values;
  }

}