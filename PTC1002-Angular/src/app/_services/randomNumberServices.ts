import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
// import { environment } from 'src/environments/environment.prod';

@Injectable({
  providedIn: 'root'
})
export class RandomNumberService {
  baseUrl = environment.apiUrl + 'NumberGenerator/';
  
  constructor(private http: HttpClient){ }
  generateRandomNumberWithP(floatP:any,numericP:any,alphanumericP:any,fileSize:any) {
    if(fileSize=="")
    {
      fileSize=undefined;
    }
    return this.http.get(this.baseUrl + 'StringOutPut/'+numericP+'/'+floatP+'/'+alphanumericP+'/'+fileSize).toPromise();
  }
  generateRandomNumber(fileSize:any) {
    if(fileSize=="")
    {
      fileSize=undefined;
    }
    return this.http.get(this.baseUrl + 'generateRandomNumber/'+fileSize).toPromise();
  }
  
emptyTextFile() {
  return this.http.get(this.baseUrl + 'EmptyTextFile');
}

}