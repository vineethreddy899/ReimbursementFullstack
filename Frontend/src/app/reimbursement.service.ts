import { Injectable } from '@angular/core';
import {HttpClient, HttpHeaders} from '@angular/common/http'
import { map } from 'rxjs/internal/operators/map';

@Injectable({
  providedIn: 'root'
})
export class ReimbursementService {
  url="https://localhost:44300/";

  constructor(private http:HttpClient) { }

  signin(data:any) {
    const header = new HttpHeaders();
    header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.post(this.url+`api/signin`, data,{headers:header});
  }
  signup(data:any) {
    const header = new HttpHeaders();
    header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.post(this.url+`api/signup`, data,{headers:header});
  }
  isUnique(data:any) {
    const header = new HttpHeaders();
    // header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.post(this.url+`api/IsUniqueEmail`, data,{headers:header});
  }

  getAll() {
    const header = new HttpHeaders();
  
    header.append( 'mode', 'no-cors');
    return this.http.get(this.url+`getall`,{headers:header});
  }

  getMyRecord(id:any) {
    const header = new HttpHeaders();
  
    header.append( 'mode', 'no-cors');
    return this.http.get(this.url+`getMyReimbursement/`+id,{headers:header});
  }

  addClaim(data:any){
    const header = new HttpHeaders();
    header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.post(this.url+`addreimbursement`, data,{headers:header});
  }
  updateClaim(data:any,id:any){
    const header = new HttpHeaders();
    header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.put(this.url+`editreimbursement/`+id, data,{headers:header});
  }

  deleteMyRecord(id:any){
    const header = new HttpHeaders();
  
    header.append( 'mode', 'no-cors');
    return this.http.delete(this.url+`delete/`+id,{headers:header});
  } 

  getPending(data:any){
    const header = new HttpHeaders();
    header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.post(this.url+`getPending`, data,{headers:header});

  }
  getApproved(data:any){
    const header = new HttpHeaders();
    header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.post(this.url+`getApproved`, data,{headers:header});

  }
  getDeclined(data:any){
    const header = new HttpHeaders();
    header.append( 'mode', 'no-cors');
    header.append( 'Content-Type', 'application/json');
    return this.http.post(this.url+`getDeclined`, data,{headers:header});

  }
 
  
}
