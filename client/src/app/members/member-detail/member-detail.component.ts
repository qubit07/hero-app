import { Component } from '@angular/core';
import { Member } from '../../models/member';
import { MembersService } from '../../services/members.service';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-member-detail',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './member-detail.component.html',
  styleUrl: './member-detail.component.css'
})
export class MemberDetailComponent {

  member: Member | undefined;

  constructor(private memberService: MembersService, private route: ActivatedRoute) {
    this.loadMember();
  }

  loadMember() {
    const username = this.route.snapshot.paramMap.get('username');
    console.log(username)
    if (!username) {
      return;
    }
    this.memberService.getMember(username).subscribe({
      next: member => this.member = member
    })
  }

}
