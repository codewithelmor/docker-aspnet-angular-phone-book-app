import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";
import parsePhoneNumber, { CountryCode } from 'libphonenumber-js';

export function PhoneValidator(iso2: string): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
        if (control.value !== null) {
            let invalidFormat = false;
            const value = control.value;
            const phoneNumber = parsePhoneNumber(value, iso2 as CountryCode);
            const isValid = phoneNumber?.isValid();        
            invalidFormat = !(isValid);
            if (isValid) {
                let nationalNumber = phoneNumber?.nationalNumber;
                if (nationalNumber !== undefined) {
                    const checkTheRightMostCharacters = value.split(nationalNumber);
                    invalidFormat = checkTheRightMostCharacters[1].length > 0;
                }
            }
            return invalidFormat ? { invalidFormat: true } : null;
        }
        return null;
    };
}