import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ReimbursementService } from 'src/app/reimbursement.service';


@Component({
  selector: 'app-declined',
  templateUrl: './declined.component.html',
  styleUrls: ['./declined.component.css']
})
export class DeclinedComponent implements OnInit {

  constructor(
    private router: ActivatedRoute,
    private resSer: ReimbursementService,
  ) {}
  collection: any = [];

  public createImgPath = (serverPath: string) => { 
    return `https://localhost:44300/${serverPath}`.replace(/[[\]]/g,'/'); 
  }
  filter = new FormGroup({
    mail: new FormControl(''),
    type: new FormControl('Request type (All)'),
  });

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
    this.resSer.getDeclined(this.data).subscribe((res: any) => {
      this.collection = res;
      console.warn(this.collection);
    });
  }
 data:any;
  ngOnInit(): void {
    this.data= {mailId:null,type:null};
    this.resSer
      .getDeclined(this.data)
      .subscribe((res: any) => {
        this.collection = res;
        console.warn(this.collection)
      });
  }

}
