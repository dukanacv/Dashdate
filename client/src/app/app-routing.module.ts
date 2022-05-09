import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';

const routes: Routes = [//routes that the app has and their paths
  { path: "", component: HomeComponent },
  { path: "members", component: MemberListComponent, canActivate: [AuthGuard] },
  { path: "members/:username", component: MemberDetailComponent, canActivate: [AuthGuard] },
  { path: "lists", component: ListsComponent, canActivate: [AuthGuard] },
  { path: "messages", component: MessagesComponent, canActivate: [AuthGuard] },
  { path: "**", component: HomeComponent }//if user type smth that is not in config => "Wildcard Route"
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
