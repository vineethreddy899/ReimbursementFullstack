import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SignupComponent } from './signup/signup.component';
import { SigninComponent } from './signin/signin.component';
import {HttpClientModule} from '@angular/common/http';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MylistComponent } from './employee/mylist/mylist.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AdminComponent } from './admin/admin.component';
import { PendingComponent } from './admin/pending/pending.component';
import { ApprovedComponent } from './admin/approved/approved.component';
import { DeclinedComponent } from './admin/declined/declined.component';
import { UploadComponent } from './employee/upload/upload.component';
import { ImageViewComponent } from './image-view/image-view.component';



@NgModule({
  declarations: [
    AppComponent,
    SignupComponent,
    SigninComponent,
    MylistComponent,
    AdminComponent,
    PendingComponent,
    ApprovedComponent,
    DeclinedComponent,
    UploadComponent,
    ImageViewComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    NgbModule,
     
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
