import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { ModalDismissReasons, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { ReimbursementService } from 'src/app/reimbursement.service';

@Component({
  selector: 'app-mylist',
  templateUrl: './mylist.component.html',
  styleUrls: ['./mylist.component.css'],
})
export class MylistComponent implements OnInit {
  closeModal: string = '';
  constructor(
    private router: ActivatedRoute,
    private resSer: ReimbursementService,
    private modalService: NgbModal
  ) {}
  
  


  

  collection: any = [];
  ngOnInit(): void {
    this.resSer
      .getMyRecord(this.router.snapshot.params['id'])
      .subscribe((res: any) => {
        this.collection = res;
        
      });
  }
  // upload image
  imgPath= {dbPath: ''};

  uploadFinished = (event:any) => { 
    this.imgPath = event; 
    console.log(this.imgPath);
  }
  public createImgPath = (serverPath: string) => { 
    return `https://localhost:44300/${serverPath}`.replace(/[[\]]/g,'/'); 
  }
  
  //add record
  errorAdded:string ='';
  isAdded: boolean = false;


  add = new FormGroup({
    Date: new FormControl(''),
    ReimbursementType: new FormControl(''),
    RequestedValue: new FormControl(0),
    Currency: new FormControl(''),
  });
  addRecord() {

    if(this.add.controls['Date'].value==''){
      this.errorAdded="Please Select Date";
      return ;
    }
    if(this.add.controls['ReimbursementType'].value==''){
      this.errorAdded="Please Select Reimbursement Type";
      return ;
    }
    if(this.add.controls['RequestedValue'].value<=0){
      this.errorAdded="Enter Reimbursement Value greater than zero.";
      return ;
    }
    if(this.add.controls['Currency'].value==''){
      this.errorAdded="Please Select Currency";
      return ;
    }
    const p = new Date(Date.parse(this.add.controls['Date'].value));

    var data = {
      ReimbursementType: this.add.controls['ReimbursementType'].value,
      RequestedValue: this.add.controls['RequestedValue'].value,
      Date: p.toJSON(),
      CreatorId: this.router.snapshot.params['id'],
      Currency: this.add.controls['Currency'].value,
      RequestPhase: 'Pending',
      ImagePath:this.imgPath.dbPath
    };

    this.resSer.addClaim(data).subscribe((res: any) => {
      this.isAdded = res;
      if (this.isAdded) {
        this.add ==
          new FormGroup({
            Date: new FormControl(''),
            ReimbursementType: new FormControl(''),
            RequestedValue: new FormControl(0),
            Currency: new FormControl(''),
          });
      }
      console.log(data)
      this.resSer
        .getMyRecord(this.router.snapshot.params['id'])
        .subscribe((res: any) => {
          this.collection = res;
        });
    });
    
   
  }


  // edit record
  errorEdited:string ="";

  isEdited: boolean = false;
  edit = new FormGroup({
    Date: new FormControl(''),
    ReimbursementType: new FormControl(''),
    RequestedValue: new FormControl(0),
    Currency: new FormControl(''),
  });
  editRecord(data:any,id:any) {
    if(this.edit.controls['Date'].value==''){
      this.errorEdited="Please Select Date";
      return ;
    }
    if(this.edit.controls['ReimbursementType'].value==''){
      this.errorEdited="Please Select Reimbursement Type";
      return ;
    }
    if(this.edit.controls['RequestedValue'].value<=0){
      this.errorEdited="Enter Reimbursement Value greater than zero.";
      return ;
    }
    if(this.edit.controls['Currency'].value==''){
      this.errorEdited="Please Select Currency";
      return ;
    }
    
    data['Date']=this.edit.controls['Date'].value;
    data['ReimbursementType']=this.edit.controls['ReimbursementType'].value;
    data['RequestedValue']=this.edit.controls['RequestedValue'].value;
    data['Currency']=this.edit.controls['Currency'].value;
    data['RequestPhase']="Pending";
    data['ImagePath']=this.imgPath.dbPath;
    
     

    this.resSer.updateClaim(data,id).subscribe((res: any) => {
      this.isEdited = res;
      this.resSer
        .getMyRecord(this.router.snapshot.params['id'])
        .subscribe((res: any) => {
          this.collection = res;
        });
    });
  }

  //delete record
  isDeleted: boolean = false;

  deleteRecord(id: any) {
    console.warn(id);
    this.resSer.deleteMyRecord(id).subscribe((res: any) => {
      this.isDeleted = true;
      this.resSer
        .getMyRecord(this.router.snapshot.params['id'])
        .subscribe((res: any) => {
          this.collection = res;
          console.warn(this.collection);
        });
    });
  }
  // for modals
  triggerModal(content: any,data?:any) {
    this.isAdded = false;
    this.isEdited=false;
    if(data!=null){
      console.warn(data)
     this.edit= new FormGroup({
        Date: new FormControl(data['Date'].substring(0, 10)),
        ReimbursementType: new FormControl(data['ReimbursementType']),
        RequestedValue: new FormControl(data['RequestedValue']),
        Currency: new FormControl(data['Currency']),
      });
      this.imgPath['dbPath']=data['ImagePath'] 

    }
    
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
