import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import { MylistComponent } from './employee/mylist/mylist.component'
import { AdminComponent } from './admin/admin.component';
import { ImageViewComponent } from './image-view/image-view.component';



const routes: Routes = [
  { path: '', redirectTo: '/signin', pathMatch: 'full' },
  {component:SignupComponent,path:'signup'},
  {component:SigninComponent,path:'signin'},
  {component:MylistComponent,path:'employee/:id'},
  {component:AdminComponent,path:'admin/:id'},
  {component:ImageViewComponent,path:'image/:id'},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
