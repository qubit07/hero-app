import { Component, Input } from '@angular/core';
import { MessageService } from '../../services/message.service';
import { CommonModule } from '@angular/common';
import { Message } from '../../models/message';

@Component({
  selector: 'app-member-messages',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent {


  @Input() username?: string;
  messages: Message[] = [];


  constructor(private messageService: MessageService) {

  }

  ngOnInit(): void {
    this.loadMessages();

  }

  loadMessages() {
    if (this.username) {
      this.messageService.getMessageThread(this.username).subscribe({
        next: messages => this.messages = messages
      })
    }
  }


}
