import { Component, OnInit } from '@angular/core';
import { AdminService } from '../../services/admin.service';
import { User } from '../../models/user';
import { CommonModule } from '@angular/common';
import { BsModalRef, BsModalService, ModalModule, ModalOptions } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../../modals/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [CommonModule, ModalModule],
  providers: [BsModalService],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent implements OnInit {

  users: User[] = [];
  bsModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>();
  availableRoles = [
    'admin',
    'member'
  ]

  constructor(private adminService: AdminService, private modalService: BsModalService) {

  }
  ngOnInit(): void {
    this.getUserWithRoles();
  }

  getUserWithRoles() {
    this.adminService.getUserWithRoles().subscribe({
      next: users => this.users = users
    })
  }

  openRolesModal(user: User) {

    const config = {
      class: 'modal-dialog-centered',
      initialState: {
        username: user.username,
        availableRoles: this.availableRoles,
        selectedRoles: [...user.roles]
      }
    }

    this.bsModalRef = this.modalService.show(RolesModalComponent, config);
    this.bsModalRef.onHide?.subscribe({
      next: () => {
        const selectedRoles = this.bsModalRef.content?.selectedRoles;
        if (!this.arrayEqual(selectedRoles!, user.roles)) {
          this.adminService.updateUserRoles(user.username, selectedRoles!).subscribe({
            next: roles => user.roles = roles
          });
        }
      }
    })
  }

  private arrayEqual(arr1: any[], arr2: any[]) {
    return JSON.stringify(arr1.sort()) === JSON.stringify(arr2.sort());
  }

}
