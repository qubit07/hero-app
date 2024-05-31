import { Component } from '@angular/core';
import { Message } from '../models/message';
import { Pagination } from '../models/pagination';
import { MessageService } from '../services/message.service';
import { CommonModule } from '@angular/common';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-messages',
  standalone: true,
  imports: [CommonModule, NgbPaginationModule, RouterModule],
  templateUrl: './messages.component.html',
  styleUrl: './messages.component.css'
})
export class MessagesComponent {
  messages: Message[] | undefined;
  pagination: Pagination | undefined;
  container = 'Outbox';
  pageNumber = 1;
  pageSize = 5;
  loading = false;


  constructor(private messageService: MessageService) {
    this.loadMessages();
  }

  loadMessages() {
    this.loading = true;
    this.messageService.getMessages(this.pageNumber, this.pageSize, this.container).subscribe({
      next: response => {
        this.messages = response.result;
        this.pagination = response.pagination;
        this.loading = false;
      }
    });
  }

  deleteMessage(id: number) {
    this.messageService.deleteMessage(id).subscribe({
      next: () => this.messages?.splice(this.messages.findIndex(m => m.id === id), 1)
    })
  }

  onRadioChange(value: string) {
    this.container = value;
    this.loadMessages();
  }

  pageChanged(event: any) {
    if (this.pageNumber != event.page) {
      this.pageNumber = event.page;
      this.loadMessages();
    }
  }
}
