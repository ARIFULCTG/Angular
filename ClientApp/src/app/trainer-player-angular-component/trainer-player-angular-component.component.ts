import { Component, OnInit } from '@angular/core';
import { HttpClientModule, HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';

import { ActivatedRoute, Router } from '@angular/router';
import { DataService } from "../Data.Service";
import { Subscription } from 'rxjs';


@Component({
  selector: 'app-trainer-player-angular-component',
  providers: [DataService],
  templateUrl: './trainer-player-angular-component.component.html',
  styleUrls: ['./trainer-player-angular-component.component.css']
})
export class TrainerPlayerAngularComponentComponent implements OnInit {

  ngOnInit(): void {
    this.data.user
      .subscribe(res => {
        // alert(res);
        if (!!res) {
          this.data2 = res;
        }
      });

  }
  public files: any[];
  players: any;
  sl: number = 0;
  playername2: string = "";
  //name2 with linked with <input name="name" id="name" #name  [value]=name2 />
  playercode2: string = "";
  playername: string = '';
  trainerid2: string = "";
  traincost2: number = 0;
  earned2: number = 0;
  matchdate2: string = "";
  picture2: string = "";
  trainername2: string = "";
  location2: string = "";
  data2: string = "";
  message: string = "";
  //subscription: Subscription;
  constructor(public http: HttpClient, public data: DataService, private route: ActivatedRoute) {
    this.files = [];
    this.http.get('https://localhost:7130/TP/GetAllPlayer')
      .subscribe(data => {
        this.players = data;
        console.log(this.players);
      });
    this.sl = 0;
    //console.log(this.message);
    this.route.queryParams.subscribe(params => {
      this.trainerid2 = params['trainerid'];
      // alert("LL")
      this.trainerchange();

      //  alert(this.trainerid2);
    });


  }
  trainerchange() {
    this.players = [];
    this.trainername2 = "";
    this.location2 = "";
    //alert('https://localhost:7130/TP/GetTrainer/' + this.trainerid2)
    this.http.get('https://localhost:7130/TP/GetTrainer/' + this.trainerid2)
      .subscribe(data => {
        if (data != "") {
          this.trainername2 = Object.values(data)[0].trainername;
          this.location2 = Object.values(data)[0].location;
          this.showPlayer();
        }
      });
  }
  showPlayer() {
    // alert("here")
    this.http.get('https://localhost:7130/TP/GetPlayer/' + this.trainerid2)
      .subscribe(data => {
        this.players = data;
        console.log(this.players);
      });
    /*this.sl = 0;*/
    // dataService.test = "hello";
  }
  onFileChanged(event: any) {
    this.files = event.target.files;
    const formData = new FormData();
    formData.append('files', this.files[0]);
    
    this.http.post('https://localhost:7130/TP/Post/', formData).subscribe(data => {
      alert(this.files[0].name);
      this.picture2 = this.files[0].name
    });
  }
  addPlayer(playercode: string, playername: string, trainerid: string, traincost: string, earned: string, matchdate: string, picture: string): void {
    this.players.push({
      playercode: playercode,
      playername: playername,
      traincost: traincost,
      earned: earned,
      matchdate: matchdate,
      picture: this.files[0].name,

    });
    this.playername2 = '';
    this.playercode2 = '';
    //this.trainerid2 = "";
    this.traincost2 = 0;
    this.earned2 = 0;
    this.matchdate2 = "";
  }
  convertDate(inputFormat: Date) {
    function pad(s: number) { return (s < 10) ? '0' + s : s; }
    var d = new Date(inputFormat)
    return [d.getFullYear(), pad(d.getMonth() + 1), pad(d.getDate())].join('-')
  }
  show(id: number, playercode1: string, playername1: string, trainerid1: string, traincost1: number, earned1: number, matchdate1: Date, picture1: string): void {
    this.sl = id;
    this.playername2 = playername1;
    this.playercode2 = playercode1;
    this.traincost2 = traincost1;
    this.earned2 = earned1;
    this.matchdate2 = this.convertDate(new Date(matchdate1));
    this.picture2 = picture1;

  }
  updatePlayer(playercode: HTMLInputElement, playername: HTMLInputElement, trainerid: HTMLInputElement, traincost: HTMLInputElement, earned: HTMLInputElement, matchdate: HTMLInputElement): void {
    this.players[this.sl].playercode = playercode.value;
    this.players[this.sl].playername = playername.value;
    /*this.players[this.sl].trainerid = trainerid.value;*/
    this.players[this.sl].traincost = traincost.value;
    this.players[this.sl].earned = earned.value;
    this.players[this.sl].matchdate = matchdate.value;
    this.playername2 = '';
    this.playercode2 = '';
    /*this.trainerid2 = "";*/
    this.traincost2 = 0;
    this.earned2 = 0;
    this.matchdate2 = "";
    this.picture2 = "";

  }
  deletePlayer(): void {
    this.players.splice(this.playercode2, 1);
    this.playername2 = '';
    this.playercode2 = '';
    // this.trainerid2 = "";
    this.traincost2 = 0;
    this.earned2 = 0;
    this.matchdate2 = "";
  }

  deleteAll(): void {
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    var data = {};
    this.http.post<any>('https://localhost:7130/TP/RemoveTrainerPlayerVm/' + this.trainerid2, JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:44437/';
    });
  }

  saveAll(): void {
    var i = 0;
    var detailsArr = [];
    var trainer2 = {
      trainerid: this.trainerid2,
      trainername: this.trainername2,
      location: this.location2
    };
    for (let value of this.players) {
      detailsArr.push({
         playercode: value.playercode,
        playername: value.playername,
        trainerid: this.trainerid2,
        traincost: value.traincost,
        earned: value.earned,
        matchdate: value.matchdate,
        picture: value.picture,
      });
    }
    var data = {
      "trainer": trainer2,
      "player": detailsArr
    };
    const httpOptions = {
      headers: new HttpHeaders({ 'Content-Type': 'application/json' })
    }
    console.log(JSON.stringify(data));
    this.http.post<any>('https://localhost:7130/TP/AddTrainerPlayerVm', JSON.stringify(data), httpOptions).subscribe(data => {
      window.location.href = 'https://localhost:44437/';
    });
  }
  myalert(data: string) {
    this.data.user
      .subscribe(res => {
        // alert(res);
        if (!!res) {
          this.data2 = res;
        }
      });
  }
}

