import { SelectListItem } from "../select-list-item";

export interface ContactViewModel {
    id: number;
    givenName: string;
    familyName: string;
    mobileNumber: string;
    birthDate?: string;
    label: SelectListItem;
}