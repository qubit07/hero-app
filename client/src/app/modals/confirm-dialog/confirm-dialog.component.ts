import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';
import { BsModalRef, BsModalService, ModalModule } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule, ModalModule],
  providers: [BsModalService],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.css'
})
export class ConfirmDialogComponent {

  bsModalRef = inject(BsModalRef);
  title = '';
  message = '';
  btnOkText = '';
  btnCancelText = '';
  result = false;


  confirm() {
    this.result = true;
    this.bsModalRef.hide();
  }

  decline() {
    this.bsModalRef.hide();
  }

}
