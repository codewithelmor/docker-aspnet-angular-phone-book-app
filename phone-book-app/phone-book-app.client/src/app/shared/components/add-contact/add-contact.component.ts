import { Component, EventEmitter, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ContactInputModel } from '../../models/input-models/contact.input-model';
import { SelectListItem } from '../../models/select-list-item';
import { PhoneValidator } from '../../validators/phone.validator';

@Component({
  selector: 'app-add-contact',
  templateUrl: './add-contact.component.html',
  styleUrl: './add-contact.component.css'
})
export class AddContactComponent implements OnInit, OnChanges {

  @Input()
  labels: SelectListItem[] | null = [];

  @Output()
  addContact: EventEmitter<ContactInputModel> = new EventEmitter<ContactInputModel>();

  addForm = new FormGroup({
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
  }  

  close() {
    this.addForm.reset();
    this.addForm = new FormGroup({
      givenName: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
      familyName: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
      mobileNumber: new FormControl('', [Validators.required, Validators.maxLength(20), PhoneValidator('PH')]),
      birthDate: new FormControl('', [Validators.maxLength(10)]),
      label: new FormControl('', [Validators.required, Validators.minLength(1), Validators.maxLength(20)]),
    });
  }

  save() {
    let contact = this.addForm.value as ContactInputModel;
    this.addContact.emit(contact);
    this.close();
    let element: HTMLElement = document.querySelector('#addModal .btn-close') as HTMLElement;
    element.click();
  }

}