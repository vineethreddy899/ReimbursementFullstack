import { Component, OnInit } from '@angular/core';
import { EmailValidator, FormControl, FormGroup } from '@angular/forms';
import { ReimbursementService } from '../reimbursement.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  constructor(private resSer: ReimbursementService) {}
 isCreated:boolean=false;
  errorMessage: string = '';
  log = new FormGroup({
    email: new FormControl(''),
    pass: new FormControl(''),
    conpass: new FormControl(''),
    name: new FormControl(''),
    pan: new FormControl(''),
    bank: new FormControl('Select your Bank'),
    bankAccount: new FormControl(''),
  });

  ngOnInit(): void {}

  validator() {
    this.errorMessage = '';
    var mailformat = /^\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$/;
    if (!this.log.controls['email'].value.match(mailformat)) {
      this.errorMessage = 'Enter a valid email';
      return false;
    }
    var passFormat = /^(?=.{5,})(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[\W])/;
    if (!this.log.controls['pass'].value.match(passFormat)) {
      this.errorMessage =
        'Enter a valid password (Minimum length is 8 and it must have at-least 1 special character, 1 number and 1 alphabet) ';
      return false;
    }
    if (this.log.controls['pass'].value != this.log.controls['conpass'].value) {
      this.errorMessage = 'Password and Confirm Password does not match ';
      return false;
    }
    if (
      !this.log.controls['pan'].value.match(/^[0-9a-zA-Z]+$/) ||
      this.log.controls['pan'].value.length !=10
    ) {
      this.errorMessage = 'Pan Number should be 10-digit alphanumeric';
      return false;
    }
    if (this.log.controls['bank'].value == 'Select your Bank') {
      this.errorMessage = 'Select a bank';
      return false;
    }
    if (
      !this.log.controls['bankAccount'].value.match(/^[0-9]+$/) ||
      this.log.controls['bankAccount'].value.length != 12
    ) {
      this.errorMessage = 'Enter a valid 12 digit account number';
      return false;
    }
    var email = {"mail":this.log.controls['email'].value};
    this.resSer.isUnique(email).subscribe((res: any) => {
      console.warn(res);
      if (res == false) {
        this.errorMessage = 'Enter a unique Email';
      }
      return res;
    });
    return true;
  }
  auth() {
    if (!this.validator()) {
      return;
    }
    var data={
      "Name":this.log.controls['name'].value,
      "Email":this.log.controls['email'].value,
      "Bank":this.log.controls['bank'].value,
      "BankAccountNumber":this.log.controls['bankAccount'].value.toString(),
      "PanCardNumber":this.log.controls['pan'].value,
      "Password":this.log.controls['pass'].value,
      "ConfirmPassword":this.log.controls['conpass'].value,
  }
    this.resSer.signup(data).subscribe((res: any) => {
      console.warn(res);
      if (res == null) {
        this.errorMessage="Something Went wrong"
      }
      else {
        this.isCreated=true;
      }
    });
  }
}
