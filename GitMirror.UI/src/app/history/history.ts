import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgIconComponent } from '@ng-icons/core';

@Component({
  selector: 'app-history',
  imports: [NgIconComponent, RouterLink],
  templateUrl: './history.html',
  styleUrl: './history.css',
})
export class History {

}
