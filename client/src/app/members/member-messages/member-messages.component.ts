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


  @Input() messages: Message[] = [];


  constructor() {

  }

  ngOnInit(): void {


  }




}
