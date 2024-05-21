import { Component, Input } from '@angular/core';
import { Member } from '../../models/member';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../services/account.service';
import { take } from 'rxjs';
import { User } from '../../models/user';
import { Photo } from '../../models/photo';
import { MembersService } from '../../services/members.service';

@Component({
  selector: 'app-photo-editor',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './photo-editor.component.html',
  styleUrl: './photo-editor.component.css'
})
export class PhotoEditorComponent {
  @Input() member: Member | undefined;

  user: User | undefined;


  constructor(private accountService: AccountService, private memberService: MembersService) {
    this.accountService.currentUser$.pipe(take(1)).subscribe({
      next: user => {
        if (user) {
          this.user = user;
        }
      }
    })
  }

  setMainPhoto(photo: Photo) {
    return this.memberService.setMainPhoto(photo.id).subscribe({
      next: () => {
        if (this.user && this.member) {
          this.user.photoUrl = photo.url;
          this.accountService.setCurrentUser(this.user);
          this.member.photoUrl = photo.url;
          this.member.photos.forEach(p => {
            if (p.isMain) {
              p.isMain = false;
            }
            if (p.id === photo.id) {
              p.isMain = true;
            }
          })
        }
      }
    })
  }



}
