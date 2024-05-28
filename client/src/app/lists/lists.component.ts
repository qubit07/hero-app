import { Component } from '@angular/core';
import { Member } from '../models/member';
import { MembersService } from '../services/members.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MemberCardComponent } from '../members/member-card/member-card.component';
import { NgbPaginationModule } from '@ng-bootstrap/ng-bootstrap';
import { Pagination } from '../models/pagination';

@Component({
  selector: 'app-lists',
  standalone: true,
  imports: [CommonModule, MemberCardComponent, NgbPaginationModule, FormsModule],
  templateUrl: './lists.component.html',
  styleUrl: './lists.component.css'
})
export class ListsComponent {

  members: Member[] | undefined;
  filter = { predicate: 'friend' };
  pageNumber = 1;
  pageSize = 5;
  pagination: Pagination | undefined;

  constructor(private memberService: MembersService) {
    this.loadFriends();
  }

  loadFriends() {
    this.memberService.getFriendships(this.filter.predicate, this.pageNumber, this.pageSize).subscribe({
      next: response => {
        this.members = response.result;
        this.pagination = response.pagination;
      }
    })
  }

  handleOnChange(event: any) {
    this.filter.predicate = event.target.value;
    this.loadFriends();
  }

  pageChanged(event: any) {
    if (this.pageNumber !== event) {
      this.pageNumber = event;
      this.loadFriends();
    }
  }

}
