import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { NgIcon } from "@ng-icons/core";

@Component({
  selector: 'app-overview',
  imports: [NgIcon, RouterLink],
  templateUrl: './overview.html',
  styleUrl: './overview.css',
})
export class Overview {

}
