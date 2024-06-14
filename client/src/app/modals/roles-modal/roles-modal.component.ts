import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-roles-modal',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './roles-modal.component.html',
  styleUrl: './roles-modal.component.css'
})
export class RolesModalComponent {
  username = '';
  availableRoles: any[] = [];
  selectedRoles: any[] = [];

  constructor(public bsModalRef: BsModalRef) {

  }

  updateChecked(checkedValue: string) {
    const index = this.selectedRoles.indexOf(checkedValue);
    if (index !== -1) {
      this.selectedRoles.splice(index, 1);
    } else {
      this.selectedRoles.push(checkedValue);
    }
  }

}
