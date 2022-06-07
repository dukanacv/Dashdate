import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { Member } from '../_models/member';


@Injectable({
  providedIn: 'root'
})
export class MembersService {
  baseUrl = environment.apiUrl

  constructor(private http: HttpClient) { }

  getMembers() {
    return this.http.get<Member[]>(this.baseUrl + "users")
  }

  getMember(username: string) {
    return this.http.get<Member>(this.baseUrl + "users/" + username)
  }

  updateMember(member: Member) {
    return this.http.put(this.baseUrl + "users", member)
  }

  setMainPhoto(photoId: number) {
    return this.http.put(this.baseUrl + "users/main-photo/" + photoId, {})
  }

  deletePhoto(photoId: number) {
    return this.http.delete(this.baseUrl + "users/delete-photo/" + photoId)
  }

  addLike(username: string) {
    return this.http.post(this.baseUrl + "likes/" + username, {})
  }

  getLikes(whatIsNeeded: string) {
    return this.http.get<Partial<Member[]>>(this.baseUrl + "likes?whatIsNeeded=" + whatIsNeeded);
  }
}
