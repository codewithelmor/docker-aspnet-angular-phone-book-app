import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ContactInputModel } from '../../models/input-models/contact.input-model';
import { SelectListItem } from '../../models/select-list-item';
import { ContactViewModel } from '../../models/view-models/contact.view-model';
import { PhoneValidator } from '../../validators/phone.validator';

@Component({
  selector: 'app-update-contact',
  templateUrl: './update-contact.component.html',
  styleUrl: './update-contact.component.css'
})
export class UpdateContactComponent implements OnInit, OnChanges {

  @Input()
  labels: SelectListItem[] | null = [];

  @Input()
  existingContact: ContactViewModel = {
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

  @Output()
  updateContact: EventEmitter<ContactInputModel> = new EventEmitter<ContactInputModel>();

  updateForm = new FormGroup({
    id: new FormControl(0, [Validators.required, Validators.min(1)]),    
    givenName: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
    familyName: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
    mobileNumber: new FormControl('', [Validators.required, Validators.maxLength(20), PhoneValidator('PH')]),
    birthDate: new FormControl('', [Validators.maxLength(10)]),
    label: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
  });

  constructor() {
  }

  ngOnInit(): void {
  }

  ngOnChanges(changes: SimpleChanges): void {
    this.updateForm = new FormGroup({
      id: new FormControl(this.existingContact.id, [Validators.required, Validators.min(1)]),
      givenName: new FormControl(this.existingContact.givenName, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
      familyName: new FormControl(this.existingContact.familyName, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
      mobileNumber: new FormControl(this.existingContact.mobileNumber, [Validators.required, Validators.maxLength(20), PhoneValidator('PH')]),
      birthDate: new FormControl(this.existingContact.birthDate ? this.existingContact.birthDate : '', [Validators.maxLength(10)]),
      label: new FormControl(this.existingContact.label.text, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
    });    
  }

  close() {
    this.updateForm.reset();
    this.updateForm = new FormGroup({
      id: new FormControl(this.existingContact.id, [Validators.required, Validators.min(1)]),
      givenName: new FormControl(this.existingContact.givenName, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
      familyName: new FormControl(this.existingContact.familyName, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
      mobileNumber: new FormControl(this.existingContact.mobileNumber, [Validators.required, Validators.maxLength(20), PhoneValidator('PH')]),
      birthDate: new FormControl(this.existingContact.birthDate ? this.existingContact.birthDate : '', [Validators.maxLength(10)]),
      label: new FormControl(this.existingContact.label.text, [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
    });
  }

  save() {
    let contact = this.updateForm.value as ContactInputModel;
    this.updateContact.emit(contact);
    this.close();
    let element: HTMLElement = document.querySelector('#updateModal .btn-close') as HTMLElement;
    element.click();
  }

}