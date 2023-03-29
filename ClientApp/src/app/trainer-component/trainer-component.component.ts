import { Component, OnInit } from '@angular/core';
import {
  HttpClientModule,
  HttpClient,
  HttpParams,
  HttpHeaders
} from '@angular/common/http';
@Component({
  selector: 'app-trainer-component',
  templateUrl: './trainer-component.component.html',
  styleUrls: ['./trainer-component.component.css']
})
export class TrainerComponentComponent implements OnInit {


  ngOnInit(): void {
  }
  public files: any[];
  trainers: any;
  constructor(public http: HttpClient) {
    this.files = [];
    this.http.get('https://localhost:7130/TP/GetAllTrainer')
      .subscribe(data => {
        this.trainers = data;
        // alert(data);
      });
  }
}
