import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { ReimbursementService } from '../reimbursement.service';

@Component({
  selector: 'app-signin',
  templateUrl: './signin.component.html',
  styleUrls: ['./signin.component.css'],
})
export class SigninComponent implements OnInit {
  ngOnInit(): void {}
  isAuth: boolean = false;

  error: boolean = false;
  log = new FormGroup({
    email: new FormControl(''),
    pass: new FormControl(''),
  });
  constructor(private router: Router, private resSer: ReimbursementService) {}

  auth() {
    console.log(this.log.controls['email'].value);
    try {
      if (
        this.log.controls['email'].value == '' ||
        this.log.controls['pass'].value == ''
      ) {
        this.error = true;
        return;
      }

      var data = {
        Email: this.log.controls['email'].value,
        Password: this.log.controls['pass'].value,
      };

      this.resSer.signin(data).subscribe((res: any) => {
        console.warn(res['isApprover']);
        if (res['id'] != null) {
          if (res['isApprover'])
            this.router.navigateByUrl('admin/' + `${res['id']}`);
          else this.router.navigateByUrl('employee/' + `${res['id']}`);
        }
      });
      
    } catch (error) {}
  }
}
