import { Component, Input, ViewEncapsulation } from '@angular/core';
import { Member } from '../../models/member';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-member-card',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './member-card.component.html',
  styleUrl: './member-card.component.css',
})
export class MemberCardComponent {
  @Input() member: Member | undefined


  constructor() {

  }
}
