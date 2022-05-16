import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { ListsComponent } from './lists/lists.component';
import { MemberDetailComponent } from './members/member-detail/member-detail.component';
import { MemberEditComponent } from './members/member-edit/member-edit.component';
import { MemberListComponent } from './members/member-list/member-list.component';
import { MessagesComponent } from './messages/messages.component';
import { AuthGuard } from './_guards/auth.guard';
import { PreventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';

const routes: Routes = [//routes that the app has and their paths
  { path: "", component: HomeComponent },
  { path: "members", component: MemberListComponent, canActivate: [AuthGuard] },
  { path: "members/:username", component: MemberDetailComponent, canActivate: [AuthGuard] },
  {
    path: "member/edit", component: MemberEditComponent, canActivate: [AuthGuard], //if using memberS/edit => say patMatch:'full'
    canDeactivate: [PreventUnsavedChangesGuard]
  },
  { path: "lists", component: ListsComponent, canActivate: [AuthGuard] },
  { path: "messages", component: MessagesComponent, canActivate: [AuthGuard] },
  { path: "**", component: HomeComponent }//if user type smth that is not in config => "Wildcard Route"
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
