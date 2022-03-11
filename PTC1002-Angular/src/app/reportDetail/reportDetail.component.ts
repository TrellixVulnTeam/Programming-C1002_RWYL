import { Component, OnInit, NgModule } from '@angular/core';
import { Router } from '@angular/router';
import { IReportDetailDto, ReportDetailDto } from '../shared/models/reportDetailDto';
import { ReportDetailService } from '../_services/reportDetailServices';
@Component({
  selector: 'app-reportDetail',
  templateUrl: './reportDetail.component.html',
  styleUrls: ['./reportDetail.component.css'],
})
export class ReportDetailComponent implements OnInit {
  alphaNumericPerct: number=0;
  floatPerct: number=0;
  numericPerct: number=0;
  fileSize: any;
  process:any;
  n: number = 1;
  floatCounter: number = 0;
  numCounter: number = 0;
  alphaCounter: number = 0;
  reportDetailDto: any;
  constructor(private router: Router,private reportDetailService: ReportDetailService,) { }

  ngOnInit() {
    this.viewReport();
  }
 
  //-------------report--------------//

  viewReport(){
    debugger;
    this.reportDetailService.viewRandomNumberReport().subscribe(resp => {
      debugger;
      this.reportDetailDto = resp as ReportDetailDto;
      this.alphaNumericPerct=this.reportDetailDto.alphaNumericPerct;
      this.floatPerct=this.reportDetailDto.floatPerct;
      this.numericPerct=this.reportDetailDto.numericPerct;
    }, error => {
      console.log(error);
    });
  
    }
}

