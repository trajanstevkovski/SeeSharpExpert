import { Component, OnInit } from '@angular/core';
import { IUserCode, ICode } from '../winners-list/winner-list.model';
import { SubmitCodeService } from './submit-code.service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-submit-code',
  templateUrl: './submit-code.component.html',
  styleUrls: ['./submit-code.component.css']
})
export class SubmitCodeComponent implements OnInit {
  userCode: IUserCode = {} as IUserCode;

  constructor(private submitService : SubmitCodeService,
     private toastrService : ToastrService,
     private router : Router
     ) { 
    this.userCode.code = {} as ICode;
  }

  submit(){
    this.submitService.submitCode(this.userCode).subscribe((result)=> {
      console.log(result);
      if(result == null){
        this.toastrService.info("Try again", "Better luck next time.");
        this.router.navigate(["/"]);
      }
      this.toastrService.success(result.awardDescription ,"You got a REWARD");
      this.router.navigate(["/"]);
    },(error) => {
      console.log(error);
      this.toastrService.error(error.error.exceptionMessage, "Error");
    })
  }
  ngOnInit() {
  }

}
