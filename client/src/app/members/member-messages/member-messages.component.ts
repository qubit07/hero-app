import { AfterViewChecked, Component, Input, ViewChild } from '@angular/core';
import { MessageService } from '../../services/message.service';
import { CommonModule } from '@angular/common';
import { Message } from '../../models/message';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-member-messages',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './member-messages.component.html',
  styleUrl: './member-messages.component.css'
})
export class MemberMessagesComponent implements AfterViewChecked {

  @ViewChild('messageForm') messageForm?: NgForm;
  @ViewChild('scrollMessages') scrollContainer?: any;
  @Input() username: string | undefined;
  messageContent = '';


  constructor(public messageService: MessageService) {

  }
  ngAfterViewChecked(): void {
    this.scollToBottom();
  }

  sendMessage() {
    if (!this.username) return;

    this.messageService.sendMessage(this.username, this.messageContent).then(() => {
      this.messageForm?.reset();
    })
  }

  private scollToBottom() {
    if (this.scrollContainer) {
      this.scrollContainer.nativeElement.scrollTop = this.scrollContainer.nativeElement.scollHeight;
    }
  }

}
