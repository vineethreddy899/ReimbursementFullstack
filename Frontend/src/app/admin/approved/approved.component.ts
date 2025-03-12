import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ReimbursementService } from 'src/app/reimbursement.service';

@Component({
  selector: 'app-approved',
  templateUrl: './approved.component.html',
  styleUrls: ['./approved.component.css']
})
export class ApprovedComponent implements OnInit {

  constructor(
    private router: ActivatedRoute,
    private resSer: ReimbursementService,
  ) {}
  collection: any = [];
  filter = new FormGroup({
    mail: new FormControl(''),
    type: new FormControl('Request type (All)'),
  });

  public createImgPath = (serverPath: string) => { 
    return `https://localhost:44300/${serverPath}`.replace(/[[\]]/g,'/'); 
  }
  getFilter(){
    if(this.filter.controls['mail'].value==''){
      this.data['mailId']=null;
    }else{
      this.data['mailId']=this.filter.controls['mail'].value;
    }
    if(this.filter.controls['type'].value=='Request type (All)'){
      this.data['type']=null;
    }else{
      this.data['type']=this.filter.controls['type'].value;
    }
    this.resSer.getApproved(this.data).subscribe((res: any) => {
      this.collection = res;
      console.warn(this.collection);
    });
  }
 data:any;
  ngOnInit(): void {
    this.data= {mailId:null,type:null};
    this.resSer
      .getApproved(this.data)
      .subscribe((res: any) => {
        this.collection = res;
        console.warn(this.collection)
      });
  }

}
