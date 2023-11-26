import { Component, OnInit } from '@angular/core';
import { findIndex } from 'lodash';
import { BehaviorSubject, Observable } from 'rxjs';
import { ContactInputModel } from '../shared/models/input-models/contact.input-model';
import { SelectListItem } from '../shared/models/select-list-item';
import { ContactViewModel } from '../shared/models/view-models/contact.view-model';
import { ContactService } from '../shared/services/contact.service';
import { LabelService } from '../shared/services/label.service';
import { NotificationHandlerService } from '../shared/services/notification-handler.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrl: './contact.component.css'
})
export class ContactComponent implements OnInit {

  private labelsSubject: BehaviorSubject<SelectListItem[]> = new BehaviorSubject<SelectListItem[]>([]);
  public label$: Observable<SelectListItem[]> = this.labelsSubject.asObservable();  

  private contactsSubject: BehaviorSubject<ContactViewModel[]> = new BehaviorSubject<ContactViewModel[]>([]);
  public contact$: Observable<ContactViewModel[]> = this.contactsSubject.asObservable();

  public selectedLabel: string = '';

  public existingContact: ContactViewModel = {
    id: 0,
    givenName: '',
    familyName: '',
    mobileNumber: '',
    birthDate: '',
    label: {
      disabled: false,
      group: '',
      selected: false,
      text: '',
      value: ''
    }
  };  

  constructor(
    private labelService: LabelService,
    private contactService: ContactService,
    private notificationHandlerService: NotificationHandlerService
  ) {
  }

  ngOnInit(): void {    
    this.labelService
      .dropdown()
      .subscribe({
        next: (data) => {
          this.labelsSubject.next(data);
        },
        error: (error) => {
          //console.error(error);
        },
        complete: () => {
          //console.info('complete');
        }
      });

    this.contactService
      .list()
      .subscribe({
        next: (data) => {
          this.contactsSubject.next(data);
        },
        error: (error) => {
          //console.error(error);
        },
        complete: () => {
          //console.info('complete');
        }
      });
  }

  onClickLabel(labelId: string) {
    this.selectedLabel = labelId;
  }

  addContact(model: ContactInputModel) {
    const nextCallback = (data: ContactViewModel) => {
      const currentContacts = this.contactsSubject.value;
      const updatedContacts = [...currentContacts, data];
      this.contactsSubject.next(updatedContacts);

      this.createOrUpdateLabels(data);
    };
    this.notificationHandlerService.handleCreate(this.contactService.create(model), nextCallback);
  }

  update(contact: ContactViewModel) {
    this.existingContact = contact;
  }

  updateContact(model: ContactInputModel) {
    const nextCallback = (data: ContactViewModel) => {
      const currentContacts = this.contactsSubject.value;
      const updatedContacts = currentContacts.map(contact =>
        contact.id === data.id ? data : contact
      );
      this.contactsSubject.next(updatedContacts);

      this.createOrUpdateLabels(data);
    };
    this.notificationHandlerService.handleUpdate(this.contactService.update(this.existingContact.id, model), nextCallback);
  }

  delete(contact: ContactViewModel) {
    const nextCallback = () => {
      const currentContacts = this.contactsSubject.value;
      const index = findIndex(currentContacts, c => c.id === contact.id);
      currentContacts.splice(index, 1);
      this.contactsSubject.next(currentContacts);
    };
    this.notificationHandlerService.handleDelete(this.contactService.delete(contact.id), nextCallback);
  }

  private createOrUpdateLabels(data: ContactViewModel) {
    const currentLabels = this.labelsSubject.value;
    const labelIndex = findIndex(currentLabels, l => l.value === data.label.value);
    if (labelIndex >= 0) {
      const updatedLabels = currentLabels.map(label =>
        label.value === data.label.value ? data.label : label
      );
      this.labelsSubject.next(updatedLabels);
    } else {
      const updatedLabels = [...currentLabels, data.label];
      this.labelsSubject.next(updatedLabels);
    }
  }

}
