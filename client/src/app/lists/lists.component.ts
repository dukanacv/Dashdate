import { Component, OnInit } from '@angular/core';
import { Member } from '../_models/member';
import { MembersService } from '../_services/members.service';

@Component({
  selector: 'app-lists',
  templateUrl: './lists.component.html',
  styleUrls: ['./lists.component.css']
})
export class ListsComponent implements OnInit {
  members!: Partial<Member[]>//bcs not every time we gonna display full members
  whatIsNeeded = "likedBy";

  constructor(private memberService: MembersService) { }

  ngOnInit(): void {
    this.loadLikes()
  }

  loadLikes() {
    this.memberService.getLikes(this.whatIsNeeded).subscribe(response => {
      this.members = response
    })
  }
}
