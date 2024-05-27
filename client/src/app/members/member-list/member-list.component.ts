import { Component } from '@angular/core';
import { Member } from '../../models/member';
import { MembersService } from '../../services/members.service';
import { CommonModule } from '@angular/common';
import { MemberCardComponent } from '../member-card/member-card.component';
import { Pagination } from '../../models/pagination';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { UserParams } from '../../models/userParams';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-member-list',
  standalone: true,
  imports: [CommonModule, MemberCardComponent, NgbPaginationModule, FormsModule, ReactiveFormsModule],
  templateUrl: './member-list.component.html',
  styleUrl: './member-list.component.css'
})
export class MemberListComponent {

  members: Member[] = [];
  pagination: Pagination | undefined;
  userParams: UserParams | undefined;

  constructor(private memberService: MembersService) {
    this.userParams = this.memberService.getUserParams();
    this.loadMembers();
  }

  loadMembers() {
    if (this.userParams) {
      this.memberService.setUserParams(this.userParams);
      this.memberService.getMembers(this.userParams).subscribe({
        next: response => {
          if (response.result && response.pagination) {
            this.members = response.result;
            this.pagination = response.pagination;
          }
        }
      })
    }
  }

  resetFilters() {
    this.userParams = this.memberService.resetUserParams();
    this.loadMembers();

  }

  pageChanged(event: any) {
    if (this.userParams && this.userParams.pageNumber !== event) {
      this.userParams.pageNumber = event;
      this.memberService.setUserParams(this.userParams);
      this.loadMembers();
    }
  }

}
