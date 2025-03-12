import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ReimbursementService } from 'src/app/reimbursement.service';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { FormControl, FormGroup } from '@angular/forms';

@Component({
  selector: 'app-pending',
  templateUrl: './pending.component.html',
  styleUrls: ['./pending.component.css'],
})
export class PendingComponent implements OnInit {
  constructor(
    private router: ActivatedRoute,
    private resSer: ReimbursementService,
    private modalService: NgbModal
  ) {}
  collection: any = [];
  


  public createImgPath = (serverPath: string) => { 
    return `https://localhost:44300/${serverPath}`.replace(/[[\]]/g,'/'); 
  }
  approve = new FormGroup({
    ApprovedBy: new FormControl(''),
    ApprovedAmount: new FormControl(0.00),
    InternalNotes: new FormControl(''),
  });
  reject = new FormGroup({
    DeclinedBy: new FormControl(''),
    InternalNotes: new FormControl(''),
  });

  errorMessage="";
  isApproved: boolean = false;
  isRejected: boolean = false;
   data:any;
  ngOnInit(): void {
    this.data = { mailId: null, type: null };
    this.resSer.getPending(this.data).subscribe((res: any) => {
      this.collection = res;
      console.warn(this.collection);
    });
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
    this.resSer.getPending(this.data).subscribe((res: any) => {
      this.collection = res;
      console.warn(this.collection);
    });
  }

  getApproved(data:any,id:any){
    this.errorMessage="";
    if(this.approve.controls['ApprovedBy'].value==''){
      this.errorMessage="Enter the email id of approved by";
      return;
    }
    if(this.approve.controls['ApprovedAmount'].value<=0.00){
      this.errorMessage="Enter amount greater than zero";
      return ;
    }
    data['RequestPhase']="Approved";
    data['ApprovedBy']=this.approve.controls['ApprovedBy'].value;
    data['ApprovedAmount']=this.approve.controls['ApprovedAmount'].value;
    data['InternalNotes']=this.approve.controls['InternalNotes'].value;
    this.resSer.updateClaim(data,id).subscribe((res: any) => {
      this.isApproved = res;
      if(!res){
        this.errorMessage="Error occured";

      }
    this.resSer.getPending(this.data).subscribe((res: any) => {
      this.collection = res;
      console.warn(this.collection);
    });
    });
    
   
  }
  getRejected(data:any,id:any){
    this.errorMessage="";
    if(this.reject.controls['DeclinedBy'].value==''){
      this.errorMessage="Enter the email id of approved by";
      return;
    }
    
    data['RequestPhase']="Declined";
    data['ApprovedBy']=this.reject.controls['DeclinedBy'].value;
    data['InternalNotes']=this.reject.controls['InternalNotes'].value;
    this.resSer.updateClaim(data,id).subscribe((res: any) => {
      this.isRejected = res;
      if(!res){
        this.errorMessage="Error occured";

      }
    this.resSer.getPending(this.data).subscribe((res: any) => {
      this.collection = res;
      console.warn(this.collection);
    });
    });
    
   
  }



  closeModal: string = '';
  triggerModal(content: any, data?: any) {
    this.isApproved = false;
    this.isRejected = false;
    this.errorMessage="";
    this.modalService
      .open(content, { ariaLabelledBy: 'modal-basic-title' })
      .result.then(
        (res) => {
          this.closeModal = `Closed with: ${res}`;
        },
        (res) => {
          this.closeModal = `Dismissed ${this.getDismissReason(res)}`;
        }
      );
  }

  private getDismissReason(reason: any): string {
    if (reason === ModalDismissReasons.ESC) {
      return 'by pressing ESC';
    } else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
      return 'by clicking on a backdrop';
    } else {
      return `with: ${reason}`;
    }
  }
}
