import { Component, OnInit, Output } from '@angular/core';
import { IUserCodeAward } from './winner-list.model';
import { WinnersListService } from './winners-list.service';

@Component({
  selector: 'app-winners-list',
  templateUrl: './winners-list.component.html',
  styleUrls: ['./winners-list.component.css']
})
export class WinnersListComponent implements OnInit {
  private winners: Array<IUserCodeAward>;

  constructor(private winnerListService : WinnersListService) { 
    this.winners = [];
  }

  ngOnInit() {
    this.winnerListService.getAllWinners().subscribe((result) => {
      this.winners = result;
    },(error) => {
      console.log(error);
    })
  }
}
