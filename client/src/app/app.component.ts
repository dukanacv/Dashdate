import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'The Dashdate app';
  users: any

  constructor(private http: HttpClient) { }

  ngOnInit(): void {//observables dont do anything unless we sub to them
    this.getUsers()
  }

  getUsers() {
    this.http
      .get("https://localhost:5001/api/users")
      .subscribe(response => {
        this.users = response
      }, error => {
        console.log(error)
      })
  }
}
