import { Component } from '@angular/core';
import { Member } from '../../models/member';
import { MembersService } from '../../services/members.service';
import { CommonModule } from '@angular/common';
import { MemberCardComponent } from '../member-card/member-card.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [CommonModule, MemberCardComponent],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent {

  members$: Observable<Member[]> | undefined;

  constructor(private memberService: MembersService) {
    this.members$ = this.memberService.getMembers();
  }

}
